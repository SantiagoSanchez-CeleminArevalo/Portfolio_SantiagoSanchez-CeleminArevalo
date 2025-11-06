"""In this class imports and inicializate the diferents factory's."""

import uuid
import Ice
from typing import Optional
from typing import Dict
import RemoteTypes as rt  # noqa: F401; pylint: disable=import-error

from remotetypes.remoteset import RemoteSet
from remotetypes.remotedict import RemoteDict
from remotetypes.remotelist import RemoteList


class Factory(rt.Factory):
    """Init the class factory of diferent tipes of factory."""

    def __init__(self):
        """Pass de correct parametres."""
        self._instances: Dict[str, rt.RTypePrx] = {}


    def get(self, typeName: rt.TypeName,
        identifier: Optional[str] = None,
        current: Optional[Ice.Current] = None) -> rt.RTypePrx:
        """Obtiene o crea una instancia del tipo especificado."""
        # If no identifier is provided, generate a random one
        if not identifier:
            identifier = str(uuid.uuid4())
        # If an instance with this identifier already exists, we return the same
        if identifier in self._instances:
            return rt.RTypePrx.checkedCast(self._instances[identifier])

        if current is None:
            raise ValueError("Current es None en la factoria")
        if typeName == rt.TypeName.RSet:
            servant = RemoteSet(identifier)
            proxy = current.adapter.addWithUUID(servant)
            self._instances[identifier] = proxy
            return rt.RTypePrx.checkedCast(proxy)

        elif typeName == rt.TypeName.RList:
            servant = RemoteList(identifier)
            proxy = current.adapter.addWithUUID(servant)
            self._instances[identifier] = proxy
            return rt.RTypePrx.checkedCast(proxy)

        elif typeName == rt.TypeName.RDict:
            servant = RemoteDict(identifier)
            proxy = current.adapter.addWithUUID(servant)
            self._instances[identifier] = proxy
            return rt.RTypePrx.checkedCast(proxy)
        else:
            raise ValueError(f"Tipo {typeName} falló al crear factoria")

