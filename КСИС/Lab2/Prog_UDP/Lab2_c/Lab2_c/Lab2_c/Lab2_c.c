#define _CRT_SECURE_NO_WARNINGS  
#define _WINSOCK_DEPRECATED_NO_WARNINGS  

#pragma comment(lib, "ws2_32.lib")  

#include <winsock2.h>   
#include <ws2tcpip.h>  
#include <locale.h>     
#include <windows.h>    
#include <stdio.h>      
#include <winsock.h>    

char DataBuffer[1024];  // ����� ��� ������ � �������� ������ (������ 1024 �����)

int main(int argc, char** argv)
{
    // ������������� ��������� ������� ��� ����������� ����� �������� ������
    SetConsoleCP(1251);
    // ������������� ��������� ������� ��� ����������� ������ �������� ������
    SetConsoleOutputCP(1251);

    struct sockaddr_in SockAddrLocal, SockAddrSend, SockAddrRecv;  // ��������� ��� �������� ������� �������
    SOCKET SockLocal = INVALID_SOCKET;  // ��������� �����, ���� ��� �����������������
    struct hostent* pHostEnt;  // ��������� ��� �������� ���������� � �����
    int nSockOptBC, nAddrSize, nPortRemote, nMsgLen, i;  // ���������� ��� ������ � �������� � �����������
    WSADATA WSAData;  // ��������� ��� �������� ���������� � ������ WinSock
    WORD wWSAVer;  // ������ WinSock

    // ��������� ���������� ���������� ��������� ������
    if (argc != 4) {
        puts("�������� ���������\n");
        puts("�����: UDP_SEND <addr/name> <port> <F>\n");
        return -1;
    }

    // ������������� ���������� ������� WinSock
    wWSAVer = MAKEWORD(1, 1);  // ��������� ������ 1.1
    if (WSAStartup(wWSAVer, &WSAData) != 0) {  // ��������� WinSock
        puts("������ ������������� WinSockets");
        return -1;
    }

    // �������� UDP-������
    SockLocal = socket(PF_INET, SOCK_DGRAM, 0);
    if (SockLocal == INVALID_SOCKET) {
        fputs("������ �������� ������\n", stderr);
        return -1;
    }

    // ��������� ����������������� �������� (Broadcast)
    nSockOptBC = 1;
    setsockopt(SockLocal, SOL_SOCKET, SO_BROADCAST, (char*)(&nSockOptBC), sizeof(nSockOptBC));

    // �������� ������ � ���������� ������
    memset(&SockAddrLocal, 0, sizeof(SockAddrLocal));  // �������� ���������
    SockAddrLocal.sin_family = AF_INET;  // ���������, ��� ���������� IPv4
    SockAddrLocal.sin_addr.S_un.S_addr = INADDR_ANY;  // ��������� ������������ ��� ������� ����������
    SockAddrLocal.sin_port = 0;  // ������������� ��������� ����

    if (bind(SockLocal, (struct sockaddr*)&SockAddrLocal, sizeof(SockAddrLocal)) != 0) {
        fputs("������ �������� � ���������� ������\n", stderr);
        return -1;
    }

    // ���������� ������ �������
    memset(&SockAddrSend, 0, sizeof(SockAddrSend));  // �������� ���������
    SockAddrSend.sin_family = AF_INET;  // ���������, ��� ���������� IPv4

    if (strcmp(argv[1], "255.255.255.255") == 0) {  // ���������, �������� �� ����� �����������������
        SockAddrSend.sin_addr.S_un.S_addr = INADDR_BROADCAST;
    }
    else {
        SockAddrSend.sin_addr.S_un.S_addr = inet_addr(argv[1]);  // ����������� ������ � IP-�����
        if (SockAddrSend.sin_addr.S_un.S_addr == INADDR_NONE) {  // ���� �� ������� �������������
            if ((pHostEnt = gethostbyname(argv[1])) == NULL) {  // ������� �������� IP-����� �� ��������� �����
                fprintf(stderr, "���� �� �������: %s\n", argv[1]);
                return -1;
            }
            SockAddrSend.sin_addr = *(struct in_addr*)(pHostEnt->h_addr_list[0]);
        }
    }

    // ����������� �������� � ������ �� ������ � �����
    if (sscanf(argv[2], "%u", &nPortRemote) < 1) {
        fprintf(stderr, "��������� ����� �����: %s\n", argv[2]);
        return -1;
    }
    SockAddrSend.sin_port = htons((unsigned short)nPortRemote);  // ������������ ���� � ������� ������� ������

    // �������� ���� �������� ���������
    for (i = 3; i < argc; ++i) {  // ������������ ���������� ��������� ��������� ������ (���������)
        // �������� ���������
        fprintf(stdout, "������� �� %s:%u: \"%s\" \n",
            inet_ntoa(SockAddrSend.sin_addr), // ����������� IP-����� �� ��������� ������� � ������ (��������, "192.168.1.1")
            ntohs(SockAddrSend.sin_port),     // ����������� ���� �� �������� ������� ������ � �������� (����� ���������� ���������� ����� �����)
            argv[i]                           // ������� ���������, ������� ����� ����������
        );


        nMsgLen = strlen(argv[i]) + 1;  // ���������� ����� ��������� (+1 ��� null-�����������)

        if (sendto(SockLocal, argv[i], nMsgLen, 0, (struct sockaddr*)&SockAddrSend, sizeof(SockAddrSend)) < nMsgLen) {
            fprintf(stderr, "������ �������: \"%s\"\n", argv[i]);
            continue;
        }

        // ��������� ������
        nAddrSize = sizeof(SockAddrRecv);
        nMsgLen = recvfrom(SockLocal, DataBuffer, sizeof(DataBuffer) - 1, 0, (struct sockaddr*)&SockAddrRecv, &nAddrSize);

        if (nMsgLen <= 0) {  // ������ ������
            fputs("������ ������ ������\n", stderr);
            continue;
        }

        DataBuffer[nMsgLen] = '\0';  // ��������� null-����������
        fprintf(stdout, "����� �� %s:%u: \"%s\" \n",
            inet_ntoa(SockAddrRecv.sin_addr),
            ntohs(SockAddrRecv.sin_port),
            DataBuffer
        );
    }

    // ���������� ������
    shutdown(SockLocal, 2);  // ��������� ����������
    Sleep(100);  // ��������� �������� ����� ��������� ������
    closesocket(SockLocal);  // ��������� �����
    SockLocal = INVALID_SOCKET;  // ��������, ��� ����� ����� �� �������
    WSACleanup();  // ��������� ������ � WinSock

    return 0;  // ��������� ���������
}
