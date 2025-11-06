/* Programa secuencial que calcula la suma de los elementos de un vector v[i] 
 * y la almacena en la variable sum */ 

#include <stdio.h> 
#include <stdlib.h> 
#include <time.h> 
#define N 20 // Probar distintos tamaños de vector 
#include <omp.h>

void main() 
{ 
    int i, sum = 0; 
    int v[N];    


    srand (time(NULL));
    for (i = 0; i < N; i++) v[i] = rand()%100; 
    clock_t inicio = clock();
    {

        int suma = 0;
        for (i = 0; i < N; i++) suma+= v[i];   
        #pragma omp atomic
        sum += suma;        
    }
    clock_t fin = clock();
    double tiempo = ((double)(fin - inicio)/CLOCKS_PER_SEC); 

    printf("\nVector de números: \n "); 
    for (i = 0; i < N; i++) printf("%d \t",v[i]);
    
	printf("\n La suma es: %d \n\n", sum); 
    printf("El tiempo que ha tardado en ejecutarse ha sido: %f\n",tiempo);
}