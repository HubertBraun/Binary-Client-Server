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

            Match m = reg.Match(UserInput);
            GroupCollection groups = m.Groups;

            string[] str = new string[3];
            str[0] = m.Groups[1].Value;     // pierwsza liczba
            str[1] = m.Groups[2].Value;     // operacja matematyczna
            str[2] = m.Groups[3].Value;     // druga liczba
            Segment s = new Segment(str);
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

                string[] UserInput;  //wczytanie danych do wyslania
                while (true)
                {
                    UserInput = ReadUserInput();
                    Segment seg = new Segment(UserInput);

                    Console.WriteLine(seg.ReadSegment());     // wyswietlenie segmentu
                    c.buffer = BufferUtilites.ToBuffer(seg._bitAR);   
                    Console.WriteLine("Message sended: {0}", BufferUtilites.ReadMessage(c.buffer)); // wyswiwietlenie segmentu w postaci szesnastkowej
                    c.Write(ref c.buffer);  // wysylanie
                    c.buffer = new byte[32];
                    c.Read(ref c.buffer);   // odbieranie
                    Console.WriteLine("Message received: {0}", BufferUtilites.ReadMessage(c.buffer)); // wyswiwietlenie segmentu w postaci szesnastkowej
                    seg = new Segment(c.buffer);
                    Console.WriteLine(seg.ReadSegment());     // wyswietlenie segmentu
                    Console.WriteLine("Serwer: {0}",c.ReadAnswer(seg));
                    //Console.WriteLine("Message received: {0}", BufferUtilites.ReadMessage(c.buffer));
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
