using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Binary_Client_Server 
{
    class ClientTest
    {
        private static string[] ReadUserInput()
        {
            Regex reg = new Regex("(\\d+)\\s*?(\\D)\\s*?(\\d+)");
            string UserInput = Console.ReadLine();  // wczytanie danych do wyslania
            Segment s;
            string[] str = new string[1];
            if (UserInput.ToLower() == "exit")
            {
                str[0] = "exit";
                s = new Segment(str);
                
                return str;
            }
            Match m = reg.Match(UserInput);
            GroupCollection groups = m.Groups;
            if (m.Groups.Count == 4)
            {
                //Console.WriteLine(m.Groups[1].Value);
                //Console.WriteLine(m.Groups[2].Value);
                //Console.WriteLine(m.Groups[3].Value);
                str = new string[3];
                str[0] = m.Groups[1].Value;     // pierwsza liczba
                str[1] = m.Groups[2].Value;     // operacja matematyczna
                str[2] = m.Groups[3].Value;     // druga liczba
                s = new Segment(str);
            }
            else
            {
                reg = new Regex("(\\d+)\\s*?(\\D)");
                m = reg.Match(UserInput);
                groups = m.Groups;
                str = new string[2];
                str[0] = m.Groups[1].Value;     // pierwsza liczba
                str[1] = m.Groups[2].Value;     // operacja matematyczna
                s = new Segment(str);
            }

            return str;
        }

        private static void Main(string[] args)     // metoda do sprawdzania dzialania klienta
        {
 
            Client c = new Client();        // utworzenie obiektu klienta (bezargumentowo do testow)
            try
            {
                Console.WriteLine("Client");    // informacja o utworzeniu obiektu klienta
                c.Createclient();               // utworzenie klienta, port 27015
                c.CreateStream();               // utworzenie strumienia z serwerem
                Console.WriteLine("Connected"); // informacja o polaczeniu
                c.buffer = c.IDRequest();
                c.Write(c.buffer);
                Segment seg = new Segment(c.buffer);
                Console.WriteLine("Żądanie ID wysłane:\n{0}\n{1}", seg.ReadSegment(), BufferUtilites.ReadBuffer(c.buffer), seg._BitAR.Length);     // wyswietlenie segmentu
                c.buffer = new byte[16];
                c.Read(ref c.buffer);
                seg = new Segment(c.buffer);
                Console.WriteLine("ID otrzymane:\n{0}\n{1}", seg.ReadSegment(), BufferUtilites.ReadBuffer(c.buffer), seg._BitAR.Length);     // wyswietlenie segmentu

                string[] UserInput;  //wczytanie danych do wyslania

               //TODO: WAZNE - PRZERWANIE PETLI
                while (true)
                {
                    UserInput = ReadUserInput();
                    seg = new Segment(UserInput);

                    c.buffer = BufferUtilites.ToBuffer(seg._BitAR);   
                    c.Write(c.buffer);  // wysylanie
                    if(UserInput[0] == "exit")
                    {
                        Console.WriteLine("EXIT!");
                        break;
                    }
                    Console.WriteLine("Segment wysłany:\n{0}\n{1}", seg.ReadSegment(), BufferUtilites.ReadBuffer(c.buffer), seg._BitAR.Length);     // wyswietlenie segmentu
                    c.buffer = new byte[16];
                    c.Read(ref c.buffer);   // odbieranie
                    seg = new Segment(c.buffer);
                    Console.WriteLine("Segment otrzymany:\n{0}\n{1}", seg.ReadSegment(), BufferUtilites.ReadBuffer(c.buffer), seg._BitAR.Length);     // wyswietlenie segmentu

                    switch(c.ReadAnswer(seg))   // wyswietlenie odpowiedzi od serwera
                    {
                        case -2:    
                            Console.WriteLine("OVERFLOW");
                            break;
                        case -3:
                            Console.WriteLine("NOTALLOWED");
                            break;
                        default:
                            Console.WriteLine("Serwer: {0}", c.ReadAnswer(seg));    // poprawna odpowiedz
                            break;
                    }

                }
                
                c.Exit();   // bezpieczne zakonczenie polaczenia
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                c.Exit();   // awaryjne zakonczenie polaczenia
            }
            Console.ReadKey();  // zamykanie aplikacji przez wcisniecie dowolnego przycisku
        }
    }
}
