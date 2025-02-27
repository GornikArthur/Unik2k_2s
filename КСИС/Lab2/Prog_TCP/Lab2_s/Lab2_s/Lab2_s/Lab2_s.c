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
	//������ ��������� ������: ����� �����
	if (argc > 1)
		if (sscanf(argv[1], "%u", &nPort) < 1)
			fprintf(stderr, "��������� ����: %s, use default", nPort);
	//������������� ���������� �������
	wWSAVer = MAKEWORD(1, 1);
	if (WSAStartup(wWSAVer, &WSAData) != 0) {
		puts("������ ������������� ���������� WinSocket");
		return -1;
	}
	//�������� ���������� ������
	SockBase = socket(PF_INET, SOCK_STREAM, 0);
	if (SockBase == INVALID_SOCKET) {
		fputs("������ �������� ������\n", stderr);
		return -1;
	}
	//�������� �������� ������ � ���������� ������
	memset(&SockAddrBase, 0, sizeof(SockAddrBase));
	SockAddrBase.sin_family = AF_INET;
	SockAddrBase.sin_addr.S_un.S_addr = INADDR_ANY;
	SockAddrBase.sin_port = htons(nPort); //(<�����_�����_�������>);
	if (bind(SockBase,
		(struct sockaddr*)&SockAddrBase, sizeof(SockAddrBase)
	) != 0)
	{
		fprintf(stderr, "������ �������� � ���������� �����: %u\n",
			ntohs(SockAddrBase.sin_port)
		);
		return -1;
	}
	//��������� ������ "�������������"
	if (listen(SockBase, 2) != 0) { //������� �� 2 �����
		closesocket(SockBase);
		fputs("������ ��������� ������ �������������\n", stderr);;
		return -1;
	}
	fprintf(stderr,
		"������ �������, ���� %u\n",
		ntohs(SockAddrBase.sin_port)
	);
	//�������� ������� ���� - ����� � ������������ ����������
	while (1) { //��� ������� ���� ������ ����������
		nAddrSize = sizeof(SockAddrPeer);
		SockData = accept(SockBase,
			(struct sockaddr*)&SockAddrPeer, &nAddrSize
		);
		if (SockData == INVALID_SOCKET) {
			fputs("������ ������ ����������\n", stderr);
			continue;
		}
		//���� ������������ ������ ����������
		while (1) {
			nCnt = recv(SockData, DataBuffer, sizeof(DataBuffer) - 1, 0);
			if (nCnt <= 0)
				break;
			int res = fibo(atoi(DataBuffer)); // Compute Fibonacci number
			printf("\n�� ������� ��������� ���������� ����� ��������.\n��������� = %d", res);
			memset(DataBuffer, 0, sizeof(DataBuffer)); // Clear buffer
			snprintf(DataBuffer, sizeof(DataBuffer), "%d", res); // Convert result to string

			send(SockData, DataBuffer, strlen(DataBuffer), 0); //������� "���"
		}
		shutdown(SockData, 2);
		closesocket(SockData); SockData = INVALID_SOCKET;
	}
	//���������� - ����� ������� �� �����������!
	shutdown(SockBase, 2);
	Sleep(100);
	closesocket(SockBase); SockBase = INVALID_SOCKET;
	WSACleanup();
	return 0;
}
