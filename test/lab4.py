#http://web.simmons.edu/~grovesd/comm244/notes/week2/links
#http://old-dos.ru/index.php?page=files&mode=files&do=show&id=100953
#http://badwebsite.com/

import socket
import threading

PROXY_HOST = '127.0.0.1'
PROXY_PORT = 8080

# Черный список доменов (для блокировки)
BLACKLIST = {"example.com", "badwebsite.com"}


def handle_client(client_socket):
    try:
        request = client_socket.recv(4096)
        headers = request.decode(errors='ignore').split("\r\n")
        request_line = headers[0]
        method, full_url, protocol = request_line.split()

        # Обработка абсолютного URL (как у браузеров через прокси)
        if full_url.startswith("http://")==False:
            response = "HTTP/1.1 400 Bad Request"
            client_socket.send(response.encode())
            client_socket.close()
            return

        url = full_url[len("http://"):]
        host = url.split("/")[0]
        path = "/" + "/".join(url.split("/")[1:])

        # Проверка чёрного списка
        if host in BLACKLIST:
            response = "HTTP/1.1 403 Forbidde"
            client_socket.send(response.encode())
            print(f"[БЛОКИРОВКА] {host} заблокирован!")
            client_socket.close()
            return

        # Переписываем request-line (с абсолютного URL → относительный путь)
        new_request_line = f"{method} {path} {protocol}"
        request = request.replace(request_line.encode(), new_request_line.encode())

        # Подключаемся к целевому серверу
        server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        server_socket.connect((host, 80))
        server_socket.sendall(request)

        while True:
            response = server_socket.recv(4096)
            if not response:
                break
            client_socket.send(response)

        print(f"[ПРОКСИ] {method} {host}{path}")

    except Exception:
        pass
    finally:
        client_socket.close()

def start_proxy():
    proxy_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    proxy_socket.bind((PROXY_HOST, PROXY_PORT))
    proxy_socket.listen(1)
    print(f"[ЗАПУСК] Прокси-сервер запущен на {PROXY_HOST}:{PROXY_PORT}")

    while True:
        client_socket, _ = proxy_socket.accept()
        client_thread = threading.Thread(target=handle_client, args=(client_socket,))
        client_thread.start()


if __name__ == "__main__":
    start_proxy()
