import requests
import json
import time

# 🔹 API URLs
GEONAMES_USERNAME = "demo"  # ⚠️ ЗАМЕНИТЕ НА СВОЙ ЛОГИН
GEONAMES_URL = "http://api.geonames.org/searchJSON"

# 1️⃣ Получаем список всех городов для страны (с пагинацией)
def get_all_cities(country_code):
    cities = []
    start_row = 0
    max_rows = 1000  # Лимит GeoNames

    while True:
        params = {
            "country": country_code,
            "featureClass": "P",  # Только населённые пункты
            "maxRows": max_rows,
            "startRow": start_row,
            "username": GEONAMES_USERNAME
        }
        response = requests.get(GEONAMES_URL, params=params)
        data = response.json()

        if "geonames" not in data or not data["geonames"]:
            break  # ❌ Если данных больше нет, выходим из цикла

        cities.extend([city["name"] for city in data["geonames"]])
        start_row += max_rows  # 🔄 Следующая страница
        time.sleep(1)  # ⏳ Пауза, чтобы избежать блокировки API

    return cities

# 2️⃣ Загружаем все страны (код -> название)
def get_countries():
    RESTCOUNTRIES_URL = "https://restcountries.com/v3.1/all"
    response = requests.get(RESTCOUNTRIES_URL)
    countries = response.json()
    return {c["name"]["common"]: c["cca2"] for c in countries}  # {"Russia": "RU"}

# 3️⃣ Загружаем все города для всех стран и сохраняем в JSON
def save_all_countries_with_cities():
    data = {}
    countries = get_countries()

    for country_name, country_code in countries.items():
        print(f"🔹 Загружаем города для {country_name} ({country_code})...")
        data[country_name] = get_all_cities(country_code)

    # Сохраняем в JSON-файл
    with open("all_countries_cities.json", "w", encoding="utf-8") as f:
        json.dump(data, f, indent=4, ensure_ascii=False)

    print("✅ Все данные сохранены в 'all_countries_cities.json'!")

# 🔥 Запускаем
save_all_countries_with_cities()
