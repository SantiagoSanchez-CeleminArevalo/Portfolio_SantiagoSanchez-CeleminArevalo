
# *********************************************************************
# * Class Name: Tablero
# *
# * Author/s name: Santiago Sánchez-Celemín Arévalo
# *
# * Release/Creation date: 6/10/2024
# *
# * Class description: Procesa la cadena de entrada para identificar el tamaño del tablero, las posiciones de límites, objetivos, el jugador 
# *                     y las cajas, y proporciona métodos de validación para asegurar que el tablero esté correctamente configurado.
# *
# *********************************************************************
class Tablero:

    # *********************************************************************
    # * Method name: __init__
    # *
    # * Description of the Method: Constructor de la clase. Inicializa el tablero y procesa la cadena de entrada para extraer las posiciones de límites, objetivos, jugador y cajas.
    # *
    # * Calling arguments: cadena (str): El mapa del tablero representado como una cadena.
    # *
    # * Return value: None
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************
    def __init__(self, cadena):
        self.fil, self.col, self.limites, self.objetivos, self.jugador, self.cajas = self.descifrar_cadena(cadena)

    # *********************************************************************
    # * Method name: descifrar_cadena
    # *
    # * Description of the Method: Procesa la cadena del tablero para obtener  las posiciones de filas, columnas, límites, objetivos, jugador y cajas en el tablero.
    # *
    # * Calling arguments: cadena (str): El mapa del tablero representado como una cadena.
    # *
    # * Return value: Tuple (fil, col, limites, objetivos, jugador, cajas)
    # *               - fil (int): Número de filas en el tablero
    # *               - col (int): Número máximo de columnas en el tablero
    # *               - limites (array): Lista de posiciones de paredes
    # *               - objetivos (array): Lista de posiciones de objetivos
    # *               - jugador (x,y): Posición del jugador
    # *               - cajas (array): Lista de posiciones de cajas
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions:  - ValueError: si encuentra un caracter inválido en el mapa.
    # *********************************************************************

    def descifrar_cadena(self, cadena):
        lin = cadena.split("\\n")  # Cada línea del mapa es un elemento en lin
        fil = len(lin)  # Número de filas
        col = max(len(linea) for linea in lin)  # Número máximo de columnas
        
        # Inicializamos las variables
        limites = []
        objetivos = []
        jugador = None
        cajas = []

        for fila, linea in enumerate(lin):
            for columna, caracter in enumerate(linea):  # Itera sobre cada columna en la fila
                pos = (fila, columna)
                
                # Clasifica cada caracter
                if caracter in {'#', '.', '@', '$', '+', '*', ' '}:
                    if caracter == '#':  # Es Pared
                        limites.append(pos)
                    elif caracter == '.':  # Es Objetivo
                        objetivos.append(pos)
                    elif caracter == '@':  # Es Jugador
                        jugador = pos
                    elif caracter == '$':  # Es Caja
                        cajas.append(pos)
                    elif caracter == '+':  # Es Jugador sobre objetivo
                        jugador = pos
                        objetivos.append(pos)
                    elif caracter == '*':  # Caja sobre objetivo
                        cajas.append(pos)
                        objetivos.append(pos)
                else:
                    raise ValueError(f"Caracter inválido en el mapa: {caracter}")

        # Comprobamos que el tablero tenga jugador, cajas y objetivos
        self.control_errores_tablero(jugador, cajas, objetivos)

        return fil, col, limites, objetivos, jugador, cajas
    
    # *********************************************************************
    # * Method name: control_errores_tablero
    # *
    # * Description of the Method: Verifica que el tablero tenga un jugador, al menos una caja, y al menos un objetivo.  Si falta alguno de estos elementos, lanza un error.
    # *
    # * Calling arguments:
    # *                     jugador (x,y): Posición del jugador.
    # *                     cajas (array): Lista de posiciones de cajas.
    # *                     objetivos (array): Lista de posiciones de objetivos.
    # *
    # * Return value: None
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions:
    # *                      - ValueError: si falta el jugador, las cajas o los objetivos en el tablero.
    # *********************************************************************

    def control_errores_tablero(self, jugador, cajas, objetivos):
        if jugador is None:  # Verifica que haya un jugador
            raise ValueError("Error: Este nivel no tiene un jugador")
        if not cajas:  # Verifica que haya cajas
            raise ValueError("Error: Este nivel no tiene cajas")
        if not objetivos:  # Verifica que haya objetivos
            raise ValueError("Error: Este nivel no tiene objetivos")
        
    # *********************************************************************
    # * Method name: Posicion_valida
    # *
    # * Description of the Method: Comprueba si una posición es válida, es decir, que no coincida con la posición de una pared ni  de una caja.
    # *
    # * Calling arguments:
    # *                    pos (x,y): Posición a verificar (fila, columna).
    # *                    lim (array): Lista de posiciones de límites.
    # *                    cajas (array): Lista de posiciones de cajas.
    # *
    # * Return value: bool - True si la posición es válida, False si no lo es.
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************
    
    def Posicion_valida(pos,lim,cajas):
        Pos_val = pos not in lim and pos not in cajas# Comporbamos si la posicioones es una pared o es una caja
        return Pos_val

    
    

            
