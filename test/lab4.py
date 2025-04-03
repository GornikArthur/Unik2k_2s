import socket
import threading

# Настройки прокси-сервера
PROXY_HOST = '127.0.0.1'  # Локальный IP-адрес
PROXY_PORT = 8080  # Порт для подключения браузера

# Черный список доменов (для блокировки)
BLACKLIST = {"example.com", "badwebsite.com"}


def handle_client(client_socket):
    """ Обработчик запросов клиентов """
    request = client_socket.recv(4096)  # Получаем запрос от клиента
    request_line = request.split(b"\r\n")[0]  # Первая строка запроса (например, "GET http://site.com/ HTTP/1.1")

    try:
        # Разбираем строку запроса
        method, full_url, protocol = request_line.decode().split()
        url_parts = full_url.split("/")
        domain = url_parts[2] if len(url_parts) > 2 else ""

        # Фильтрация заблокированных сайтов
        if domain in BLACKLIST:
            response = "HTTP/1.1 403 Forbidden\r\n\r\n<b>Access Denied</b>"
            client_socket.send(response.encode())
            print(f"[БЛОКИРОВКА] {domain} заблокирован!")
            client_socket.close()
            return

        # Преобразуем полный URL в путь
        path = "/" + "/".join(url_parts[3:]) if len(url_parts) > 3 else "/"

        # Определяем целевой сервер и порт
        target_host = domain
        target_port = 80

        # Создаем соединение с целевым сервером
        server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        server_socket.connect((target_host, target_port))

        # Формируем новый запрос (заменяем полный URL на путь)
        modified_request = f"{method} {path} {protocol}\r\n"
        request = request.replace(request_line, modified_request.encode())

        # Отправляем запрос на реальный сервер
        server_socket.send(request)

        # Получаем ответ от сервера
        response = server_socket.recv(4096)
        client_socket.send(response)  # Отправляем клиенту

        # Логируем запрос
        print(f"[ПРОКСИ] {method} {full_url} -> {target_host}{path}")

        # Закрываем соединения
        server_socket.close()
        client_socket.close()

    except Exception as e:
        print(f"[ОШИБКА] {e}")
        client_socket.close()


def start_proxy():
    """ Запуск прокси-сервера """
    proxy_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
    proxy_socket.bind((PROXY_HOST, PROXY_PORT))
    proxy_socket.listen(5)
    print(f"[ЗАПУСК] Прокси-сервер запущен на {PROXY_HOST}:{PROXY_PORT}")

    while True:
        client_socket, _ = proxy_socket.accept()
        client_thread = threading.Thread(target=handle_client, args=(client_socket,))
        client_thread.start()


if __name__ == "__main__":
    start_proxy()
