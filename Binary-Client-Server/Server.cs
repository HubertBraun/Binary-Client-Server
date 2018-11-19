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

        public Tuple<string, int> Calculate(Segment seg)
        {
            string[] str = seg.Encoding();
            string operation = str[0];
            int number1 = str[4].ConvertStringtoInt();    // pierwsza liczba
            int number2 = str[5].ConvertStringtoInt();    // druga liczba
            int toReturn = 0;
            switch(operation)
            {
                case "000":    // dodawanie
                    toReturn = number1 + number2;
                    Console.WriteLine(number1 + "+" + number2 +  "=" +  toReturn);
                    break;
                case "001":    // odejmowanie
                    toReturn = number1 - number2;
                    Console.WriteLine(number1 + "-" + number2 + "=" + toReturn);
                    break;
                case "010":    // mnozenie
                    toReturn = number1 * number2;
                    Console.WriteLine(number1 + "*" + number2 + "=" + toReturn);
                    break;
                case "011":    // dzielenie
                    toReturn = number1 / number2;
                    Console.WriteLine(number1 + "/" + number2 + "=" + toReturn);
                    break;
                case "100":    // AND
                    toReturn = number1 & number2;
                    Console.WriteLine(number1 + "&" + number2 + "=" + toReturn);
                    break;
                case "101":    // OR
                    toReturn = number1 | number2;
                    Console.WriteLine(number1 + "|" + number2 + "=" + toReturn);
                    break;
                case "110":    // porownywanie
                    if (number1 == number2)
                        toReturn = 1;
                    else
                        toReturn = 0;

                    Console.WriteLine(number1 + "==" + number2 + "=" + toReturn);
                    break;
                case "111":    // potegowanie
                    toReturn = number1 ^ number2;
                    Console.WriteLine(number1 + "^" + number2 + "=" + toReturn);
                    break;
                default:
                    Console.WriteLine("Nierozpoznana operacja");
                    break;

            }
            return new Tuple<string, int>(operation, toReturn);

        }

        public Segment MakeAnswer(Tuple<string, int> t)
        {

            Operation op = (Operation)t.Item1.ConvertStringtoInt();
            Segment seg = new Segment(t.Item2, op);
            return seg;
        }

    }
}
