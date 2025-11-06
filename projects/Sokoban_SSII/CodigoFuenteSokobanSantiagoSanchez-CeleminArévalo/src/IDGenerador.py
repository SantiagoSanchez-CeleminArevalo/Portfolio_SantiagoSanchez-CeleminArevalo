
import hashlib

# *********************************************************************
# * Class Name: IdGenerador
# * Author/s name: Santiago Sánchez-Celemín Arévalo
# * Release/Creation date: 6/11/2024
# * Class description: Codifica en md5 la tupla formada por el jugador y las cajas
# *
# *********************************************************************

class IDGenerador:

    # *********************************************************************
    # * Method name: generar_id
    # *
    # * Description of the Method: Genera un hash MD5 único a partir de una cadena dada y lo convierte en mayúsculas.
    # *
    # * Calling arguments: cadena (str): La cadena de entrada para la que se generará el ID.
    # *
    # * Return value: str, el hash MD5 de la cadena en formato hexadecimal y en mayúsculas.
    # *
    # * Required Files: Ninguno
    # *
    # * List of Checked Exceptions: Ninguno
    # *********************************************************************

    def generar_id(cadena):
        cadena = cadena.replace(" ","")
        has = hashlib.md5(cadena.encode()).hexdigest().upper()# Codificaión en md5
        return has
    