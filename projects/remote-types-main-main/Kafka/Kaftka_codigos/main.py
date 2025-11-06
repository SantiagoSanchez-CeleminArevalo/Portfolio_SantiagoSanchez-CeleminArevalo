import multiprocessing
import sys
from Kaftka_codigos.consumer import recibir_mensajes
import Ice # type: ignore
Ice.loadSlice("Kaftka_codigos/remotetypes.ice")
import RemoteTypes as rt  # type: ignore
from RemoteTypes import FactoryPrx, RSetPrx, RListPrx, RDictPrx # type: ignore

def Procesar_todas_clases(kafka_topic_in, kafka_topic_out, kafka_server, ice_proxy):
    " Procesa todas las operaciones"
    with Ice.initialize() as communicator:
        try:
            proxy = communicator.stringToProxy(ice_proxy)
            factory_service = FactoryPrx.checkedCast(proxy)
            if not factory_service:
                raise RuntimeError("Proxy no válido")
        except Exception as e:
            print(f"Error al inicializar el proxy: {e}")
            return
        print("--------------------------------------------------------------")
        print("Servidor Iniciado en el servidor:", kafka_server)
        print("Servidor Iniciado en el topic de entrada: ", kafka_topic_in)
        print("La salida del procesamiento se espera en:", kafka_topic_out)
        print("--------------------------------------------------------------")  
        print("Procesando mensajes...")
        print("--------------------------------------------------------------")
        for operation in recibir_mensajes(kafka_topic_in, kafka_server,factory_service, kafka_topic_out):
            print(f"Operación recibida: {operation}")
def main():
    "Función principal, inicializa las clases y procesa las operaciones"
    if len(sys.argv) < 4:
        print("Uso: Kaftka-client <proxy> <kafka_topic_in> <kafka_topic_out> [kafka_server](opcional, si no lo pone se pondrá localhost:9092)")
        sys.exit(1)

    proxy = sys.argv[1]
    topic_in = sys.argv[2]
    topic_out = sys.argv[3]
    server = sys.argv[4] if len(sys.argv) > 4 else "localhost:9092"

    proceso = multiprocessing.Process(target=Procesar_todas_clases, args=(topic_in,topic_out,server,proxy))
    proceso.start()
    proceso.join()

if __name__ == "__main__":
    main()
