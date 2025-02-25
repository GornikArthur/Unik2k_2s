#define _CRT_SECURE_NO_WARNINGS  // Отключает предупреждения безопасности, связанные с функциями стандартной библиотеки C
#pragma comment(lib, "ws2_32.lib")  // Подключает библиотеку Windows Sockets 2 (ws2_32.lib) при компиляции

#include <winsock2.h>   // Основная библиотека для работы с сокетами в Windows
#include <ws2tcpip.h>   // Дополнительные функции для работы с IP-адресами (например, inet_pton, getaddrinfo)
#include <locale.h>     // Для установки локализации (например, для корректного отображения символов)

#include <windows.h>    // Заголовочный файл для работы с Windows API
#include <stdio.h>      // Стандартный ввод/вывод (printf, scanf и т. д.)
#include <winsock.h>    // Устаревшая версия библиотеки для работы с сокетами (лучше использовать winsock2.h)

#define DEFAULT_ECHO_PORT 7  // Определение порта по умолчанию (7 — стандартный порт для echo-серверов)
char DataBuffer[1024];  // Буфер для приёма и передачи данных размером 1024 байта

// Функция вычисления n-го числа Фибоначчи
int fibo(int count) {
    int a = 1;
    int b = 1;
    if (count == 0 || count == 1) {  // Если n = 0 или n = 1, возвращаем 1
        return 1;
    }
    for (int i = 2; i <= count; i++) {  // Итеративный подсчёт числа Фибоначчи
        int temp = a + b;
        a = b;
        b = temp;
    }
    return b;  // Возвращаем n-е число Фибоначчи
}

int main(int argc, char** argv) {
    // Устанавливаем кодировку консоли для корректного ввода и вывода русских символов
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    struct sockaddr_in SockAddrLocal, SockAddrRemote;  // Структуры для хранения адресов сервера и клиента
    SOCKET SockLocal = INVALID_SOCKET;  // Локальный сокет, инициализируется как недействительный
    unsigned short nPort = DEFAULT_ECHO_PORT;  // Переменная для хранения номера порта, по умолчанию 7
    int nAddrSize, nCnt;  // Размер структуры адреса и переменная для хранения количества принятых байтов
    WSADATA WSAData;  // Структура для хранения информации о реализации Windows Sockets
    WORD wWSAVer;  // Версия Windows Sockets

    // Разбор аргументов командной строки: получение номера порта
    if (argc > 1)
        if (sscanf(argv[1], "%u", &nPort) < 1)  // Читаем порт из аргумента командной строки
            fprintf(stderr, "Ошибочный порт: %s, use default", nPort);  // Ошибка, если номер порта некорректен

    // Инициализация библиотеки сокетов
    wWSAVer = MAKEWORD(1, 1);  // Версия 1.1
    if (WSAStartup(wWSAVer, &WSAData) != 0) {  // Запуск библиотеки сокетов
        puts("Ошибка инициализации подсистемы WinSocket");
        return -1;
    }

    // Создание UDP-сокета
    SockLocal = socket(PF_INET, SOCK_DGRAM, 0);  // Используется протокол UDP (SOCK_DGRAM)
    if (SockLocal == INVALID_SOCKET) {  // Проверяем, удалось ли создать сокет
        fputs("Ошибка создания сокета\n", stderr);
        return -1;
    }

    // Привязка сокета к локальному адресу
    memset(&SockAddrLocal, 0, sizeof(SockAddrLocal));  // Обнуляем структуру адреса
    SockAddrLocal.sin_family = AF_INET;  // Указываем семейство адресов (IPv4)
    SockAddrLocal.sin_addr.S_un.S_addr = INADDR_ANY;  // Привязываем ко всем доступным сетевым интерфейсам
    SockAddrLocal.sin_port = htons(nPort);  // Преобразуем номер порта в сетевой порядок байтов
    if (bind(SockLocal, (struct sockaddr*)&SockAddrLocal, sizeof(SockAddrLocal)) != 0) {  // Привязываем сокет
        fprintf(stdout, "Ошибка привязки сокета, порт %u\n", ntohs(SockAddrLocal.sin_port));
        return -1;
    }

    fprintf(stderr, "Сервер запущен, порт %u\n", ntohs(SockAddrLocal.sin_port));  // Выводим сообщение о запуске сервера

    // Основной рабочий цикл сервера
    while (1) {  // Сервер работает бесконечно
        nAddrSize = sizeof(SockAddrRemote);
        // Получение входящего сообщения от клиента
        nCnt = recvfrom(SockLocal, DataBuffer, sizeof(DataBuffer) - 1, 0,
            (struct sockaddr*)&SockAddrRemote, &nAddrSize);
        if (nCnt < 0) {  // Проверяем, удалось ли получить данные
            fputs("Ошибка приема сообщения\n", stderr);
            continue;
        }

        // Вычисление числа Фибоначчи
        int res = fibo(atoi(DataBuffer));  // Преобразуем строку в число и вычисляем число Фибоначчи
        printf("\nНа сервере произошли вычисления числа Фибоначчи.\nРезультат = %d", res);

        memset(DataBuffer, 0, sizeof(DataBuffer));  // Очищаем буфер перед отправкой ответа
        snprintf(DataBuffer, sizeof(DataBuffer), "%d", res);  // Преобразуем результат обратно в строку

        // Отправляем ответ клиенту
        sendto(SockLocal, DataBuffer, strlen(DataBuffer), 0,
            (struct sockaddr*)&SockAddrRemote, sizeof(SockAddrRemote));
    }

    // Завершение работы сервера (этот код никогда не выполнится, так как цикл бесконечный)
    shutdown(SockLocal, 2);  // Закрываем соединение (2 - запрет отправки и приема)
    Sleep(100);  // Даем время завершить соединение
    closesocket(SockLocal);  // Закрываем сокет
    SockLocal = INVALID_SOCKET;  // Обнуляем переменную сокета
    WSACleanup();  // Освобождаем ресурсы, связанные с Winsock
    return 0;  // Выход из программы
}
