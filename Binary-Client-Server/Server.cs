using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{
    class Server : Host
    {
        private TcpListener listener;   

        public Server() => _IP = System.Net.IPAddress.Parse("127.0.0.1");       // przypisanie adresu (serwer lokalny)
        public void CreateListener() => listener = new TcpListener(_IP, portNum);   // port nasluchowy
        public void StartListen() => listener.Start();      // rozpoczecie nasluchiwania
        public void AccecptClient() => client = listener.AcceptTcpClient();         // akceptacja klienta

    }
}
