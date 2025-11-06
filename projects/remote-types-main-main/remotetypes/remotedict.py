"""RemoteDict class we used to implements the diferents functions of RDict."""

import json
import os
from typing import Optional
from typing import Dict
import Ice
import RemoteTypes as rt  # noqa: F401; pylint: disable=import-error # type: ignore
from remotetypes.iterable import Iterable  # type: ignore

class RemoteDict(rt.RDict):
    """Implementation of the remote interface RDict."""

    _storage_: Dict[str, str]
    def __init__(self, identifier: str, json_file: str = "remotedict.json") -> None:
        """Initialize the RemoteDict with an optional identifier."""
        self._storage_ = {}
        self.identifi = identifier
        self.json_file = json_file
        self._load()

    def _load(self):
        """Load Information of the RDict JSON."""
        if os.path.exists(self.json_file):
            with open(self.json_file, "r", encoding="utf-8") as file:
                data = json.load(file)
                if self.identifi in data:
                    self._storage_ = data[self.identifi]["storage"]

    def _save(self):
        """Save information in the stora of RDict."""
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
        """Remove an item from the dictionary."""
        if item in self._storage_:
            del self._storage_[item]
            self._save()
        else:
            raise rt.KeyError(key=item)

    def length(self, current: Optional[Ice.Current] = None) -> int: # pylint: disable=unused-argument
        """Return the length of the dictionary."""
        return len(self._storage_)

    def contains(self, item: str, current: Optional[Ice.Current] = None) -> bool: # pylint: disable=unused-argument
        """Check if the dictionary contains a specific key."""
        return item in self._storage_

    def hash(self, current: Optional[Ice.Current] = None) -> int: # pylint: disable=unused-argument
        """Return a hash value for the dictionary (sum of hash values of its keys)."""
        return hash(frozenset(self._storage_.keys()))

    def iter(self, current: Optional[Ice.Current] = None) -> rt.Iterable:
        """Create an iterable object for the dictionary's keys."""
        iterable = Iterable(self._storage_)
        if current is None:
            raise ValueError("Error, es None")
        iterable = Iterable(self._storage_.keys())
        proxy = current.adapter.addWithUUID(iterable)
        return rt.IterablePrx.checkedCast(proxy)

    def setItem(self, key: str, item: str, current: Optional[Ice.Current] = None) -> None: # pylint: disable=unused-argument
        """Set an item in the dictionary with the specified key."""
        self._storage_[key] = item
        self._save()

    def getItem(self, key: str, current: Optional[Ice.Current] = None) -> str: # pylint: disable=unused-argument
        """Get an item from the dictionary by key."""
        if key in self._storage_:
            return self._storage_[key]
        raise rt.KeyError(key=key)

    def pop(self, key: str, current: Optional[Ice.Current] = None) -> str: # pylint: disable=unused-argument
        """Pop an item from the dictionary by key."""
        if key in self._storage_:
            value = self._storage_.pop(key)
            self._save()
            return value
        raise rt.KeyError(key=key)
