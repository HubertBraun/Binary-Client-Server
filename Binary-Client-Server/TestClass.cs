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
                    string test = s._bitAR.Length.ToString();
                    int i = 0;
                    Console.WriteLine(s._bitAR.Length);
                    foreach (var c in s._bitAR.ToDigitString()) Console.Write(c);
                    Console.WriteLine();
                    foreach (var sx in seg)
                    {
                        Console.WriteLine(i + ": " + sx + "       size: " + sx.Length);
                        i++;
                    
                    }
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
