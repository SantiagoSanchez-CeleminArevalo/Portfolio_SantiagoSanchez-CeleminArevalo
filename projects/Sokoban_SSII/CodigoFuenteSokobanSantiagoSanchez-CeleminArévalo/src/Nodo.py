from IDGenerador import IDGenerador
from decimal import Decimal, ROUND_HALF_UP

# *********************************************************************
# * Class Name: Nodo
# * Author/s name: Santiago Sánchez-Celemín Arévalo
# * Release/Creation date: 6/11/2024
# * Class description: En esta clase elaboraremos los nodos pertinenetes para la tarea T3, en la que estrá formado
#                       por una seria de atributos para formar ese nodo
# *
# *********************************************************************

class Nodo:

    # *********************************************************************
    # * Method name: __init__
    # * Name of the original author: Santiago Sánchez-Celemín Arévalo
    # *
    # * Description of the Method: Inicializa una instancia de la clase Nodo, configurando los atributos del nodo.
    # * Calling arguments:
    # *     id_nodo (int): Identificador único del nodo.
    # *     estado (tuple): Estado actual del nodo.
    # *     padre (Nodo): Nodo padre.
    # *     accion (str): Acción que llevó al nodo a su estado actual.
    # *     profundidad (int): Profundidad del nodo en el árbol de búsqueda.
    # *     costo (float): Costo acumulado hasta el nodo actual.
    # *     heuristica (float): Heurística calculada para el nodo.
    # *     valor (float): Valor utilizado para la estrategia de búsqueda.
    # *
    # * Return value: None
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************
    def __init__(self, id_nodo, estado, padre, accion, profundidad, costo, heuristica, valor):
        self.id_nodo = id_nodo
        self.estado = estado
        self.padre = padre
        self.accion = accion
        self.profundidad = profundidad
        self.costo = costo
        self.heuristica = heuristica
        self.valor = valor


    # *********************************************************************
    # * Method name: seleccionar_estrategia
    # *
    # * Description of the Method: Calcula un valor de selección basado en la estrategia de búsqueda especificada.
    # *
    # * Calling arguments: estrategia (str): Tipo de estrategia ('BFS', 'DFS', 'UC', o otro).
    # *
    # * Return value: float en dos decimales, siendo el valor calculado basado en la estrategia.
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************
    def seleccionar_estrategia(self, estrategia):
        if estrategia == 'BFS':
            val = self.profundidad
        elif estrategia == 'DFS':
            val = 1 / (self.profundidad + 1)
        elif estrategia == 'UC':
            val = self.costo
        elif estrategia == 'GREEDY':
            val = self.heuristica
        elif estrategia == 'A*':
            val = self.heuristica + self.costo

        return val

    # *********************************************************************
    # * Method name: Print_estado
    # *
    # * Description of the Method: Retorna el estado del nodo, para la generación del Id.
    # *
    # * Calling arguments: Ninguno
    # *
    # * Return value: tuple, el estado del nodo.
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************
    def Print_estado(self):
        impresion_Estado = (self.estado)
        return impresion_Estado
    

    # *********************************************************************
    # * Method name: Print_solucion
    # *
    # * Description of the Method: Imprime una representación detallada del nodo, incluyendo su ID, estado, ID del nodo padre, y otros atributos.
    # *
    # * Calling arguments: Ninguno
    # *
    # * Return value: None
    # *
    # * Required Files: IDGenerador (para generar IDs de estado)
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************
    
    

    def Print_solucion(self):
        jug, caj = self.estado
        estado_str = f"{jug}{str(caj).replace(' ', '')}"
        Id = IDGenerador.generar_id(estado_str)  # Genera el Identificador
        Id_nodo_padre = None
        if self.padre is not None:
            Id_nodo_padre = self.padre.id_nodo  # Recoge el Identificador del nodo padre

        # Usamos Decimal para asegurar el redondeo a la alta
        
        costo_redondeado = Decimal(self.costo).quantize(Decimal('0.01'), rounding=ROUND_HALF_UP)
        heuristica_redondeada = Decimal(self.heuristica).quantize(Decimal('0.01'), rounding=ROUND_HALF_UP)
        valor_redondeado = Decimal(self.valor).quantize(Decimal('0.01'), rounding=ROUND_HALF_UP)
        print(f"{self.id_nodo},{Id},{Id_nodo_padre},{self.accion},{self.profundidad},{costo_redondeado:.2f},{heuristica_redondeada:.2f},{valor_redondeado:.2f}")


    
    # *********************************************************************
    # * Method name: __lt__
    # *
    # * Description of the Method: Define el operador < para comparar dos nodos en función de sus valores y IDs.
    # *
    # * Calling arguments: otro (Nodo): Otro nodo con el cual se compara el nodo actual.
    # *
    # * Return value: bool, True si el nodo actual es menor que 'otro', de acuerdo a los valores de comparación definidos.
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************
    def __lt__(self, otro):
        if self.valor == otro.valor:
            return self.id_nodo < otro.id_nodo
        else:
            return self.valor < otro.valor
        
            
            

