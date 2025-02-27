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
	struct sockaddr_in SockAddrServer;
	SOCKET SockData = INVALID_SOCKET;
	struct hostent* pHostEnt;
	int nPortServer, nMsgLen, i;
	WSADATA WSAData;
	WORD wWSAVer;
	//��������� ������
	if (argc != 4) {
		puts("�������� ���������\n");
		puts("�����: TCP_SEND <addr/name> <port> <F>\n");
		return -1;
	}
	//������������� ���������� �������
	wWSAVer = MAKEWORD(1, 1);
	if (WSAStartup(wWSAVer, &WSAData) != 0) {
		puts("������ ������������� WinSocket");
		return -1;
	}
	//���������� ������ �������
	memset(&SockAddrServer, 0, sizeof(SockAddrServer));
	SockAddrServer.sin_family = AF_INET;
	if (strcmp(argv[1], "255.255.255.255") == 0) //����� broadcast
		SockAddrServer.sin_addr.S_un.S_addr = INADDR_BROADCAST;
	else {
		SockAddrServer.sin_addr.S_un.S_addr = inet_addr(argv[1]);
		if (SockAddrServer.sin_addr.S_un.S_addr == INADDR_NONE) {
			if ((pHostEnt = gethostbyname(argv[1])) == NULL) {
				fprintf(stderr, "���� �� �������: %s\n", argv[1]);
				return -1;
			}
			SockAddrServer.sin_addr = *(struct in_addr*)(pHostEnt->h_addr_list[0]);
		}
	}
	if (sscanf(argv[2], "%u", &nPortServer) < 1) {
		fprintf(stderr, "��������� ����� �����: %s\n", argv[2]);
		return -1;
	}
	SockAddrServer.sin_port = htons((unsigned short)nPortServer);
	//�������� ���������� ������
	SockData = socket(PF_INET, SOCK_STREAM, 0);
	if (SockData == INVALID_SOCKET) {
		fputs("������ �������� ������\n", stderr);
		return -1;
	}
	//������ �� ������������ ����������
	if (connect(SockData,
		(const struct sockaddr*)&SockAddrServer, sizeof(SockAddrServer)) != 0)
	{
		fprintf(stderr, "������ ���������� � %s:%u\n",
			inet_ntoa(SockAddrServer.sin_addr),
			ntohs(SockAddrServer.sin_port)
		);
		closesocket(SockData);
		return -1;
	}
	fprintf(stdout, "����������� ���������� � ��������: %s:%u\n",
		inet_ntoa(SockAddrServer.sin_addr),
		ntohs(SockAddrServer.sin_port)
	);
	//������� ����
	for (i = 3; i < argc; ++i) { //��������� ��������� ��������� ������
		//�������� ���������
		fprintf(stdout, "�������: \"%s\" \n", argv[i]);
		nMsgLen = strlen(argv[i]) + 1;
		if (send(SockData, argv[i], nMsgLen, 0) < nMsgLen) {
			fprintf(stderr, "������ �������: \"%s\"\n", argv[i]);
			continue;
		}
		//������� � ���������� �����
		fprintf(stdout, "�����...");
		nMsgLen = recv(SockData, DataBuffer, sizeof(DataBuffer) - 1, 0);
		if (nMsgLen <= 0) { //������ ������ ������
			fputs("������ ������\n", stderr);
			continue;
		}
		DataBuffer[nMsgLen] = '\0';
		fprintf(stdout, "\b\b\b: \"%s\" \n", DataBuffer);
	}
	//����������
	shutdown(SockData, 2);
	Sleep(100);
	closesocket(SockData); SockData = INVALID_SOCKET;
	WSACleanup();
	return 0;
}
