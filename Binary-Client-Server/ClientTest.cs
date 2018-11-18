using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Binary_Client_Server 
{
    class ClientTest
    {
        private static void Main(string[] args)     // metoda do sprawdzania dzialania klienta
        {

            Client c = new Client();        // utworzenie obiektu klienta (bezargumentowo do testow)
            string UserInput;
            try
            {
                while (true)    // do testow
                {
                    UserInput = Console.ReadLine();  //wczytanie danych do wyslania
                    Segment s = new Segment(Regex.Split(UserInput, "\\s+"));
                    string[] seg = s.Encoding();
                    BitArray arr = s.GetBuffer();

                    foreach (string str in arr)
                    {
                        Console.Write(str);
                    }

                }


                Console.WriteLine("Client");    // informacja o utworzeniu obiektu klienta
                c.Createclient();               // utworzenie klienta, port 27015
                c.CreateStream();               // utworzenie strumienia z serwerem
                Console.WriteLine("Connected"); // informacja o polaczeniu
                //TODO: wczytywanie dwoch liczb, zapisanie ich do BitArray[] 
                //TODO: oraz przeksztalcenie do postaci binarnej

                c.buffer = new byte[UserInput.Length];  // ustalenie wielkosci bufora
                //TODO: wielkosc bufora zalezna od wielkosci wpisanych przez uzytkownika zmiennych
                
                for (int i = 0; i < UserInput.Length; i++)  // wpisyawnie do bufora
                    c.buffer[i] = Convert.ToByte(UserInput.ElementAt(i));
                
                c.Write(ref c.buffer);  // wysylanie
                Console.WriteLine("Message sended: {0}", BufferUtilites.ReadMessage(c.buffer));
                c.Read(ref c.buffer);   // odbieranie
                Console.WriteLine("Message received: {0}", BufferUtilites.ReadMessage(c.buffer));
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
