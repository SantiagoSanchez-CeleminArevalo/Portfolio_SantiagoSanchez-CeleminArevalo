# *********************************************************************
# * Class Name: Descripcion_tablero
# * Author/s name: Santiago Sánchez-Celemín Arévalo
# * Release/Creation date: 16/10/2024
# * Class description: Esta clase proporciona una descripción del tablero de juego e imprime las características clave, como el tamaño, la posición de muros, objetivos, el jugador y las cajas.
# *
# *********************************************************************
class Descripcion_tablero:

    # *********************************************************************
    # * Method name: mostrar_descripcion
    # *
    # * Description of the Method: Imprime una descripción detallada del tablero, incluyendo el ID del tablero, tamaño, muros, objetivos, posición del jugador y posición de las cajas.
    # *
    # * Calling arguments:
    # *     md5 (str): ID único del tablero en formato MD5.
    # *     fil (int): Cantidad de filas en el tablero.
    # *     col (int): Cantidad de columnas en el tablero.
    # *     lim (list[tuple]): Lista de tuplas que representan las posiciones de los muros. objetivo (list[tuple]): Lista de tuplas que representan las posiciones de los objetivos.
    # *     jugador (tuple): Posición del jugador en el tablero.
    # *     cajas (list[tuple]): Lista de tuplas que representan las posiciones de las cajas.
    # *
    # * Return value: None
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************
    def mostrar_descripcion(md5, fil,col,lim,objetivo,jugador,cajas):
        Walls_String = str(lim).replace(" ", "")
        Targets_String = str(objetivo).replace(" ", "")
        Player_String = str(jugador).replace(" ", "")
        Boxes_String = str(cajas).replace(" ", "")
        print(f"ID:{md5}") #Imprimimos el id
        print(f"        Rows:{fil}")#Imprimimos la cantidad de filas
        print(f"        Columns:{col}")#imprimimos la cantidad de columnas
        print(f"        Walls:{Walls_String}")#Imprimimos la posicion de los muros
        print(f"        Targets:{Targets_String}")# Imprimimos la posicion  de los objetivos
        print(f"        Player:{Player_String}") #Imprimimos la posicion del jugador
        print(f"        Boxes:{Boxes_String}")#Imprimimos la posicion del objetivo