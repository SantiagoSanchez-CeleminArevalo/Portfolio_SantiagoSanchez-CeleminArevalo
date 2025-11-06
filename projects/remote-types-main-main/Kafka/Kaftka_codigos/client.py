import Ice # type: ignore
import json
from typing import Dict, Any
Ice.loadSlice("Kaftka_codigos/remotetypes.ice")
import RemoteTypes as rt # type: ignore
from RemoteTypes import FactoryPrx, RSetPrx, RListPrx, RDictPrx # type: ignore

import json
from typing import Dict, Any

def recibir_cada_operacion(factory_service, operation: Any) -> Dict[str, Any]:
    """Recibe una operación en formato JSON y la ejecuta sobre el objeto transferido correspondiente."""
    try:
        # Comprobamos si el mensaje es un JSON
        if not isinstance(operation, dict):
            if isinstance(operation, str):
                try:
                    operation = json.loads(operation)
                except json.JSONDecodeError as e:
                    raise ValueError(f"El mensaje no es un JSON válido: {e}")
            else:
                raise ValueError("El mensaje recibido no es un JSON ni una cadena de texto.")
            
        required_keys = ["id", "object_identifier", "object_type", "operation"]
        for key in required_keys:
            if key not in operation:
                raise ValueError(f"Falta clave: {key}")

        op_id = operation["id"]
        obj_id = operation["object_identifier"]
        obj_type = operation["object_type"]
        method = operation["operation"]
        args = operation.get("args", {})

        transfer = get_transfer_object(factory_service, obj_id, obj_type)
        if not transfer:
            raise ValueError(f"No se encontró el objeto: {obj_id} ({obj_type})")

        result = execute_operation(transfer, method, args)

        return {"id": op_id, "status": "ok", "result": result}

    except ValueError as ve:
        print(f"Error de valor: {ve}")
        return {"id": operation.get("id", "unknown") if isinstance(operation, dict) else "unknown",
                "status": "error", "error": "ValueError", "details": str(ve)}

    except KeyError as ke:
        print(f"Error de clave: {ke}")
        return {"id": operation.get("id", "unknown"), "status": "error", "error": "KeyError", "details": str(ke)}

    except Exception as e:
        print(f"Error inesperado al procesar la operación: {e}")
        return {
            "id": operation.get("id", "unknown") if isinstance(operation, dict) else "unknown",
            "status": "error",
            "error": type(e).__name__,
            "details": str(e),
        }


def get_transfer_object(factory_service, identifier: str, obj_type: str):
    "Obtiene el objeto transferido a partir de su identificador y el tipo"
    if obj_type == "RSet":
        transfer = factory_service.get(rt.TypeName.RSet, identifier)
        return rt.RSetPrx.checkedCast(transfer)
    elif obj_type == "RList":
        transfer = factory_service.get(rt.TypeName.RList, identifier)
        return rt.RListPrx.checkedCast(transfer)
    elif obj_type == "RDict":
        transfer = factory_service.get(rt.TypeName.RDict, identifier)
        return rt.RDictPrx.checkedCast(transfer)
    else:
        raise ValueError(f"Tipo de objeto no válido: {obj_type}")

def execute_operation(transfer, operation: str, args: Dict[str, Any]) -> Any:
    "Ejecuta una operación sobre el objeto transferido"
    if isinstance(transfer, RSetPrx):
        return execute_rset_operation(transfer, operation, args)
    elif isinstance(transfer, RListPrx):
        return execute_rlist_operation(transfer, operation, args)
    elif isinstance(transfer, RDictPrx):
        return execute_rdict_operation(transfer, operation, args)
    else:
        raise ValueError("Tipo de objeto desconocido")

def execute_rset_operation(rset: RSetPrx, operation: str, args: Dict[str, Any]) -> Any:
    "Ejecuta una operación sobre un RSet"
    if operation == "add":
        rset.add(args["item"])
        return f"Elemento {args['item']} añadido al RSet."
    elif operation == "remove":
        rset.remove(args["item"])
        return f"Elemento {args['item']} eliminado del RSet."
    elif operation == "length":
        return f"El RSet tiene {rset.length()} elementos."
    elif operation == "contains":
        result = rset.contains(args["item"])
        return f"El elemento {args['item']} {'está' if result else 'no está'} en el RSet."
    elif operation == "hash":
        return f"Hash del RSet: {rset.hash()}"
    elif operation == "identifier":
        return f"Identificador del RSet: {rset.identifier()}"
    elif operation == "iter":
        return {"error": "OperationNotSupported"}
    else:
        raise ValueError(f"Operación no válida: {operation}")

def execute_rlist_operation(rlist: RListPrx, operation: str, args: Dict[str, Any]) -> Any:
    "Ejecuta una operación sobre un RList"
    if operation == "append":
        rlist.append(args["item"])
        return f"Elemento {args['item']} añadido al RList."
    elif operation == "remove":
        rlist.remove(args["item"])
        return f"Elemento {args['item']} eliminado del RList."
    elif operation == "length":
        return f"El RList tiene {rlist.length()} elementos."
    elif operation == "contains":
        result = rlist.contains(args["item"])
        return f"El elemento {args['item']} {'está' if result else 'no está'} en el RList."
    elif operation == "hash":
        return f"Hash del RList: {rlist.hash()}"
    elif operation == "identifier":
        return f"Identificador del RList: {rlist.identifier()}"
    elif operation == "iter":
        return {"error": "OperationNotSupported"}
    else:
        raise ValueError(f"Operación no válida: {operation}")

def execute_rdict_operation(rdict: RDictPrx, operation: str, args: Dict[str, Any]) -> Any:
    "Ejecuta una operación sobre un RDict"
    if operation == "setItem":
        rdict.setItem(args["key"], args["item"])
        return f"Elemento {args['key']}: {args['item']} añadido al RDict."
    elif operation == "getItem":
        item = rdict.getItem(args["key"])
        return f"Elemento con clave {args['key']} encontrado: {item}"
    elif operation == "remove":
        removed_item = rdict.pop(args["key"])
        return f"Elemento con clave {args['key']} eliminado: {removed_item}"
    elif operation == "length":
        return f"El RDict tiene {rdict.length()} elementos."
    elif operation == "contains":
        result = rdict.contains(args["key"])
        return f"El elemento con clave {args['key']} {'está' if result else 'no está'} en el RDict."
    elif operation == "hash":
        return f"El hash del RDict es: {rdict.hash()}"
    elif operation == "identifier":
        return f"El identificador del RDict es: {rdict.identifier()}"
    elif operation == "iter":
        return {"error": "OperationNotSupported"}
    else:
        raise ValueError(f"Operación no válida: {operation}")

