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
                while (true)
                {
                    s.StartListen();        // rozpoczenie nasluchiwania
                    s.AccecptClient();      // akceptacja klienta, ktory probuje nawiazac polaczenie
                    Console.WriteLine("Client connected");  // informacja o poprawnym nawiazaniu polaczenia
                    Segment seg;
                    s.CreateStream();       // utworzenie strumienia
                    s.buffer = new byte[16];         // ustalenie wielkosci bufora
                    s.Read(ref s.buffer);
                    seg = new Segment(s.buffer);
                    Console.WriteLine("ID otrzymane:\n{0}\n{1}", seg.ReadSegment(), BufferUtilites.ReadBuffer(s.buffer), seg._bitAR.Length);     // wyswietlenie segmentu
                    s.buffer = s.IDRequest();   //tworzenie odpowiedzi na przydzielenie id sesji
                    s.Write(s.IDRequest());
                    seg = new Segment(s.buffer);
                    Console.WriteLine("ID wyslane:\n{0}\n{1}", seg.ReadSegment(), BufferUtilites.ReadBuffer(s.buffer), seg._bitAR.Length);     // wyswietlenie segmentu
                    while (true)
                    {
                        s.buffer = new byte[16];    //reset buffera
                        s.Read(ref s.buffer);   // odczytanie wiadomosci
                        seg = new Segment(s.buffer);
                        if (s.CheckExit(seg) == true)
                        {
                            Console.WriteLine("EXIT!");
                            break;
                        }
                        Console.WriteLine("Segment otrzymany:\n{0}\n{1}", seg.ReadSegment(), BufferUtilites.ReadBuffer(s.buffer), seg._bitAR.Length);     // wyswietlenie segmentu


                        seg = s.MakeAnswer(s.Calculate(seg));
                        s.buffer = BufferUtilites.ToBuffer(seg._bitAR);
                        s.Write(s.buffer);  //wyslanie odpowiedzi
                        Console.WriteLine("Segment wysłany:\n{0}\n{1}", seg.ReadSegment(), BufferUtilites.ReadBuffer(s.buffer), seg._bitAR.Length);     // wyswietlenie segmentu
                                                                                                                                                        //Console.WriteLine("Długość {0}", seg._bitAR.Length);
                    }
                    s.Exit();   // bezpieczne zakonczenie polaczenia
                }
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
