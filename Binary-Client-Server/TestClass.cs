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
        private static string[] ReadUserInput()
        {
            Regex reg = new Regex("(\\d+)\\s*?(\\D)\\s*?(\\d+)");
            string UserInput = Console.ReadLine();  // wczytanie danych do wyslania
            Segment s;
            Match m = reg.Match(UserInput);
            GroupCollection groups = m.Groups;
            string[] str = new string[1];
            if (m.Groups.Count == 4)
            { Console.WriteLine(m.Groups[1].Value);
                Console.WriteLine(m.Groups[2].Value);
                Console.WriteLine(m.Groups[3].Value);
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
                Console.WriteLine(m.Groups[1].Value);
                Console.WriteLine(m.Groups[2].Value);
                Console.WriteLine(m.Groups[3].Value);
                str = new string[2];
                str[0] = m.Groups[1].Value;     // pierwsza liczba
                str[1] = m.Groups[2].Value;     // operacja matematyczna
                Console.WriteLine("Silnia!");
                s = new Segment(str);
            }

            return str;
        }


        private static void Main()
        {
            
            try
            {
                string[] UserInput;
                Console.WriteLine("asdd");
                UserInput = TestClass.ReadUserInput();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
                Console.ReadKey();  // zamykanie aplikacji przez wcisniecie dowolnego przycisku
        }

    }


}
