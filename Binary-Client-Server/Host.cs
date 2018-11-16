using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

//TODO: impelentacja protokolu
// POLA OPERACJI                        POLA STATUSU            POLE DLUGOSCI DANYCH(32 bity)       POLE DANYCH
// 000 - dodawanie						0000                    0x00000002 - 2 zmienne po 1 bicie   zmienna dlugosc                      -							
// 001 - odejmowanie				    0001                    -                                   
// 010 - mnozenie						0010                    -                                   
// 011 - dzielenie                      0011                    -                                   
// 100 - AND                            0100                    -                                   
// 101 - OR                             0101                    -                                   
// 110 - przesuniecie bitowe w prawo	0110                    -                                   
// 111 - przesuniecie bitowe w lewo	    0111                    0xFFFFFFFF - 2 zmienne po 2^31                     -

namespace Binary_Client_Server
{
    abstract class Host     // klasa po ktorej dziedziczy serwer i klient
    {
        //TODO: rozwazyc BitArray
        public byte[] buffer;                   // bufor danych
        protected TcpClient client;
        protected const int portNum = 27015;    // port
        protected IPAddress _IP;                // ip
        NetworkStream ns;
        
        public void CreateStream() => ns = client.GetStream();                      // tworzenie strumienia
        public void Read(ref byte[] buffer) => ns.Read(buffer, 0, buffer.Length);   // wysylanie wiadomosci
        public void Write(ref byte[] buffer) => ns.Write(buffer, 0, buffer.Length); // odczytywanie wiadomosci
        //TODO metoda public void setPort(int PortNumer);
        
        public void Exit()
        {
            if(ns!=null)    // jesli blad wystapi na poczatku programu i strumien nie zdazy zostac utworzony
                ns.Close(); 
            if (client != null)
                client.Close();
        }
    }
}
