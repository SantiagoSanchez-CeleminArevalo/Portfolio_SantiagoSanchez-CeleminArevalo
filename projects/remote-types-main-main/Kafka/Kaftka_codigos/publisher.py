
import json
from confluent_kafka import Producer # type: ignore

def Enviar_mensaje(kafka_topic_out, kafka_server, message):
    "Envía un mensaje al topic de Kafka"
    producer = Producer({'bootstrap.servers': kafka_server})
    try:
        producer.produce(kafka_topic_out, json.dumps(message).encode('utf-8'))
        producer.flush()
        print(f"Mensaje enviado a {kafka_topic_out}: {message}")
        print("------------------------------------------------")
        print("\n")

    except Exception as e:
        print(f"Error enviando mensaje a Kafka: {e}")

