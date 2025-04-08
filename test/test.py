full_url = "http://example.com/path/page.html"

# Убираем префикс "http://"
url = full_url[len("http://"):]  # Получим "example.com/path/page.html"

# Извлекаем доменное имя (host)
host = url.split("/")[0]  # Получим "example.com"

# Формируем путь из оставшейся части URL
path = "/" + "/".join(url.split("/")[1:])  # Получим "/path/page.html"

print("Host:", host)  # Выведет: Host: example.com
print("Path:", path)  # Выведет: Path: /path/page.html