INFORME COMPRACIÓN
--------------------------------------------------------------------
Para realizar dicho informe, ha sido necesario utilizar, el primer snapshot (Warning) de la compilación
de nuestro código con un tamaño de 300, y el segundo snapshot(snapshot) de la compilación de nuestro código
con un tamaño de 1000. En ello, hemos podido analizar los disitntos resultados, ofreciendonos estos
resultados:

En Primer Lugar analizaremos el snapshot 'Con optimizaciones' de tamaño 1000

Medidas de Tiempo:
    Tiempo total de ejecución del programa: 59,79 Segundos
    Tiempo total en bucles críticos:    
        matmul.cpp: 72 = 59,560 Segundos
        matmul.cpp: 69, 47, 58 = Cuentas con tiempos mínimos en milisegundos
------------------------------------------------------------------------
Rendimiento:
    Operaciones de punto flotante(GFLOPS): 
        0,27 que es un 0,47% de rendimiento máximo de doble precisión
        0,27 que es un 0,24% de rendimiento máximo de precisión única
        *Esto indica que dicho programa se encuentra muy lejos de llegar el potencial completo de CPU

    Operaciones enteras por Segundo(GINTOPS): 1,34, el cual equivale 2,99% de los 44,86 GINTOPS
-------------------------------------------------------------------------
Uso de Memoria:
    Ancho de Banda CPU: 
         15,020 GB/s que es un 4,69% de los  320,245 GB/s
        *Dado que utiliza poco ancho de banda puede que no pueda utilizar mas, dado que esta sufriendo un 'Cuello de botella'

    Ancho de Banda de Cache L2 y L3:
        L2: 9,64 GB/s lo que supone un 10,63%
        L3: 1,71 GB/s lo que supone un 2,79%
        *Esto esta suponiendo que el rendimiendo de la cache L2 es superior a la cache L3
        por lo que podria afectar a el total del rendimiento por el rendimeinto bajo de L3
    
    Ancho de Banda DRAM:
        Supone un 1,07 GB/s, lo que supone un 5,49% del máximo(19,58 GB/s)
--------------------------------------------------------------------------
Conclusión:
    Medidas de Tiempo: Casi todo el tiempo podemos comprobar como se centra en un único bucle, ofreciendo
                       al usuario una optimización de ese bucle bastante crítica
    
    Rendimiento: Dicho programa podemos comprobar como no está utilizando al máximo
    
    Uso de Memoria: El uso de la memoria y su acceso son muy bajos
****************************************************************************************************************
En segundo lugar analizaremos el snapsot (warning) 'sin optimizaciones'

Medidas de Tiempo:
    Tiempo total de ejecución del programa: 0,09 Segundos
    Tiempo total en bucles críticos:    
        matmul.cpp: 72 = 0,080 Segundos
        matmul.cpp: 69, 47, 58 = Cuentans con tiempos mínimos que no nos aportan casi nada
------------------------------------------------------------------------
Rendimiento:
    Operaciones de punto flotante(GFLOPS): 
        0,64 que es un 1,12% de rendimiento máximo de doble precisión
        0,64 que es un 0,55% de rendimiento máximo de precisión única
        *Esto nos indica que dichas operaciones utilizan muy poco el potencial harware total

    Operaciones enteras por Segundo(GINTOPS):
        3,18, el cual equivale 7,32% de los 43,11 GINTOPS( X64)
        3,18, el cual equivale 3,79% de los 83,87 GINTOPS( X32)
-------------------------------------------------------------------------
Uso de Memoria:
    Ancho de Banda CPU: 
         35,45 GB/s que es un 11,11% de los  318,93 GB/s
        *Dado que utiliza poco ancho de banda puede que no pueda utilizar mas, dado que esta sufriendo un 'Cuello de botella'

    Ancho de Banda de Cache L2 y L3:
        L2: 3,39 GB/s lo que supone un 3,21%
        L3: 1,2,58 GB/s lo que supone un 4,12%
    
    Ancho de Banda DRAM:
        Supone un 0,025 GB/s, lo que supone un 0,13% del máximo(19,58 GB/s)
--------------------------------------------------------------------------
Conclusión:
    Medidas de Tiempo: Casi todo el tiempo podemos comprobar como se centra en un único bucle, ofreciendo
                        por lo que la mejor idea para optimizar este código es mejorar ese bucle
    
    Rendimiento: Dicho programa podemos comprobar como no está utilizando al máximo
    
    Uso de Memoria: El uso de la memoria y su acceso son muy bajos
***********************************************************************************************
Ahora realizaremos la comparación de los dos snapshots
--------------------------------------------------------------------------
Medida de Tiempo:

    El programa 1 tiene un tiempo de ejecucción, bastante superior respecto al programa 2. Pero
    los dos programas vemos como su momento más crítico es el bucle de la línea 72. Aun así, dicho bucle es mas significativo en el programa 'sin optimizaciones' dado que el tamaño
    de la matriz es inferior (300 < 1000)
----------------------------------------------------------------------------
Rendimiento:

    Respecto a operaciones con punto flotante, comprobamos como el programa 'sin optimizaciones'
    cuenta con un mejor rendimiento que el programa 'con optimizaciones', auque ambos no utilizan 
    la máxima potencia de CPU

    Para realizar mejor el analisis de ambos programas podemos decir que el programa 'sin optimizaciones'
    utiliza más el procesamiento que el programa 'con optimizaciones'
-----------------------------------------------------------------------------
Uso de Memoria:

    Podemos comprobar como el programa 'sin optimizaciones' utiliza bastante mas el ancho de banda
    con la CPU y la memoria L1, en comparación del programa 'con optimizaciones'. Lo que explica que
    el programa 'sin optimizaciones sea más rapido

    También comprobamos como el programa 'con optimizaciones' Utiliza mejor la cache L2, pero la
    cache L3 es utilizada mejor por el programa 'sin optimizaciones'

    Por último, comprobamos como el programa 'con optimizaciones' tiene un mejor uso de la memoria DRAM
    frente al otro programa, pero los dos niveles son muy bajos, lo que nos está indicando un cuello de botella
---------------------------------------------------------------------------------
Conclusión:
     
    El programa 'sin optimizaciones' es mucho más rápido a la hora de la ejecución, esto también se debe
    al bajo tamaño de la patriz. Pero también tiene un mejor rendimiento en operaciones de punto flotante,
    sin olvidarnos de que su ancho de banda es superior

    El programa 'con optimizaciones' cuenta con un tiempo de ejecucción superior, aunque utiliza más caché
    L2 y DRAM. Sin embargo, su rendimiento es bajo, dado que su ancho de banda es muy pequeño
