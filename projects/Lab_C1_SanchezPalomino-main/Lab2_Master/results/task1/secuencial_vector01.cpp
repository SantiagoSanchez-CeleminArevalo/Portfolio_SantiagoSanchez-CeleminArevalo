/* Programa calculo minimo y máximo*/ 
#include <stdio.h> 
#include <stdlib.h> 
#include <time.h> 
#include <omp.h>
#define N 19000
int main() {
    int x;
    int vector[N];
    int maximo, minimo;

    srand(time(NULL));
    for(x = 0; x < N; x++){
        vector[x] = rand();    
    }

    maximo = 0;
    minimo = 1;
    #pragma omp paralel
    {
        int maximo_local = vector[0];
        int minimo_local = vector[0];
        #pragma omp for
        for (x = 0; x< N;x++){
            if(vector[x]> maximo_local){
                maximo_local = vector[x];
            }
            if(vector[x]< minimo_local){
                minimo_local = vector[x];
            }
        }
        #pragma omp critical
        {
            if (maximo_local > maximo){
                    maximo = maximo_local;
            }
            if(minimo_local < minimo){
                minimo = minimo_local;
            }
        }
    }
    printf("El valor máximo es: %d\n", maximo);
    printf("El valor mínimo es: %d\n", minimo);
    return 0;
}