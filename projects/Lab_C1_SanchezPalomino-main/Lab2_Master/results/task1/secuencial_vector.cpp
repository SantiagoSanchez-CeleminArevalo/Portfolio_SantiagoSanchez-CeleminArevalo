/* Programa calculo minimo y máximo*/ 
#include <stdio.h> 
#include <stdlib.h> 
#include <time.h> 
#define N 19000
int main() {
    int x;
    int vector[N];
    int maximo, minimo;

    srand(time(NULL));
    for(x = 0; x < N; x++){
        vector[x] = rand();    
    }

    maximo = vector[0];
    minimo = vector[0];
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