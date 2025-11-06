from confluent_kafka import Producer  # type: ignore
import sys

def publish_test_message(kafka_topic, kafka_server, message):
    """Envía un mensaje a un topic de Kafka"""
    try:
        producer = Producer({'bootstrap.servers': kafka_server})
        producer.produce(kafka_topic, message.encode('utf-8'))
        producer.flush()
        print(f"Mensaje enviado al topic '{kafka_topic}': {message}")
    except Exception as e:
        print(f"Error enviando el mensaje: {e}")

def main():
    """Envía mensajes a un topic de Kafka"""
    if len(sys.argv) != 4:
        print("Uso: python3 test.py <kafka_topic> <kafka_server> <mensaje>")
        sys.exit(1)

    kafka_topic = sys.argv[1]
    kafka_server = sys.argv[2]
    message = sys.argv[3]

    try:
        publish_test_message(kafka_topic, kafka_server, message)
    except Exception as e:
        print(f"Error inesperado: {e}")

if __name__ == "__main__":
    main()
