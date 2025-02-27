#define _CRT_SECURE_NO_WARNINGS  
#define _WINSOCK_DEPRECATED_NO_WARNINGS  

#pragma comment(lib, "ws2_32.lib")  

#include <winsock2.h>   
#include <ws2tcpip.h>  
#include <locale.h>     
#include <windows.h>    
#include <stdio.h>      
#include <winsock.h>    

char DataBuffer[1024];  // Буфер для приема и отправки данных (размер 1024 байта)

int main(int argc, char** argv)
{
    // Устанавливаем кодировку консоли для корректного ввода русского текста
    SetConsoleCP(1251);
    // Устанавливаем кодировку консоли для корректного вывода русского текста
    SetConsoleOutputCP(1251);

    struct sockaddr_in SockAddrLocal, SockAddrSend, SockAddrRecv;  // Структуры для хранения адресов сокетов
    SOCKET SockLocal = INVALID_SOCKET;  // Локальный сокет, пока что неинициализирован
    struct hostent* pHostEnt;  // Структура для хранения информации о хосте
    int nSockOptBC, nAddrSize, nPortRemote, nMsgLen, i;  // Переменные для работы с сокетами и сообщениями
    WSADATA WSAData;  // Структура для хранения информации о версии WinSock
    WORD wWSAVer;  // Версия WinSock

    // Проверяем количество аргументов командной строки
    if (argc != 4) {
        puts("Неверные аргументы\n");
        puts("Вызов: UDP_SEND <addr/name> <port> <F>\n");
        return -1;
    }

    // Инициализация библиотеки сокетов WinSock
    wWSAVer = MAKEWORD(1, 1);  // Указываем версию 1.1
    if (WSAStartup(wWSAVer, &WSAData) != 0) {  // Запускаем WinSock
        puts("Ошибка инициализации WinSockets");
        return -1;
    }

    // Создание UDP-сокета
    SockLocal = socket(PF_INET, SOCK_DGRAM, 0);
    if (SockLocal == INVALID_SOCKET) {
        fputs("Ошибка создания сокета\n", stderr);
        return -1;
    }

    // Разрешаем широковещательную передачу (Broadcast)
    nSockOptBC = 1;
    setsockopt(SockLocal, SOL_SOCKET, SO_BROADCAST, (char*)(&nSockOptBC), sizeof(nSockOptBC));

    // Привязка сокета к локальному адресу
    memset(&SockAddrLocal, 0, sizeof(SockAddrLocal));  // Обнуляем структуру
    SockAddrLocal.sin_family = AF_INET;  // Указываем, что используем IPv4
    SockAddrLocal.sin_addr.S_un.S_addr = INADDR_ANY;  // Разрешаем использовать все сетевые интерфейсы
    SockAddrLocal.sin_port = 0;  // Автоматически назначаем порт

    if (bind(SockLocal, (struct sockaddr*)&SockAddrLocal, sizeof(SockAddrLocal)) != 0) {
        fputs("Ошибка привязки к локальному адресу\n", stderr);
        return -1;
    }

    // Подготовка адреса сервера
    memset(&SockAddrSend, 0, sizeof(SockAddrSend));  // Обнуляем структуру
    SockAddrSend.sin_family = AF_INET;  // Указываем, что используем IPv4

    if (strcmp(argv[1], "255.255.255.255") == 0) {  // Проверяем, является ли адрес широковещательным
        SockAddrSend.sin_addr.S_un.S_addr = INADDR_BROADCAST;
    }
    else {
        SockAddrSend.sin_addr.S_un.S_addr = inet_addr(argv[1]);  // Преобразуем строку в IP-адрес
        if (SockAddrSend.sin_addr.S_un.S_addr == INADDR_NONE) {  // Если не удалось преобразовать
            if ((pHostEnt = gethostbyname(argv[1])) == NULL) {  // Пробуем получить IP-адрес по доменному имени
                fprintf(stderr, "Хост не опознан: %s\n", argv[1]);
                return -1;
            }
            SockAddrSend.sin_addr = *(struct in_addr*)(pHostEnt->h_addr_list[0]);
        }
    }

    // Преобразуем аргумент с портом из строки в число
    if (sscanf(argv[2], "%u", &nPortRemote) < 1) {
        fprintf(stderr, "Ошибочный номер порта: %s\n", argv[2]);
        return -1;
    }
    SockAddrSend.sin_port = htons((unsigned short)nPortRemote);  // Конвертируем порт в сетевой порядок байтов

    // Основной цикл отправки сообщений
    for (i = 3; i < argc; ++i) {  // Обрабатываем оставшиеся аргументы командной строки (сообщения)
        // Отправка сообщения
        fprintf(stdout, "Отсылка на %s:%u: \"%s\" \n",
            inet_ntoa(SockAddrSend.sin_addr), // Преобразует IP-адрес из двоичного формата в строку (например, "192.168.1.1")
            ntohs(SockAddrSend.sin_port),     // Преобразует порт из сетевого порядка байтов в хостовый (чтобы отобразить корректный номер порта)
            argv[i]                           // Выводит сообщение, которое будет отправлено
        );


        nMsgLen = strlen(argv[i]) + 1;  // Определяем длину сообщения (+1 для null-терминатора)

        if (sendto(SockLocal, argv[i], nMsgLen, 0, (struct sockaddr*)&SockAddrSend, sizeof(SockAddrSend)) < nMsgLen) {
            fprintf(stderr, "Ошибка отсылки: \"%s\"\n", argv[i]);
            continue;
        }

        // Получение ответа
        nAddrSize = sizeof(SockAddrRecv);
        nMsgLen = recvfrom(SockLocal, DataBuffer, sizeof(DataBuffer) - 1, 0, (struct sockaddr*)&SockAddrRecv, &nAddrSize);

        if (nMsgLen <= 0) {  // Ошибка приема
            fputs("Ошибка приема ответа\n", stderr);
            continue;
        }

        DataBuffer[nMsgLen] = '\0';  // Добавляем null-терминатор
        fprintf(stdout, "Ответ от %s:%u: \"%s\" \n",
            inet_ntoa(SockAddrRecv.sin_addr),
            ntohs(SockAddrRecv.sin_port),
            DataBuffer
        );
    }

    // Завершение работы
    shutdown(SockLocal, 2);  // Завершаем соединение
    Sleep(100);  // Небольшая задержка перед закрытием сокета
    closesocket(SockLocal);  // Закрываем сокет
    SockLocal = INVALID_SOCKET;  // Помечаем, что сокет более не активен
    WSACleanup();  // Завершаем работу с WinSock

    return 0;  // Завершаем программу
}
