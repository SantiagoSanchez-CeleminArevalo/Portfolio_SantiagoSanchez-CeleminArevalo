/* Programa calculo minimo y máximo*/ 
#include <stdio.h> 
#include <stdlib.h> 
#include <time.h> 
#include <omp.h>
#define N 19000
int main() {
    int x;
    int vector[N];
    int maximo=0;
    int minimo = 1000;

    srand(time(NULL));
    for(x = 0; x < N; x++){
        vector[x] = rand();    
    }

    #pragma omp parallel for reduction(max:maximo) reduction(min:minimo)
    for (x = 0; x< N;x++){
        if(vector[x]> maximo){
            maximo = vector[x];
        }
        if(vector[x]< minimo){
            minimo = vector[x];
        }
    }
    printf("El valor máximo es: %d\n", maximo);
    printf("El valor mínimo es: %d\n", minimo);
    return 0;
}