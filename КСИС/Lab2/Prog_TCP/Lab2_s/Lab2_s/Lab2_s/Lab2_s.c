#define _CRT_SECURE_NO_WARNINGS
#pragma comment(lib, "ws2_32.lib")

#include <winsock2.h>
#include <ws2tcpip.h>  // Optional: for additional functions if needed
#include <locale.h>

#include <windows.h>
#include <stdio.h>
#include <winsock.h>
#define DEFAULT_ECHO_PORT 7
char DataBuffer[1024];

int fibo(int count) {
	int a = 1;
	int b = 1;
	if (count == 0 || count == 1) {
		return 1;
	}
	for (int i = 2; i <= count; i++) {
		int temp = a + b;
		a = b;
		b = temp;
	}
	return b;
}

int main(int argc, char** argv)
{
	// Set input code page (optional, if reading Russian input)
	SetConsoleCP(1251);
	// Set output code page so that Russian characters are displayed correctly
	SetConsoleOutputCP(1251);
	struct sockaddr_in SockAddrBase, SockAddrPeer;
	SOCKET SockBase = INVALID_SOCKET, SockData = INVALID_SOCKET;
	unsigned short nPort = DEFAULT_ECHO_PORT;
	int nAddrSize, nCnt;
	WSADATA WSAData;
	WORD wWSAVer;
	//разбор командной строки: номер порта
	if (argc > 1)
		if (sscanf(argv[1], "%u", &nPort) < 1)
			fprintf(stderr, "Ошибочный порт: %s, use default", nPort);
	//инициализация подсистемы сокетов
	wWSAVer = MAKEWORD(1, 1);
	if (WSAStartup(wWSAVer, &WSAData) != 0) {
		puts("Ошибка инициализации подсистемы WinSocket");
		return -1;
	}
	//создание локального сокета
	SockBase = socket(PF_INET, SOCK_STREAM, 0);
	if (SockBase == INVALID_SOCKET) {
		fputs("Ошибка создания сокета\n", stderr);
		return -1;
	}
	//привязка базового сокета к локальному адресу
	memset(&SockAddrBase, 0, sizeof(SockAddrBase));
	SockAddrBase.sin_family = AF_INET;
	SockAddrBase.sin_addr.S_un.S_addr = INADDR_ANY;
	SockAddrBase.sin_port = htons(nPort); //(<номер_порта_сервера>);
	if (bind(SockBase,
		(struct sockaddr*)&SockAddrBase, sizeof(SockAddrBase)
	) != 0)
	{
		fprintf(stderr, "Ошибка привязки к локальному порту: %u\n",
			ntohs(SockAddrBase.sin_port)
		);
		return -1;
	}
	//включение режима "прослушивания"
	if (listen(SockBase, 2) != 0) { //очередь на 2 места
		closesocket(SockBase);
		fputs("Ошибка включения режима прослушивания\n", stderr);;
		return -1;
	}
	fprintf(stderr,
		"Сервер запущен, порт %u\n",
		ntohs(SockAddrBase.sin_port)
	);
	//основной рабочий цикл - прием и обслуживание соединений
	while (1) { //для сервера цикл обычно бесконечен
		nAddrSize = sizeof(SockAddrPeer);
		SockData = accept(SockBase,
			(struct sockaddr*)&SockAddrPeer, &nAddrSize
		);
		if (SockData == INVALID_SOCKET) {
			fputs("Ошибка приема соединения\n", stderr);
			continue;
		}
		//цикл обслуживания одного соединения
		while (1) {
			nCnt = recv(SockData, DataBuffer, sizeof(DataBuffer) - 1, 0);
			if (nCnt <= 0)
				break;
			int res = fibo(atoi(DataBuffer)); // Compute Fibonacci number
			printf("\nНа сервере произошли вычисления числа Фибоначи.\nРезультат = %d", res);
			memset(DataBuffer, 0, sizeof(DataBuffer)); // Clear buffer
			snprintf(DataBuffer, sizeof(DataBuffer), "%d", res); // Convert result to string

			send(SockData, DataBuffer, strlen(DataBuffer), 0); //возврат "эха"
		}
		shutdown(SockData, 2);
		closesocket(SockData); SockData = INVALID_SOCKET;
	}
	//завершение - здесь никогда не достигается!
	shutdown(SockBase, 2);
	Sleep(100);
	closesocket(SockBase); SockBase = INVALID_SOCKET;
	WSACleanup();
	return 0;
}
