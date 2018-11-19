using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{
    class ServerTest
    {

        private static void Main(string[] args)
        {
            Server s = new Server();    // utworzenie obiektu serwera 
            try
            {
                Console.WriteLine("Server");    // informacja o utworzeniu obiektu serwera
                Console.WriteLine("Waiting for connection");
                s.CreateListener();     // przypisanie portu (27015)     
                s.StartListen();        // rozpoczenie nasluchiwania
                s.AccecptClient();      // akceptacja klienta, ktory probuje nawiazac polaczenie
                Console.WriteLine("Client connected");  // informacja o poprawnym nawiazaniu polaczenia
                s.CreateStream();       // utworzenie strumienia
                s.buffer = new byte[16];         // ustalenie wielkosci bufora
                while (true)
                {
                    s.Read(ref s.buffer);   // odczytanie wiadomosci
                    Segment seg = new Segment(s.buffer);

                    Console.WriteLine(seg.ReadSegment());   //odczytanie segmentu
                    Console.WriteLine("Message received: {0}", BufferUtilites.ReadMessage(s.buffer));   // wyswiwietlenie segmentu w postaci szesnastkowej

                    s.Write(ref s.buffer);  //wyslanie odpowiedzi
                }
                s.Exit();   // bezpieczne zakonczenie polaczenia
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                s.Exit();   // awaryjne zakonczenie polaczenia
            }
            Console.ReadKey();   // zamykanie aplikacji przez wcisniecie dowolnego przycisku
        }
    }

}
