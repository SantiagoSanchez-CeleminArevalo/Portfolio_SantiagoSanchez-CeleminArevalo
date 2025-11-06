import json
from confluent_kafka import Consumer, KafkaException, KafkaError  # type: ignore
from Kaftka_codigos.client import recibir_cada_operacion
from Kaftka_codigos.publisher import Enviar_mensaje

def recibir_mensajes(kafka_topic_in, kafka_server, factory_service, kafka_topic_out):
    """Recibe mensajes del topic de Kafka"""
    consumer = Consumer({
        'bootstrap.servers': kafka_server,
        'group.id': 'client-group',
        'auto.offset.reset': 'earliest'
    })
    consumer.subscribe([kafka_topic_in])

    try:
        while True:
            msg = consumer.poll(timeout=1.0)
            if msg is None:
                continue
            if msg.error():
                if msg.error().code() == KafkaError._PARTITION_EOF:
                    print(f"Se alcanzó el final de la partición {msg.partition()}/{msg.offset()}")
                else:
                    raise KafkaException(msg.error())
            else:
                try:
                    operations = json.loads(msg.value().decode('utf-8'))
                    print("Operaciones recibidas: ", operations)
                    print("--------------------------------------------------------------")

                    if not isinstance(operations, list):
                        print("El mensaje recibido no es una lista.")
                        continue

                    respuestas = []
                    for operation in operations:
                        print(f"Operación en forma individual recibida: {operation}")
                        response = recibir_cada_operacion(factory_service, operation)
                        print(f"Respuesta: {response}")
                        print("**************************************************************")
                        respuestas.append(response)

                    print("--------------------------------------------------------------")
                    print("Enviando respuesta final en forma de lista...")
                    print("--------------------------------------------------------------")
                    Enviar_mensaje(kafka_topic_out, kafka_server, respuestas)

                except json.JSONDecodeError as jde:
                    print(f"El mensaje recibido no es un JSON válido: {jde}")
                except Exception as e:
                    print(f"Error procesando las operaciones: {e}")

    except Exception as e:
        print(f"Error en el consumidor: {e}")
    finally:
        consumer.close()
