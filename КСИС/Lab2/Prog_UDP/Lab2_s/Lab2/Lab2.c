#define _CRT_SECURE_NO_WARNINGS  // ��������� �������������� ������������, ��������� � ��������� ����������� ���������� C
#pragma comment(lib, "ws2_32.lib")  // ���������� ���������� Windows Sockets 2 (ws2_32.lib) ��� ����������

#include <winsock2.h>   // �������� ���������� ��� ������ � �������� � Windows
#include <ws2tcpip.h>   // �������������� ������� ��� ������ � IP-�������� (��������, inet_pton, getaddrinfo)
#include <locale.h>     // ��� ��������� ����������� (��������, ��� ����������� ����������� ��������)

#include <windows.h>    // ������������ ���� ��� ������ � Windows API
#include <stdio.h>      // ����������� ����/����� (printf, scanf � �. �.)
#include <winsock.h>    // ���������� ������ ���������� ��� ������ � �������� (����� ������������ winsock2.h)

#define DEFAULT_ECHO_PORT 7  // ����������� ����� �� ��������� (7 � ����������� ���� ��� echo-��������)
char DataBuffer[1024];  // ����� ��� ����� � �������� ������ �������� 1024 �����

// ������� ���������� n-�� ����� ���������
int fibo(int count) {
    int a = 1;
    int b = 1;
    if (count == 0 || count == 1) {  // ���� n = 0 ��� n = 1, ���������� 1
        return 1;
    }
    for (int i = 2; i <= count; i++) {  // ����������� ������� ����� ���������
        int temp = a + b;
        a = b;
        b = temp;
    }
    return b;  // ���������� n-� ����� ���������
}

int main(int argc, char** argv) {
    // ������������� ��������� ������� ��� ����������� ����� � ������ ������� ��������
    SetConsoleCP(1251);
    SetConsoleOutputCP(1251);

    struct sockaddr_in SockAddrLocal, SockAddrRemote;  // ��������� ��� �������� ������� ������� � �������
    SOCKET SockLocal = INVALID_SOCKET;  // ��������� �����, ���������������� ��� ����������������
    unsigned short nPort = DEFAULT_ECHO_PORT;  // ���������� ��� �������� ������ �����, �� ��������� 7
    int nAddrSize, nCnt;  // ������ ��������� ������ � ���������� ��� �������� ���������� �������� ������
    WSADATA WSAData;  // ��������� ��� �������� ���������� � ���������� Windows Sockets
    WORD wWSAVer;  // ������ Windows Sockets

    // ������ ���������� ��������� ������: ��������� ������ �����
    if (argc > 1)
        if (sscanf(argv[1], "%u", &nPort) < 1)  // ������ ���� �� ��������� ��������� ������
            fprintf(stderr, "��������� ����: %s, use default", nPort);  // ������, ���� ����� ����� �����������

    // ������������� ���������� �������
    wWSAVer = MAKEWORD(1, 1);  // ������ 1.1
    if (WSAStartup(wWSAVer, &WSAData) != 0) {  // ������ ���������� �������
        puts("������ ������������� ���������� WinSocket");
        return -1;
    }

    // �������� UDP-������
    SockLocal = socket(PF_INET, SOCK_DGRAM, 0);  // ������������ �������� UDP (SOCK_DGRAM)
    if (SockLocal == INVALID_SOCKET) {  // ���������, ������� �� ������� �����
        fputs("������ �������� ������\n", stderr);
        return -1;
    }

    // �������� ������ � ���������� ������
    memset(&SockAddrLocal, 0, sizeof(SockAddrLocal));  // �������� ��������� ������
    SockAddrLocal.sin_family = AF_INET;  // ��������� ��������� ������� (IPv4)
    SockAddrLocal.sin_addr.S_un.S_addr = INADDR_ANY;  // ����������� �� ���� ��������� ������� �����������
    SockAddrLocal.sin_port = htons(nPort);  // ����������� ����� ����� � ������� ������� ������
    if (bind(SockLocal, (struct sockaddr*)&SockAddrLocal, sizeof(SockAddrLocal)) != 0) {  // ����������� �����
        fprintf(stdout, "������ �������� ������, ���� %u\n", ntohs(SockAddrLocal.sin_port));
        return -1;
    }

    fprintf(stderr, "������ �������, ���� %u\n", ntohs(SockAddrLocal.sin_port));  // ������� ��������� � ������� �������

    // �������� ������� ���� �������
    while (1) {  // ������ �������� ����������
        nAddrSize = sizeof(SockAddrRemote);
        // ��������� ��������� ��������� �� �������
        nCnt = recvfrom(SockLocal, DataBuffer, sizeof(DataBuffer) - 1, 0,
            (struct sockaddr*)&SockAddrRemote, &nAddrSize);
        if (nCnt < 0) {  // ���������, ������� �� �������� ������
            fputs("������ ������ ���������\n", stderr);
            continue;
        }

        // ���������� ����� ���������
        int res = fibo(atoi(DataBuffer));  // ����������� ������ � ����� � ��������� ����� ���������
        printf("\n�� ������� ��������� ���������� ����� ���������.\n��������� = %d", res);

        memset(DataBuffer, 0, sizeof(DataBuffer));  // ������� ����� ����� ��������� ������
        snprintf(DataBuffer, sizeof(DataBuffer), "%d", res);  // ����������� ��������� ������� � ������

        // ���������� ����� �������
        sendto(SockLocal, DataBuffer, strlen(DataBuffer), 0,
            (struct sockaddr*)&SockAddrRemote, sizeof(SockAddrRemote));
    }

    // ���������� ������ ������� (���� ��� ������� �� ����������, ��� ��� ���� �����������)
    shutdown(SockLocal, 2);  // ��������� ���������� (2 - ������ �������� � ������)
    Sleep(100);  // ���� ����� ��������� ����������
    closesocket(SockLocal);  // ��������� �����
    SockLocal = INVALID_SOCKET;  // �������� ���������� ������
    WSACleanup();  // ����������� �������, ��������� � Winsock
    return 0;  // ����� �� ���������
}
