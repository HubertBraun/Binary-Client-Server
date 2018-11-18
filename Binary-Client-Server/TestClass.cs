using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Binary_Client_Server
{
    class TestClass 
    {

        private static void Main()
        {
            
            try
            {

                while (true)
                {
                    string UserInput = Console.ReadLine();  //wczytanie danych do wyslania
                    Segment s = new Segment(Regex.Split(UserInput, "\\s+"));
                    string[] seg = s.Encoding();

                   
                    foreach (var b in seg) Console.WriteLine(b);
                }
                }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
                Console.ReadKey();  // zamykanie aplikacji przez wcisniecie dowolnego przycisku
        }

    }


}
