from Tablero import Tablero

# *********************************************************************
# * Class Name: Sucesores
# *
# * Author/s name: Santiago Sánchez-Celemín Arévalo
# *
# * Release/Creation date: 6/10/2024
# *
# * Class description: Esta clase proporciona métodos para generar los sucesores  de un estado dado en el juego Sokoban. También verifica  si todas las cajas están en posiciones objetivo.
# *********************************************************************
class Sucesores:

    # *********************************************************************
    # * Method name: generar_sucesores
    # *
    # * Description of the Method: Genera los posibles movimientos sucesores para el jugador y las cajas en el tablero.  Considera los movimientos hacia arriba, derecha, 
    # *                             abajo e izquierda, y determina si el movimiento es válido o si involucra mover una caja.
    # *
    # * Calling arguments:
    # *                   jugador (x,y): La posición actual del jugador (fila, columna).
    # *                   cajas (array): Lista de posiciones de cajas en el tablero.
    # *                   limites (array): Lista de posiciones de límites (paredes).
    # *
    # * Return value: array - Lista de sucesores, donde cada sucesor es una tupla: (dirección, (nueva posición del jugador, posiciones de cajas actualizadas), costo)
    # *
    # * Required Files: Tablero.py (para el método de validación de posiciones)
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************

    def generar_sucesores(jugador, cajas, limites):
        sucesores = []
        movimientos = [(-1,0),(0,1),(1,0),(0,-1)]
        direccion_movimiento = ['u', 'r', 'd', 'l']
        
        for i in range(4):
            # Calculamos la posición del jugador tras los distintos movimientos
            pos_x_jug = jugador[0] + movimientos[i][0]
            pos_y_jug = jugador[1] + movimientos[i][1]
            nuevo_mov_jug = (pos_x_jug, pos_y_jug)
            #Comprobamos que nuestro jugador no se choca con una pared o esta encima de una caja
            if Tablero.Posicion_valida(nuevo_mov_jug, limites, cajas):
                sucesores.append((direccion_movimiento[i], nuevo_mov_jug, cajas, 1))
            #SI el jugador está en una caja, movemos la caja
            elif nuevo_mov_jug in cajas:
                #Calculamos la nueva posicion de la caja en función de la posicón del jugador
                pos_x_caj = pos_x_jug + movimientos[i][0]
                pos_y_caj = pos_y_jug + movimientos[i][1]
                nuevo_mov_caja = (pos_x_caj, pos_y_caj)
                #Comprobamos si la posición de la caja es valida
                if Tablero.Posicion_valida(nuevo_mov_caja, limites, cajas):
                    cajas_copia = cajas[:]
                    cajas_copia.remove(nuevo_mov_jug)#Eliminamos la poscion de la caja movida
                    cajas_copia.append(nuevo_mov_caja)#Añadimos la nueva poscion de la caja
                    sucesores.append((direccion_movimiento[i].upper(), nuevo_mov_jug, sorted(cajas_copia), 1))
        return sucesores
    
    # *********************************************************************
    # * Method name: Cajas_en_objetivos
    # *
    # * Description of the Method: Verifica si todas las cajas están en las posiciones de objetivos en el tablero.
    # *
    # * Calling arguments:
    # *                 cajas (array): Lista de posiciones actuales de las cajas.
    # *                 objetivos (array): Lista de posiciones de los objetivos.
    # *
    # * Return value: boolean - True si todas las cajas están en posiciones de objetivos, False en caso contrario.
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************

    def Cajas_en_objetivos(cajas,objetivos):
        for caj in cajas:#Recorremos cada una de las cajas
            Esta_en_obj = False 
            for obj in objetivos: #iteramos cada caja sobre caja objetivo
                if Sucesores.en_objetivo(caj, obj):
                    Esta_en_obj = True
                    break  # Salimos del bucle si encontramos la caja en algún objetivo
            if not Esta_en_obj:
                return False  # Si no encontramos la caja en ningún objetivo, retornamos False
        return True  # Si todas las cajas están en al menos un objetivo, retornamos True


# *********************************************************************
# * Function name: en_objetivo
# *
# * Description of the Function: Verifica si una caja está en una posición objetivo.
# *
# * Calling arguments:
# *             caja (array): Posición de la caja.
# *             objetivo (array): Posición del objetivo.
# *
# * Return value: bool - True si la caja está en la posición del objetivo, False en caso contrario.
# *
# * Required Files: Ninguno
# *
# * List of Checked Exceptions: Ninguno
# *********************************************************************
    def en_objetivo(caja, objetivo):
    # Comprobamos si la caja está en el mismo objetivo
        valido = set(caja) == set(objetivo)
        return valido

