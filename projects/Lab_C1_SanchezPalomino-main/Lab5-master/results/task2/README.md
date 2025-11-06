# Tarea 2
El código en [src/task2/buffer.cpp](../../src/task2/buffer.cpp) debería representar el siguiente flujo:
1. Inicializar el buffer A
2. Sumar al valor de cada posición de A el índice de la iteración
3. Inicializar el buffer B Del mismo modo que A
4. Calcular B como resultado del valor de B multiplicado por A

## Comprueba el resultado y muéstralo a continuación
* Una vez compilado nuestro código mediante el comando 'icpx -fsycl Buffer.cpp -o Buffer' obtenemos un ejecutable, que tras ejecutarlo obtenemos el resultado de :
    0
    2
    8
    18
    32
    50
    72
    98
    128
    162
    200
    242
    288
    338
    392
    450



## ¿Qué abstracción se está usando para los contenedores de datos?
* Se esta usando una anstracción de acceso a Buffers, utilizando buffers de tamño 16
## ¿Cómo se está formando el DAG? ¿implicitamente? ¿explicitamente?
* Se esta formando de fomra implicita por el uso de accesos

## Enumera todas las dependencias y el tipo de dependencias
* Kernel 1 y Kernel 2 sobre Buffer A (RAW)
    -> Kernel 2 lee (A[i]) y escribe despues de que el Kernel 1 lo modifique
    
* Kernel 2 y Kernel 4 sobre Buffer A (RAW)
    ->Kernel 4 lee (A[i])  despues de que Kernel 2 lo modifique

* Kernel 3 y Kernel 4 sobre Buffer B (RAW)
    ->Kernel 4 lee (B[i]) despues de que Kernel 3 lo inicie


----

# Task 2
Code in [src/task2/buffer.cpp](../../src/task2/buffer.cpp) should represent the following flow:
1. Initialize buffer A
2. Add to each item in A the index of the iteration
3. Initialize buffer B the same way than A
4. Compute B as the result of B multiplied by A

## Check the result and show it below
**Answer here**

## Which abstraction is being used for data containers?
**Answer here**

## How is the DAG being built implicitly or explicitly?
**Answer here**

## Enumerate all dependencies and their types
**Answer here**
