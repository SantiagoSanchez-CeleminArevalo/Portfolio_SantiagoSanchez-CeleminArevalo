# Resolución Ejercicio 1
Para la resolución de este ejercicio hemos compilado así gcc -x c -fopenmp  secuencial_vector.cpp -o vector.out
## Solución posible Secuencial
    -> Esta posble implementación del método secuencial, recorre el vector hasta encontra el máximo y el mínimo de ese vector
## Solución posible 1( Suma local y Atomic)
    -> Esta implementación recorre el vector de forma paralela calculando los resultados localmente y luego combinandolos
## Solución posible 2(Reducción)
    ->Reduce la complejidad del algoritmo mediante la directiva reduction
## Comparación de resultados
* Para calcular el tiempo utilizamos time ./ejemplo.out

| Tipo de Solución                   | Directiva                                   | Número de Hilos  | Tiempo (segundos)   | Valor Máximo   | Valor Mínimo    |
|------------------------------------|---------------------------------------------|------------------|--------------------|--------------|---------------|
| Secuencial                         | Sin optimizar                               | 1                | 0.08               | 2147452090   | 187832        |
| Secuencial                         | Sin optimizar                               | 1                | 0.08               | 2147438584   | 47673         |
| Paralelización con `omp parallel`  | `#pragma omp parallel`                      | 2                | 0.02               | 2147156985   | 5             |
| Paralelización con `omp parallel`  | `#pragma omp parallel`                      | 2                | 0.03               | 2147394737   | 12            |
| Paralelización con `reduction`     | `#pragma omp reduction(max:max) reduction(min:min)` | 2        | 0.11               | 2147451488   | 1000          |
| Paralelización con `reduction`     | `#pragma omp reduction(max:max) reduction(min:min)` | 2        | 0.004              | 2147106324   | 1000          |

## Explicación tabla
    -> Secuencial (sin optimizaciones): Supone un bajo uso de los recursos dado que solo utiliza un hilo, a medida que el tamño del vector aumenta, el problema se ve afectado aumentando los tiempos de ejecución
    -> Paralelización con `omp parallel`: En compración de la ejecución secuencail (sin optimmizaciones) supone que el programa realice cálculos simultaneamente, lo que supone que acorte el tiempo total, por ello comprobamos como el tiempo ejecución supone una mejora significativa
    -> Paralelización con `reduction`: Esta ténica de optimización supone que cada uno de los hilos calcule su minimo y su máximo de manera independiente, para posteriormente, combianr los resutltados al final, esto supone un uos del programa más eficiente