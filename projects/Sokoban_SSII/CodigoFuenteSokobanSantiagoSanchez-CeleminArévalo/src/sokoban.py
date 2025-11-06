import sys
from Tablero import Tablero
from IDGenerador import IDGenerador
from Sucesores import Sucesores
from Descripcion_tablero import Descripcion_tablero
from Busqueda_algoritmo import Busqueda_algoritmo

# *********************************************************************
# * Function name: main
# *
# * Description of the Function: Punto de entrada principal para el programa Sokoban. Según el tipo de tarea especificado, realiza operaciones
# *                             para mostrar la descripción del tablero, generar sucesores, verificar si las cajas están en los objetivos, o inciia el algoritmo de para conocer la
# *                             solución a el camino más óptimo según la estrategia utilizada
# *
# * Calling arguments: Ninguno directamente (usa sys.argv para los argumentos de línea de comando).
# *
# * Return value: None
# *
# * Required Files: Sucesores, Descripcion_tablero, Busqueda_algoritmo, IDGenerador, Tablero
# *
# * List of Checked Exceptions:
# *                             - ValueError: si hay un error en el tablero
# *
# *********************************************************************
def main():
    cadena = sys.argv[3]
    tipo = sys.argv[1]
    try:
        tablero = Tablero(cadena)
        fil, col, limites, objetivos, jugador,cajas = tablero.fil,tablero.col, tablero.limites, tablero.objetivos,tablero.jugador, tablero.cajas
        cadena_ID = f"{jugador}{sorted(cajas)}"
        Id_mapa = IDGenerador.generar_id(cadena_ID)
        sucesores_activos = Sucesores.generar_sucesores(jugador,cajas,limites)
        if (tipo == 'T1'):
            if len(sys.argv) < 4:
                print("Utiliza el formato correcto: python3 sokoban.py <acción> <parámetros>")
                return
            Descripcion_tablero.mostrar_descripcion(Id_mapa, fil,col,limites,objetivos,jugador,cajas)
        elif (tipo == 'T2S'):
            if len(sys.argv) < 4:
                print("Utiliza el formato correcto: python3 sokoban.py <acción> <parámetros>")
                return  
            print(f"ID: {Id_mapa}")
            for s in sucesores_activos:
                gen_ID = f"{s[1]}{s[2]}"
                id_nuevo_T2S = IDGenerador.generar_id(gen_ID.replace(" ", ""))
                cadena_T2S = f"[{s[0]},{id_nuevo_T2S},{s[3]}]"
                print(f"        {cadena_T2S}")
        elif tipo == 'T2T':
            if len(sys.argv) < 4:
                print("Utiliza el formato correcto: python3 sokoban.py <acción> <parámetros>")
                return
            if Sucesores.Cajas_en_objetivos(cajas, objetivos):
                print('TRUE')
            else:
                print('FALSE')
        elif tipo == 'T3':
            profundidad = float(sys.argv[7])
            estrategia = sys.argv[5]
            if len(sys.argv) < 8:
                print("Utiliza el formato correcto: python3 sokoban.py <acción> <parámetros>")
                return
            solucion = Busqueda_algoritmo.algoritmo((jugador, cajas),estrategia,profundidad, tablero)
            solucion_algoritmo = []
            if solucion is None:
                print("No se ha encontrado solución")
            else:
                #Si enceuntra solucion:
                print(cadena.replace(" ",""))
                while (solucion.padre) is not None:
                    solucion_algoritmo.append(solucion)
                    solucion = solucion.padre
                solucion_algoritmo.append(solucion)
                lista = solucion_algoritmo[::-1] #Invertimos la lista ya que se alamcena primero el último nodo
                for s in lista:
                    s.Print_solucion()

    except ValueError as e:
        print(f"Error al intentar iniciar el Test: {e}")

if __name__ == "__main__":
    main()
