# Resolución Ejercicio 2
Para la resolución de este ejercicio 2 hemos utilizado el comando para compilar gcc -x c -fopenmp  nombre.cpp -o salida.cpp

    -> función -x c : Compilammos como si fuese un .c
    -> función -fopenmp : utilizar directivas fopenmp
    -> función -o : Asignamos nombre
## Primitivas utilizadas
- En primer lugar compilamos el modelo sumavector.cpp en el que se encuentra nuestro código sin parelelizar, posteriormente ofrecemos dos soluciones:
    -> Solución 00:
        *Utilizamos '#pragma omp for', este será utilizado para que el bucle sea distribuido entre los distintos hilos

    -> Solución 01:
        *Utilizamos '#pragma omp atomic', este será utilizado para que la varible sum no tenga condiciones de carrera

    -> Solución 02:
        *Utilizamos '#pragma omp parallel for reduction(+:sum)', este seŕa utilizado para ejecutar el bucle 'for' en paralelo, sumandose al final de las iteraciones, con ello evitamos los problemas de concurrencia

## Comparación de resultados

### Tamaño vector 50
    
| Tipo de Solución                   | Número de Hilos  |         Directivas       | Tiempo de Ejecución |          Observaciones                                        |
|------------------------------------|------------------|--------------------------|---------------------|---------------------------------------------------------------|
| Secuencial                         | 1                | Sin optimizaciones       | 0.000004            | Tiempo Inicial de partida                                     |
| Paralelización summavector_00.cpp  | 2                | `#pragma omp for`        | 0.000002            | Mejora rendimiento respecto a el secuencial                   |
| Paralelización summavector_01.cpp  | 2                | `#pragma omp atomic`     | 0.000003            | Evita condiciones de carrera entre las variables compartidas  |
| Paralelización summavector_02.cpp  | 2                | `#pragma omp reduction`  | 0.000132            | Reduce tiempo desde el punto de partida                       |
| Paralelización summavector_02.cpp  | 4                | `#pragma omp reduction`  | 0.000074            | Mejora rendimiento, pero se nota la sobrecarga de hilos       |
| Paralelización summavector_02.cpp  | 8                | `#pragma omp reduction`  | 0.000105            | Supone una  sobrecarga al aumentar tanto los hilos            |

*/* COMANDO MODIFICAR HILOS: export OMP_NUM_THREADS=8
### Tamaño vector 20

| Tipo de Solución                   | Número de Hilos  |         Directivas       | Tiempo de Ejecución |          Observaciones                                      |
|------------------------------------|------------------|--------------------------|---------------------|-------------------------------------------------------------|
| Secuencial                         | 1                | Sin optimizaciones       | 0.000003            | Tiempo Inicial de partida                                   |
| Paralelización summavector_00.cpp  | 2                | `#pragma omp for`        | 0.000001            | Mejora rendimiento respecto a el secuencial                 |
| Paralelización summavector_01.cpp  | 2                | `#pragma omp atomic`     | 0.000001            | Evita condiciones de carrera entre las variables compartidas|
| Paralelización summavector_02.cpp  | 2                | `#pragma omp reduction`  | 0.000207            | Reduce tiempo desde el punto de partida                     |
| Paralelización summavector_02.cpp  | 4                | `#pragma omp reduction`  | 0.000345            | Mejora el rendimimiento al aumentar número de hilos         |
| Paralelización summavector_02.cpp  | 8                | `#pragma omp reduction`  | 0.058908            | Supone una  sobrecarga al aumentar tanto los hilos          |

### Tamaño vector 10

| Tipo de Solución                   | Número de Hilos  |         Directivas       | Tiempo de Ejecución |          Observaciones                                                   |
|------------------------------------|------------------|--------------------------|---------------------|--------------------------------------------------------------------------|
| Secuencial                         | 1                | Sin optimizaciones       | 0.000003            | Tiempo Inicial de partida                                                |
| Paralelización summavector_00.cpp  | 2                | `#pragma omp for`        | 0.000001            | Muy rápido gracias a la distribución entre hilos                         |
| Paralelización summavector_01.cpp  | 2                | `#pragma omp atomic`     | 0.000207            | Evita condiciones de carrera entre las variables compartidas             |
| Paralelización summavector_02.cpp  | 2                | `#pragma omp reduction`  | 0.000185            | Nos muestra una sobrecarga para este tamaño pequeño                      |
| Paralelización summavector_02.cpp  | 4                | `#pragma omp reduction`  | 0.000504            | Aumenta la sobrecarga al aumentar los hilos                              |
| Paralelización summavector_02.cpp  | 8                | `#pragma omp reduction`  | 0.032741            | Realizamos un uso inadecuado/excesivo de los hilos, provocando sobrecarga|

## Explicación

    ->`#pragma omp for`:Distibuye el trabajo entre los distintos hilos evitando la sobrecarga, en ella comprobamos con las distitntas tablas de distintos tamaños como este tipo de códigos recursivos funcionan mejor con tamaños pequeños.

    ->`#pragma omp atomic`: Es util para evitar las condiciones de carrera( 2 procesos acceden a una misma variable a la vez), comprobamos que aunque sea una bajada ligera, este tipo de paralelización funciana mejor en tamaños pequeños, dado que aunque su bajada de tiempo de ejecución haya sido mínima, notamos ese cambio.

    ->`#pragma omp reduction`: Comprobamos como combinando los resultados obtenido por cada hilo seǵun hayamos elegido los hilos, en este caso 2, 4, 8, vemos como con un número adecuado/moderado de hilos su funcionamiento es óptimo, pero con una sobrecarga de hilos (8) vemos como puede llegar a afectar de forma negativa, sobre todo para tamaños pequeños.

