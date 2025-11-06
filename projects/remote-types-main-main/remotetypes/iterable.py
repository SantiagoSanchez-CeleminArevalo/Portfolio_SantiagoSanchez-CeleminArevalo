"""Needed classes for implementing the Iterable interface for different types of objects."""

from typing import Optional
import RemoteTypes as rt  # noqa: F401; pylint: disable=import-error # type: ignore
import Ice

class Iterable(rt.Iterable):
    """Skeleton for an Iterable implementation."""

    def __init__(self, storage):
        """Initialize the Iterable with the underlying storage."""
        self._storage = storage
        self._iterator = iter(self._storage)

    def next(self, current: Optional[Ice.Current] = None) -> str: #pylint: disable=unused-argument
        """Return the next item in the iteration."""
        try:
            return next(self._iterator)
        except StopIteration as e:
            raise rt.StopIteration() from e
        except RuntimeError as ex:
            raise rt.CancelIteration() from ex