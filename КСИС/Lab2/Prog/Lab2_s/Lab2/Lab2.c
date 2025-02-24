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

	struct sockaddr_in SockAddrLocal, SockAddrRemote;
	SOCKET SockLocal = INVALID_SOCKET;
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
	SockLocal = socket(PF_INET, SOCK_DGRAM, 0);
	if (SockLocal == INVALID_SOCKET) {
		fputs("Ошибка создания сокета\n", stderr);
		return -1;
	}
	//привязка сокета к локальному адресу
	memset(&SockAddrLocal, 0, sizeof(SockAddrLocal));
	SockAddrLocal.sin_family = AF_INET;
	SockAddrLocal.sin_addr.S_un.S_addr = INADDR_ANY;
	SockAddrLocal.sin_port = htons(nPort); //(<номер_порта_сервера>);
	if (bind(SockLocal,
		(struct sockaddr*)&SockAddrLocal, sizeof(SockAddrLocal)
	) != 0)
	{
		fprintf(stdout, "Ошибка привязки сокета, порт %u\n",
			ntohs(SockAddrLocal.sin_port)
		);
		return -1;
	}
	fprintf(stderr, "Сервер запущен, порт %u\n",
		ntohs(SockAddrLocal.sin_port)
	);
	//основной рабочий цикл
	while (1) { //для сервера цикл обычно бесконечен
		nAddrSize = sizeof(SockAddrRemote);
		//принять входящее сообщение ("эхо-запрос")
		nCnt = recvfrom(SockLocal,
			DataBuffer, sizeof(DataBuffer) - 1, 0,
			(struct sockaddr*)&SockAddrRemote, &nAddrSize
		);
		if (nCnt < 0) { //ошибка приема запроса
			fputs("Ошибка приема сообщения\n", stderr);
			continue;
		}
		//вернуть сообщение как эхо-отклик
		int res = fibo(atoi(DataBuffer)); // Compute Fibonacci number
		printf("\nНа сервере произошли вычисления числа Фибоначи.\nРезультат = %d", res);
		memset(DataBuffer, 0, sizeof(DataBuffer)); // Clear buffer
		snprintf(DataBuffer, sizeof(DataBuffer), "%d", res); // Convert result to string

		// Send the result back to the client
		sendto(SockLocal, DataBuffer, strlen(DataBuffer), 0, (struct sockaddr*)&SockAddrRemote, sizeof(SockAddrRemote));
	}
	//завершение - здесь никогда не достигается!
	shutdown(SockLocal, 2);
	Sleep(100);
	closesocket(SockLocal); SockLocal = INVALID_SOCKET;
	WSACleanup();
	return 0;
}