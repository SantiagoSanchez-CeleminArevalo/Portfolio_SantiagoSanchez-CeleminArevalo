# Resolución Ejercicio 3
    -> Para la resolución de este ejercicio partimos de el ejecutable pi.cpp
    -> Para compilar dicho ejercicio utilizamos gcc -x c -fopenmp  pi.cpp -o pi.out
    -> Formato Secuencial

** Soluciones propuestas
 ## Solución 1 Suma Local y Atomic
    -> Cada hilo ejecuta su trbajo, posteriormente suma su resultado a la variable 'sum'
    -> Si muchos hilos intentan esta Técnica puede suponer un 'cuello de botella'
## Solución 2 Reducción (#pragma parallel for reduction(+:sum) private(xi))
    -> Genera sumas locales para cada hilo y luego las suma al terminar la operación
    -> primitiva (private): Cada hilo tiene una copia del elemento, este caso 'xi'
    -> Evita la sobrecarga
## Comparación
| Tipo de Solución                   |                     Directivas                                | Precisión|     Error         |   Tiempo    |
|------------------------------------|---------------------------------------------------------------|----------|-------------------|-------------|
| Secuencial pi.out                  | Sin optimizar                                                 | 1000000  |0.0000000000013656 |0.0054147760 |
| Secuencial pi.out                  | Sin optimizar                                                 | 5042316  |0.0000000000439249 |0.0159229840 |
| Paralelización pi_01.out           |`Suma Local y Atomic`                                          | 55555555 |0.0000000000000560 |0.9879340480 |
| Paralelización pi_01.out           |`Suma Local y Atomic`                                          | 123654975|0.0000000000000182 |2.1647886530 |
| Paralelización pi_02.out           |`Reducción (#pragma parallel for reduction(+:sum) private(xi))`| 1000000  |0.0000019999996348 |0.0065857290 |
| Paralelización pi_02.out           |`Reducción (#pragma parallel for reduction(+:sum) private(xi))`| 5042316  |0.0000003966870943 |0.0163795720 |

## Explicacion Tabla
    -> Secuencial pi.out: Vemos como a medida que el número de intervalos aumenta, nuestra eficiencia debido al timepo de ejecución disminuye

    ->Paralelización pi_01.out: Comprobamos como cuando aumentamos la precisión, su tiempo de ejecucción aumneta, lo que puede estar suponiendo cuellos de botella

    ->Paralelización pi_02.out:  Comprobamos como con intervalos de precisión más grandes, el timepo de ejecución se reduce, por lo que esta técnica pordría manejar una mayor carga de trabajo