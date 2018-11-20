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
        public void Createclient() => client = new TcpClient(_IP.ToString(), portNum);  // utworzenie klienta

        public Client(string[] args)    // konstruktor argumentowy
        {
            if (args.Length == 1)   // uruchamianie programu z parametrem
            {
                _IP = System.Net.IPAddress.Parse(args[0]);
                //TODO: obsluzyc wyjatki, przy wpisaniu niepoprawnego adresu IP
                Console.WriteLine("Adres IP Servera: {0}", _IP);

            }
            else    // uruchamianie bez parametru
            {
                Console.WriteLine("Prosze podac adres serwera");    
                _IP = System.Net.IPAddress.Parse(Console.ReadLine());
            }

        }

        public int ReadAnswer(Segment seg)
        {
            string[] str = seg.Encoding();
            if(str[1] == "1100")
            {
                return -2;
            }

            if (str.Length >= 7)
            {
                Console.WriteLine(str[6]);
                string toReturn = str[6];   // miejsce, w ktorym zapisana jest liczba
                return toReturn.ConvertStringtoInt();
            }
            else
                return -1;
           
        }
       
        public byte[] IDRequest()
        {
            byte[] buffer;
            Segment seg =  new Segment(Operation.Adding, Status.notautorized, ID.undefined, Factorial.notCalculate);
            buffer = BufferUtilites.ToBuffer(seg._bitAR);
            return buffer;
        }

    }


}
