using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

       

namespace Binary_Client_Server
{
    abstract class Host     // klasa po ktorej dziedziczy serwer i klient
    {
        public byte[] buffer;                   // bufor danych
        protected TcpClient client;
        protected const int portNum = 27015;    // port
        protected IPAddress _IP;                // ip
        NetworkStream ns;
        
        public void CreateStream() => ns = client.GetStream();                      // tworzenie strumienia
        public void Read(ref byte[] buffer) => ns.Read(buffer, 0, buffer.Length);   // wysylanie wiadomosci
        public void Write(byte[] buffer) => ns.Write(buffer, 0, buffer.Length); // odczytywanie wiadomosci

        public void Exit()
        {
            if(ns!=null)    // jesli blad wystapi na poczatku programu i strumien nie zdazy zostac utworzony
                ns.Close(); 
            if (client != null)
                client.Close();
        }


    }
}
