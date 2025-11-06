# Test de ejecución:
---
> En esta sección encontrará algunos de los test ejecutados mediante el código 'test.py'
---
- python3 test.py operations localhost:9092 '[{"id": 1, "object_identifier": "rset_123", "object_type": "RSet", "operation": "add", "args": {"item": "Elemento A"}}, {"id": 2, "object_identifier": "rset_123", "object_type": "RSet", "operation": "remove", "args": {"item": "Elemento A"}}]'

- python3 test.py operations localhost:9092 '[{"id": 3, "object_identifier": "rset_123", "object_type": "RSet", "operation": "length", "args": {}}, {"id": 4, "object_identifier": "rset_123", "object_type": "RSet", "operation": "contains", "args": {"item": "Elemento A"}}]'

- python3 test.py operations localhost:9092 '[{"id": 5, "object_identifier": "rlist_456", "object_type": "RList", "operation": "append", "args": {"item": "Elemento 1"}}, {"id": 6, "object_identifier": "rlist_456", "object_type": "RList", "operation": "remove", "args": {"item": "Elemento 2"}}, {"id": 14, "object_identifier": "rdict_789", "object_type": "RDict", "operation": "hash", "args": {}}]'

- python3 test.py operations localhost:9092 '[{"id": 7, "object_identifier": "rlist_456", "object_type": "RList", "operation": "length", "args": {}}, {"id": 8, "object_identifier": "rlist_456", "object_type": "RList", "operation": "contains", "args": {"item": "Elemento 1"}}]'

- python3 test.py operations localhost:9092 '[{"id": 24, "object_identifier": "rdict_789", "object_type": "RDict", "operation": "setItem", "args": {"key": "clave1", "item": "24"}},{"id": 10, "object_identifier": "rdict_789", "object_type": "RDict", "operation": "getItem", "args": {"key": "clave1"}}, {"id": 11, "object_identifier": "rdict_789", "object_type": "RDict", "operation": "remove", "args": {"key": "clave1"}}, {"id": 12, "object_identifier": "rdict_789", "object_type": "RDict", "operation": "length", "args": {}}]'


**Fallos**

- python3 test.py operations localhost:9092 'Este no es un JSON'

- python3 test.py operations localhost:9092 '[{'id': 3, 'object_identifier': 'rset_123', 'object_type': 'RSet', 'operation': 'length', 'args': {}}]'

- python3 test.py operations localhost:9092 '[{"id": 5, "object_identifier": "rlist_456", "object_type": "RList", "operation": "append", "args": {"item": "Elemento 1"}'

- python3 test.py operations localhost:9092 ''
