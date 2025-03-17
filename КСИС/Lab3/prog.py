import socket
import threading
import time

PORT = 5000
TCP_PORT = 6000
BUFFER_SIZE = 1024
my_ip = "192.168.1.102"
user_list = set()  # Уникальные пользователи
continue_signal = True

# Функция для приема сообщений (UDP сервер)
def receive_messages():
    global continue_signal
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    server_socket.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    server_socket.bind((my_ip, PORT))
    while continue_signal:
        try:
            _, addr = server_socket.recvfrom(BUFFER_SIZE)
            user_ip = addr[0]
            if user_ip != my_ip and user_ip not in user_list:
                print(f"📩 Новый пользователь ({user_ip})")
                user_list.add(user_ip)
        except:
            break

# Функция для отправки сообщений и поиска новых узлов (UDP клиент)
def send_broadcast():
    global continue_signal
    client_socket = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    client_socket.setsockopt(socket.SOL_SOCKET, socket.SO_BROADCAST, 1)
    broadcast_address = (f"{my_ip[:my_ip.rfind('.')]+'.255'}", PORT)
    while continue_signal:
        try:
            client_socket.sendto("".encode(), broadcast_address)
            time.sleep(5)
        except:
            break
    client_socket.close()

# Функция для установки TCP соединений с каждым узлом
def establish_tcp_connections():
    global continue_signal
    while continue_signal:
        for user_ip in list(user_list):
            try:
                client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
                client_socket.connect((user_ip, TCP_PORT))
                message = input()
                if message == "0":
                    continue_signal = False
                    message = "Выхожу из чата"
                client_socket.sendall(message.encode())
                client_socket.close()
            except:
                break
        time.sleep(10)

# Функция для приема входящих TCP соединений (TCP сервер)
def tcp_server():
    global continue_signal
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    server_socket.bind((my_ip, TCP_PORT))
    while continue_signal:
        try:
            server_socket.listen(1)
            conn, addr = server_socket.accept()
            data = conn.recv(BUFFER_SIZE).decode()
            print(f"{data}({addr})")
            conn.close()
        except:
            break

# Запускаем все параллельно
receive_thread = threading.Thread(target=receive_messages)
send_thread = threading.Thread(target=send_broadcast)
tcp_server_thread = threading.Thread(target=tcp_server)
tcp_client_thread = threading.Thread(target=establish_tcp_connections)

receive_thread.start()
send_thread.start()
tcp_server_thread.start()
tcp_client_thread.start()

receive_thread.join()
send_thread.join()
tcp_server_thread.join()
tcp_client_thread.join()

print("🔚 Завершение работы!")
