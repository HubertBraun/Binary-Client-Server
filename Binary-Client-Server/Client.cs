using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{

    class Client : Host 
    {
        public Client() => _IP = System.Net.IPAddress.Parse("127.0.0.1");   // konstruktor bezargumentowy (do testow)

        public Client(string[] args)    // konstruktor argumentowy
        {
            if (args.Length == 1)   // uruchamianie programu z parametrem
            {
                _IP = System.Net.IPAddress.Parse(args[0]);
                //TODO: obsluzyc wyjatki, przy wpisaniu niepoprawnego adresi IP
                Console.WriteLine("Adres IP Servera: {0}", _IP);

            }
            else    // uruchamianie bez parametru
            {
                Console.WriteLine("Prosze podac adres serwera");    
                _IP = System.Net.IPAddress.Parse(Console.ReadLine());
            }

        }
        public void Createclient() => client = new TcpClient(_IP.ToString(), portNum);  // utworzenie klienta


    }


}
