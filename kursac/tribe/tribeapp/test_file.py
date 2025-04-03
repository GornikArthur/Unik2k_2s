import asyncio

async def fetch_data(sleep_time):
    print(f"{sleep_time} Начинаю загрузку данных...")
    await asyncio.sleep(sleep_time)
    print(f"{sleep_time} Данные загружены!")
    return f"{sleep_time} Вот данные"

async def main():
    print("Запускаю задачу...")
    data = await fetch_data(3)  # ждет выполнения fetch_data
    data = await fetch_data(1)  # ждет выполнения fetch_data
    print("Результат:", data)

asyncio.run(main())
