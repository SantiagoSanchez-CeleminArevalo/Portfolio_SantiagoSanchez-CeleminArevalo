"""Remoteset class we used to implements the diferents functions of RSet."""

import os
import json
from typing import Optional
import Ice
import RemoteTypes as rt  # noqa: F401; pylint: disable=import-error # type: ignore

from remotetypes.iterable import Iterable  # type: ignore
from remotetypes.customset import StringSet


class RemoteSet(rt.RSet):
    """Implementation of the remote interface RSet."""

    def __init__(self, identifier: str, json_file: str = "remoteset.json") -> None:
        """Initialize a RemoteSet with an empty StringSet."""
        self._storage_ = StringSet()
        self.identifi = identifier
        self.json_file = json_file
        self._load()

    def _load(self):
        """Load Information of the RSet JSON."""
        if os.path.exists(self.json_file):
            with open(self.json_file, "r", encoding="utf-8") as file:
                data = json.load(file)
                if self.identifi in data:
                    self._storage_ = StringSet(data[self.identifi]["storage"])

    def _save(self):
        """Save information in the stora of RSet."""
        data = {}
        if os.path.exists(self.json_file):
            with open(self.json_file, "r", encoding="utf-8") as file:
                data = json.load(file)

        data[self.identifi] = {
            "storage": list(self._storage_)
        }

        with open(self.json_file, "w", encoding="utf-8") as file:
            json.dump(data, file, indent=2)

    def identifier(self, current: Optional[Ice.Current] = None) -> str: # pylint: disable=unused-argument
        """Return the identifier of the object."""
        return self.identifi

    def remove(self, item: str, current: Optional[Ice.Current] = None) -> None: # pylint: disable=unused-argument
        """Remove an item from the StringSet."""
        try:
            self._storage_.remove(item)
            self._save()
        except KeyError as error:
            raise rt.KeyError(item) from error

    def length(self, current: Optional[Ice.Current] = None) -> int: # pylint: disable=unused-argument
        """Return the number of elements in the StringSet."""
        return len(self._storage_)

    def contains(self, item: str, current: Optional[Ice.Current] = None) -> bool: # pylint: disable=unused-argument
        """Check the pertenence of an item to the StringSet."""
        return item in self._storage_

    def hash(self, current: Optional[Ice.Current] = None) -> int: # pylint: disable=unused-argument
        """Calculate a hash from the content of the internal StringSet."""
        contents = list(self._storage_)
        contents.sort()
        return hash(repr(contents))

    def iter(self, current: Optional[Ice.Current] = None) -> rt.IterablePrx:
        """Create an iterable object."""
        if current is None:
            raise ValueError("Error, es None")
        iterable = Iterable(self._storage_)
        proxy = current.adapter.addWithUUID(iterable)
        return rt.IterablePrx.checkedCast(proxy)

    def add(self, item: str, current: Optional[Ice.Current] = None) -> None: # pylint: disable=unused-argument
        """Add a new string to the StringSet."""
        self._storage_.add(item)
        self._save()

    def pop(self, current: Optional[Ice.Current] = None) -> str: # pylint: disable=unused-argument
        """Remove and return an element from the storage."""
        try:
            item = self._storage_.pop()
            self._save()
            return item
        except KeyError as exc:
            raise rt.KeyError() from exc
