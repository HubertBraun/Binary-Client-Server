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
                s.buffer = new byte[8];         // ustalenie wielkosci bufora
                //TODO: bufor moze miec zmienna wielkosc
                s.Read(ref s.buffer);   //odczytanie wiadomosci
                Console.WriteLine("Message received: {0}", BufferUtilites.ReadMessage(s.buffer));
                //TODO: sprawdzenie pierwszych 3 bitow (operacja), implementacja do kazdej operacji osobnej metody
                //TODO: byte[] NazwaOperacji(liczba1, liczba2);
                
                s.Write(ref s.buffer);  //wyslanie odpowiedzi
                Console.WriteLine("Message sended: {0}", BufferUtilites.ReadMessage(s.buffer));   
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
