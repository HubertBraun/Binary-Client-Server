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

        private int CalculateFactorial(int n)
        {
            if (n <= 1) return 1;
            else return checked(n * CalculateFactorial(n - 1));
        }

        public Tuple<string, string, int> Calculate(Segment seg)
        {
            string[] str = seg.Encoding();
            string operation = str[0];
            string status = str[1];

            int number1 = str[6].ConvertStringtoInt();    // pierwsza liczba
            int number2 = str[7].ConvertStringtoInt();    // druga liczba
            int toReturn = 0;
            
            
            if (str[4] == "0")  // jesli nie ustawiono silni
            {
                if (str[1] == BinaryMinimalizer.ReturnMinimalizedTable(Convert.ToInt32(Status.autorized)).ToDigitString())
                {
                    switch (operation)
                    {

                        case "000":    // dodawanie
                            toReturn = number1 + number2;
                            Console.WriteLine(number1 + "+" + number2 + "=" + toReturn);
                            break;
                        case "001":    // odejmowanie
                            toReturn = number1 - number2;
                            Console.WriteLine(number1 + "-" + number2 + "=" + toReturn);
                            break;
                        case "010":    // mnozenie
                            try
                            {
                                toReturn = checked(number1 * number2);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                status = "1100"; // overflow
                            }
                            Console.WriteLine(number1 + "*" + number2 + "=" + toReturn);
                            break;
                        case "011":    // dzielenie
                            try
                            {
                                toReturn = checked(number1 / number2);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                                status = "1110"; // notallowed
                            }
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
                        case "111":    // XOR
                            toReturn = number1 ^ number2;
                            Console.WriteLine(number1 + "^" + number2 + "=" + toReturn);
                            break;
                        default:
                            //TODO: TRZEBA ODESLAC WIADOMOSC
                            Console.WriteLine("Nierozpoznana operacja");
                            toReturn = -1;
                            break;

                    }

                }
                else
                {
                    status = "1111"; // notautorized

                }
                return new Tuple<string, string, int>(operation, status, toReturn);
            }
            else
            {
                try
                {
                    toReturn = CalculateFactorial(number1);
                }
                catch(Exception e)
                {
                    status = "1100"; // overflow
                    Console.WriteLine(e.Message);
                }
                return new Tuple<string, string, int>(operation, status, toReturn);
            }



        }

        public Segment MakeAnswer(Tuple<string, string , int> t)    // Operation, Status, wynik
        {

            Operation op = (Operation)t.Item1.ConvertStringtoInt();
            Status s = (Status)t.Item2.ConvertStringtoInt();
            if (t.Item3!=-1)    
            return new Segment(t.Item3, op, s, ID.defined, Factorial.notCalculate);
            else
                return new Segment(op, s, ID.defined, Factorial.notCalculate);

        }
        public byte[] IDRequest()
        {
            Segment seg = new Segment(Operation.Adding, Status.autorized, ID.defined, Factorial.notCalculate);
            buffer = BufferUtilites.ToBuffer(seg._bitAR);
            return buffer;
        }

    }
}
