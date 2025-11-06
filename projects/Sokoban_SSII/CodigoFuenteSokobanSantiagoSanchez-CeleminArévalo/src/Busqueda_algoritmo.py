from Nodo import Nodo
from Sucesores import Sucesores
from IDGenerador import IDGenerador
import heapq
import Manhatan as man

# *********************************************************************
# * Class Name: Nodo
# * Author/s name: Santiago Sánchez-Celemín Arévalo
# * Release/Creation date: 6/11/2024
# * Class description: Implementa un algoritmo de búsqueda que expande nodos y determina la solución al problema del tablero si existe.
# *
# *********************************************************************
class Busqueda_algoritmo:

     # *********************************************************************
    # * Method name: algoritmo
    # *
    # * Description of the Method: Implementa un algoritmo de búsqueda que explora los posibles estados del tablero hasta encontrar
    # *                             una solución que coloque todas las cajas en sus objetivos.
    # *
    # * Calling arguments:
    # *                     estado_inicial (tuple): Estado inicial del tablero, con la posición del jugador y las cajas.
    # *                     estrategia (str): Estrategia de búsqueda ('BFS', 'DFS', 'UC', etc.).
    # *                     profundidad (int): Profundidad máxima permitida para la búsqueda.
    # *                     tablero (Tablero): Instancia de la clase Tablero con los límites y objetivos.
    # *
    # * Return value: Nodo, el nodo final que representa la solución si se encontró, o None si no se encontro una solución.
    # *
    # * Required Files: Nodo, Sucesores, Tablero, IDGenerador
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************

    def algoritmo(estado_inicial, estrategia, profundidad, tablero):
        id = 0
        Nodos_visitados = set()
        Nodos_frontera = []
        heapq.heapify(Nodos_frontera)
        solucion_algortimo = False
        nodo_final = None
        
        # Generamos el nodo Padre
        nodo_Padre = Nodo(id, estado_inicial, None, 'NOTHING', 0, 0, 0, 0)
        nodo_Padre.heuristica = man.Manhatan.calculo_Manhattan_total(estado_inicial[1],tablero.objetivos,estrategia)
        nodo_Padre.valor = nodo_Padre.seleccionar_estrategia(estrategia)
        heapq.heappush(Nodos_frontera, (nodo_Padre.valor, nodo_Padre.id_nodo, nodo_Padre)) 

        while Nodos_frontera and not solucion_algortimo:
            # Aqui podriamos contar los nodos expandidos
            #Extraemos el nodo de la frontera con valor mas bajo o Id más bajo
            nodo_Padre = heapq.heappop(Nodos_frontera)[2]
            
            if all(caja in tablero.objetivos for caja in nodo_Padre.estado[1]):  # Comprobamos si si todas las cajas están en todos los objetivos
                solucion_algortimo = True
                nodo_final = nodo_Padre  # Guardamos el último nodo
            else:
                estado_vis_codificado = IDGenerador.generar_id(f"{nodo_Padre.Print_estado()}")
            
                if nodo_Padre.profundidad < profundidad and estado_vis_codificado not in Nodos_visitados:
                    Nodos_visitados.add(estado_vis_codificado)
                    # Generamos sucesores de cada estado generado anteriormnetne
                    for mov, estado_proximo_jug, estado_proximo_caj, costo in Sucesores.generar_sucesores(nodo_Padre.estado[0], nodo_Padre.estado[1], tablero.limites):
                        # Generamos un nodo hijo
                        id += 1
                        nodo_Hijo = Nodo(id, (estado_proximo_jug, estado_proximo_caj), nodo_Padre, mov, nodo_Padre.profundidad + 1, nodo_Padre.costo + costo, 0, 0)
                        nodo_Hijo.heuristica = man.Manhatan.calculo_Manhattan_total(estado_proximo_caj,tablero.objetivos,estrategia)
                        nodo_Hijo.valor = nodo_Hijo.seleccionar_estrategia(estrategia)
                        #Imprime los sucesores del nodo padre 28
                        #if  (nodo_Padre.id_nodo == 8):
                            #print(nodo_Hijo.Print_solucion())
                        heapq.heappush(Nodos_frontera, (nodo_Hijo.valor, nodo_Hijo.id_nodo, nodo_Hijo)) 
                         # Aqui podriamos contar los nodos que han pasado por la frontera
                #else:
                    #nodos_podados +=1 

        # Almacenamos el último nodo
        if solucion_algortimo:
            return nodo_final
        else:
            return None

        



