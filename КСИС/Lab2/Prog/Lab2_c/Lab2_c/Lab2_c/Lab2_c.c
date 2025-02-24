#define _CRT_SECURE_NO_WARNINGS
#define _WINSOCK_DEPRECATED_NO_WARNINGS

#pragma comment(lib, "ws2_32.lib")

#include <winsock2.h>
#include <ws2tcpip.h>  // Optional: for additional functions if needed

#include <locale.h>

#include <windows.h>
#include <stdio.h>
#include <winsock.h>
char DataBuffer[1024];
int main(int argc, char** argv)
{
	// Set input code page (optional, if reading Russian input)
	SetConsoleCP(1251);
	// Set output code page so that Russian characters are displayed correctly
	SetConsoleOutputCP(1251);

	struct sockaddr_in SockAddrLocal, SockAddrSend, SockAddrRecv;
	SOCKET SockLocal = INVALID_SOCKET;
	struct hostent* pHostEnt;
	int nSockOptBC, nAddrSize, nPortRemote, nMsgLen, i;
	WSADATA WSAData;
	WORD wWSAVer;
	//командная строка
	if (argc != 4) {
		puts("Неверные аргументы\n");
		puts("Вызов: UDP_SEND <addr/name> <port> <F>\n");
		return -1;
	}
	//инициализация подсистемы сокетов
	wWSAVer = MAKEWORD(1, 1);
	if (WSAStartup(wWSAVer, &WSAData) != 0) {
		puts("Ошибка инициализации WinSockets");
		return -1;
	}
	//создание локального сокета
	SockLocal = socket(PF_INET, SOCK_DGRAM, 0);
	if (SockLocal == INVALID_SOCKET) {
		fputs("Ошибка создания сокета\n", stderr);
		return -1;
	}
	//настройка сокета: разрешить отсылку на "широковещательный" адрес
	nSockOptBC = 1;
	setsockopt(SockLocal,
		SOL_SOCKET, SO_BROADCAST,
		(char*)(&nSockOptBC), sizeof(nSockOptBC)
	);
	//привязка сокета к "локальному" адресу
	memset(&SockAddrLocal, 0, sizeof(SockAddrLocal));
	SockAddrLocal.sin_family = AF_INET;
	SockAddrLocal.sin_addr.S_un.S_addr = INADDR_ANY; //все интерфейсы
	SockAddrLocal.sin_port = 0; //выбирать порт автоматически
	if (bind(SockLocal, (struct sockaddr*)&SockAddrLocal, sizeof(SockAddrLocal)) != 0)
	{
		fputs("Ошибка привязки к локальному адресу\n", stderr);
		return -1;
	}
	//подготовка адреса сервера
	memset(&SockAddrSend, 0, sizeof(SockAddrSend));
	SockAddrSend.sin_family = AF_INET;
	if (strcmp(argv[1], "255.255.255.255") == 0) //адрес broadcast
		SockAddrSend.sin_addr.S_un.S_addr = INADDR_BROADCAST;
	else {
		SockAddrSend.sin_addr.S_un.S_addr = inet_addr(argv[1]);
		if (SockAddrSend.sin_addr.S_un.S_addr == INADDR_NONE) {
			if ((pHostEnt = gethostbyname(argv[1])) == NULL) {
				fprintf(stderr, "Хост не опознан: %s\n", argv[1]);
				return -1;
			}
			SockAddrSend.sin_addr = *(struct in_addr*)(pHostEnt->h_addr_list[0]);
		}
	}
	if (sscanf(argv[2], "%u", &nPortRemote) < 1) {
		fprintf(stderr, "Ошибочный номер порта: %s\n", argv[2]);
		return -1;
	}
	SockAddrSend.sin_port = htons((unsigned short)nPortRemote);
	//рабочий цикл
	for (i = 3; i < argc; ++i) { //остальные аргументы командной строки
		//отослать сообщение
		fprintf(stdout, "Отсылка на %s:%u: \"%s\" \n",
			inet_ntoa(SockAddrSend.sin_addr),
			ntohs(SockAddrSend.sin_port),
			argv[i]
		);
		nMsgLen = strlen(argv[i]) + 1;
		if (sendto(SockLocal, argv[i], nMsgLen, 0,
			(struct sockaddr*)&SockAddrSend, sizeof(SockAddrSend)) < nMsgLen)
		{
			fprintf(stderr, "Ошибка отсылки: \"%s\"\n", argv[i]);
			continue;
		}
		//принять и отобразить ответ
		nAddrSize = sizeof(SockAddrRecv);
		nMsgLen = recvfrom(SockLocal, DataBuffer, sizeof(DataBuffer) - 1, 0,
			(struct sockaddr*)&SockAddrRecv, &nAddrSize
		);
		if (nMsgLen <= 0) { //ошибка приема запроса
			fputs("Ошибка приема ответа\n", stderr);
			continue;
		}
		DataBuffer[nMsgLen] = '\0';
		fprintf(stdout, "Ответ от %s:%u: \"%s\" \n",
			inet_ntoa(SockAddrRecv.sin_addr),
			ntohs(SockAddrRecv.sin_port),
			DataBuffer
		);
	}
	//завершение
	shutdown(SockLocal, 2);
	Sleep(100);
	closesocket(SockLocal); SockLocal = INVALID_SOCKET;
	WSACleanup();
	return 0;
}
