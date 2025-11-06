# *********************************************************************
# * Class Name: IdGenerador
# * Author/s name: Santiago Sánchez-Celemín Arévalo
# * Release/Creation date: 6/11/2024
# * Class description: Codifica en md5 la tupla formada por el jugador y las cajas
# *
# *********************************************************************

class Manhatan:

    # *********************************************************************
    # * Method name: calculo_Manhattan_total
    # *
    # * Description of the Method: Genera el cáculo Manhatan para la heurística
    # *
    # * Calling arguments: cadena (array): Posciones_actuales, pociciones_destino
    # *
    # * Return value: float, del cálculo manhatan
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************

    def calculo_Manhattan_total(posiciones_actuales, posiciones_destino,estrategia):
        if estrategia == 'GREEDY' or estrategia == 'A*':
            distancia_suma = 0
            for c in posiciones_actuales:
                distancias = []
                for ob in posiciones_destino:
                    distancia = abs(c[0] - ob[0]) + abs(c[1] - ob[1]) #Formula campus
                    distancias.append(distancia)
                distancia_minima = min(distancias) #cojo distancia mínima
                distancia_suma += distancia_minima  #sumo todas las distnacias obtenidas
        else:
            distancia_suma = 0
        
        return distancia_suma



        
        
