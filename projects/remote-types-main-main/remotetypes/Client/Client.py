#!/usr/bin/env python3

import sys
import Ice
from typing import List
Ice.loadSlice("remotetypes.ice")
import RemoteTypes as rt  # type: ignore

class Rset:
    """ Inicializamos clase Rset para su test"""
    def __init__(self, factory_service):
        """ Iniciamos la clase"""
        self.factory = factory_service
    """ Iniciamos las distintas funciones"""
    def iniciar(self, transfer_set):
        while True:
            print("\nSelecciona una operación para el RSet:")
            print("1. Añadir un elemento")
            print("2. Eliminar un elemento")
            print("3. Verificar la longitud del RSet")
            print("4. Verificar si un elemento está en el RSet")
            print("5. Obtener el hash del RSet")
            print("6. Iterar sobre los elementos del RSet")
            print("7. Imprimir el identificador")
            print("8. Salir")

            try:
                option = int(input("¿Qué opción eliges? (1-8): "))
            except ValueError as e:
                print("No es un número válido, excepcion: ",e)
                continue

            if option == 1:
                item = input("¿Qué elemento quieres añadir? ")
                transfer_set.add(item)
                print(f"Elemento '{item}' añadido al RSet")
            elif option == 2:
                item = input("¿Qué elemento quieres eliminar? ")
                try:
                    transfer_set.remove(item)
                    print(f"Elemento '{item}' eliminado del RSet.")
                except rt.KeyError as e:
                    print(f"No está el elemento '{item}' en el RSet, excepcion: ", e)
            elif option == 3:
                length = transfer_set.length()
                print(f"El RSet tiene {length} elementos.")
            elif option == 4:
                item = input("¿Qué elemento quieres verificar? ")
                if transfer_set.contains(item):
                    print(f"El elemento '{item}' está en el RSet.")
                else:
                    print(f"El elemento '{item}' no está en el RSet.")
            elif option == 5:
                rset_hash = transfer_set.hash()
                print(f"El hash del RSet es: {rset_hash}")
            elif option == 6:
                iterator = transfer_set.iter()
                print("Iterando sobre los elementos del RSet:")
                while True:
                    try:
                        item = iterator.next()
                        print(item)
                    except rt.StopIteration as e :
                        print("terminado de iterar con excepcion: ",e)
                        break
            elif option == 7:
                idn = transfer_set.identifier()
                print(f"El identificador del RSet es: {idn}")
            elif option == 8:
                print("Saliendo del programa")
                break
            else:
                print("Eso no es una opción válida")


class Rlist:
    """ Inicializamos clase RList para su test"""
    def __init__(self, factory_service):
        """ Iniciamos la clase"""
        self.factory = factory_service

    def iniciar(self, transfer_list):
        """ Iniciamos las distintas funciones"""
        while True:
            print("\nSelecciona una operación para el RList:")
            print("1. Añadir un elemento")
            print("2. Eliminar un elemento")
            print("3. Verificar la longitud del RList")
            print("4. Verificar si un elemento está en el RList")
            print("5. Obtener el hash del RList")
            print("6. Iterar sobre los elementos del RList")
            print("7. Imprimir identificador")
            print("8. Salir")

            try:
                option = int(input("¿Qué opción eliges? (1-8): "))
            except ValueError as e:
                print("No es un número válido, excepcion: ",e)
                continue

            if option == 1:
                item = input("¿Qué elemento quieres añadir? ")
                transfer_list.append(item)
                print(f"Elemento '{item}' añadido al RList")
            elif option == 2:
                item = input("¿Qué elemento quieres eliminar? ")
                try:
                    transfer_list.remove(item)
                    print(f"Elemento '{item}' eliminado del RList.")
                except rt.KeyError as e:
                    print(f"No encuentro el elemento '{item}' en el RList, la excepción ha sido: ",e)
            elif option == 3:
                length = transfer_list.length()
                print(f"El RList tiene {length} elementos.")
            elif option == 4:
                item = input("¿Qué elemento quieres Comprobar? ")
                if transfer_list.contains(item):
                    print(f"El elemento '{item}' está en el RList.")
                else:
                    print(f"El elemento '{item}' no está en el RList.")
            elif option == 5:
                rlist_hash = transfer_list.hash()
                print(f"El hash del RList es: {rlist_hash}")
            elif option == 6:
                iterator = transfer_list.iter()
                print("Iterando sobre los elementos del RList:")
                while True:
                    try:
                        item = iterator.next()
                        print(item)
                    except rt.StopIteration as e:
                        print("Terminado de iterar, con excepcion: ", e)
                        break
            elif option == 7:
                idn = transfer_list.identifier()
                print(f"El identificador del RList es: {idn}")
            elif option == 8:
                print("Saliendo del programa")
                break
            else:
                print("Eso no es una opción válida.")


class Rdict:
    """ Inicializamos clase RDict para su test"""
    def __init__(self, factory_service):
        """ Iniciamos la clase"""
        self.factory = factory_service

    def iniciar(self, transfer_dict):
        """ Iniciamos las distintas funciones"""
        while True:
            print("\nSelecciona una operación para el RDict:")
            print("1. Añadir un elemento")
            print("2. Obtener un elemento")
            print("3. Eliminar un elemento ")
            print("4. Verificar la longitud del RDict")
            print("5. Verificar si un elemento está en el RDict")
            print("6. Obtener el hash del RDict")
            print("7. Iterar sobre las claves del RDict")
            print("8. Imprimir Identificador")
            print("9. Salir")

            try:
                option = int(input("¿Qué opción eliges? (1-9): "))
            except ValueError as e:
                print("no es un número válido, excepcion: ",e)
                continue

            if option == 1:
                key = input("¿Qué clave quieres añadir? ")
                value = input("¿Qué valor quieres añadir? ")
                transfer_dict.setItem(key, value)
                print(f"Elemento '{key}: {value}' añadido al RDict.")
            elif option == 2:
                key = input("¿Qué clave quieres obtener? ")
                try:
                    item = transfer_dict.getItem(key)
                    print(f"Elemento con clave '{key}' encontrado: {item}")
                except rt.KeyError as e:
                    print(f"No esta el elemento con clave '{key}' en el RDict, excepcion:", e)
            elif option == 3:
                key = input("¿Qué clave quieres eliminar? ")
                try:
                    removed_item = transfer_dict.pop(key)
                    print(f"Elemento con clave '{key}' eliminado: {removed_item}")
                except rt.KeyError as e:
                    print(f"No está el elemento con clave '{key}' para eliminar, excepcion:", e)
            elif option == 4:
                length = transfer_dict.length()
                print(f"El RDict tiene {length} elementos.")
            elif option == 5:
                key = input("¿Qué clave quieres Comprobar? ")
                if transfer_dict.contains(key):
                    print(f"El elemento con clave '{key}' está en el RDict.")
                else:
                    print(f"El elemento con clave '{key}' no está en el RDict.")
            elif option == 6:
                rdict_hash = transfer_dict.hash()
                print(f"El hash del RDict es: {rdict_hash}")
            elif option == 7:
                iterator = transfer_dict.iter()
                print("Iterando sobre las claves del RDict:")
                while True:
                    try:
                        key = iterator.next()
                        print(key)
                    except rt.StopIteration as e:
                        print("iteracion termianda, excepcion:", e)
                        break
            elif option == 8:
                idn = transfer_dict.identifier()
                print(f"El identificador del RDict es: {idn}")
            elif option == 9:
                print("Saliendo del programa")
                break
            else:
                print("Eso no es una opción válida.")


class Client(Ice.Application):
    """ DEfinimos el cliente para ejecutar pruebas"""
    def run(self, argv: List[str]) -> int:
        """ Iniciamos todos los componentes del cliente"""
        try:
            proxy = self.communicator().stringToProxy(argv[1])
        except IndexError as e:
            print("Ingresa el Proxy, !Es Obligatorio!, excepcion: ", e)
            return -1

        factory_service = rt.FactoryPrx.checkedCast(proxy)
        if not factory_service:
            print("Proxy no correcto")
            return -1

        while True:
            print("\nElige una opción para poder realizar las pruebas:")
            print("1. RSet")
            print("2. RList")
            print("3. RDict")
            print("4. Salir")

            try:
                selection = int(input("Elige una opción (1, 2, 3 o 4): "))
            except ValueError:
                print("Numero no valido Elige 1, 2, 3 o 4.")
                continue

            if selection == 1:
                name = input("¿Quieres darle un nombre al RSet? (presiona Enter para no darle un nombre): ") 
                transfer = factory_service.get(rt.TypeName.RSet, name)
                print(f"Creando un RSet")
                transfer_set = rt.RSetPrx.checkedCast(transfer)
                if not transfer_set:
                    print("No se pudo crear el RSet")
                    continue
                clientSet = Rset(factory_service)
                clientSet.iniciar(transfer_set)
            
            elif selection == 2:
                name = input("¿Quieres darle un nombre al RList? (presiona Enter para no darle unnombre): ")
                transfer = factory_service.get(rt.TypeName.RList, name)
                print(f"Creando un RList")
                transfer_list = rt.RListPrx.checkedCast(transfer)
                if not transfer_list:
                    print("No se pudo crear el RList")
                    continue
                clientList = Rlist(factory_service)
                clientList.iniciar(transfer_list)

            elif selection == 3:
                name = input("¿Quieres darle un nombre al RDict? (presiona Enter para no darle un nombre): ")
                transfer = factory_service.get(rt.TypeName.RDict, name)
                print(f"Creando un RDict")
                transfer_dict = rt.RDictPrx.checkedCast(transfer)
                if not transfer_dict:
                    print("No se pudo crear el RDict")
                    continue
                clientDict = Rdict(factory_service)
                clientDict.iniciar(transfer_dict)

            elif selection == 4:
                break

            else:
                print("Elige 1, 2, 3 o 4.")
                continue

        return 0


if __name__ == '__main__':
    client = Client()
    sys.exit(client.main(sys.argv))
