"""RemoteList class we used to implements the diferents functions of RList."""

import json
import os
from typing import Optional
from typing import List
import Ice
import RemoteTypes as rt  # noqa: F401; pylint: disable=import-error # type: ignore
from remotetypes.iterable import Iterable  # type: ignore

class RemoteList(rt.RList):
    """Implementation of the remote interface RList."""

    _storage_: List[str]
    def __init__(self, identifier: str , json_file: str = "remotelist.json") -> None:
        """Initialize the RemoteList with an optional identifier."""
        self._storage_ = []
        self.identifi = identifier
        self.json_file = json_file
        self._load()

    def _load(self):
        """Load Information of the RList JSON."""
        if os.path.exists(self.json_file):
            with open(self.json_file, "r", encoding="utf-8") as file:
                data = json.load(file)
                if self.identifi in data:
                    self._storage_ = data[self.identifi]["storage"]

    def _save(self):
        """Save information in the stora of RList."""
        data = {}
        if os.path.exists(self.json_file):
            with open(self.json_file, "r", encoding="utf-8") as file:
                data = json.load(file)
        data[self.identifi] = {
            "storage": self._storage_
        }
        with open(self.json_file, "w", encoding="utf-8") as file:
            json.dump(data, file, indent=2)

    def identifier(self, current: Optional[Ice.Current] = None) -> str: # pylint: disable=unused-argument
        """Return the identifier of the object."""
        return self.identifi

    def remove(self, item: str, current: Optional[Ice.Current] = None) -> None: # pylint: disable=unused-argument
        """Remove an item from the list."""
        try:
            self._storage_.remove(item)
            self._save()
        except ValueError as e:
            raise rt.KeyError(key=item) from e

    def length(self, current: Optional[Ice.Current] = None) -> int: # pylint: disable=unused-argument
        """Return the length of the list."""
        return len(self._storage_)

    def contains(self, item: str, current: Optional[Ice.Current] = None) -> bool: # pylint: disable=unused-argument
        """Check if the item is in the list."""
        return item in self._storage_

    def hash(self, current: Optional[Ice.Current] = None) -> int: # pylint: disable=unused-argument
        """Return a hash value for the list (sum of hash values of its items)."""
        return hash(tuple(self._storage_))

    def iter(self, current: Optional[Ice.Current] = None) -> rt.Iterable:
        """Create an iterable object."""
        iterable = Iterable(self._storage_)
        if current is None:
            raise ValueError("Error, es None")
        iterable = Iterable(self._storage_)
        proxy = current.adapter.addWithUUID(iterable)
        return rt.IterablePrx.checkedCast(proxy)

    def append(self, item: str, current: Optional[Ice.Current] = None) -> None: # pylint: disable=unused-argument
        """Append an item to the list."""
        self._storage_.append(item)
        self._save()

    def pop(self, index: Optional[int] = None, current: Optional[Ice.Current] = None) -> str: # pylint: disable=unused-argument
        """Pop an item from the list at the specified index."""
        if index is None or index < 0 or index >= len(self._storage_):
            raise rt.IndexError(message="El indice esta fuera de los límites.")
        item = self._storage_.pop(index)
        self._save()
        return item

    def getItem(self, index: int, current: Optional[Ice.Current] = None) -> str: # pylint: disable=unused-argument
        """Get the item at the specified index."""
        try:
            return self._storage_[index]
        except IndexError as e:
            raise rt.IndexError(message="El indice esta fuera de los límites.") from e
