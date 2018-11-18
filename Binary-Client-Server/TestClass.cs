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
                bool run = true;
                while (run)
                {
                    //BitArray b;
                    //byte[] bt = new byte[2] { 1, 2 };

                    //b = StringUtilities.BytetoBinTransfer(bt);
                    //run = false;
                    //foreach (var i in b) Console.Write(Convert.ToInt32(i));

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
