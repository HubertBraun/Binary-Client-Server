using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace Binary_Client_Server
{
    static class StringUtilities
    {
        public static string ToDigitString(this BitArray array)
        {
            var builder = new StringBuilder();
            foreach (var bit in array.Cast<bool>())
                builder.Append(bit ? "1" : "0");
            return builder.ToString();
        }


        public static int ConvertStringtoInt(this string str1)
        {
            if (str1 == "")
                throw new Exception("Invalid input");
            int val = 0, res = 0;

            for (int i = 0; i < str1.Length; i++)
            {
                try
                {
                    val = Int32.Parse(str1[i].ToString());
                    if (val == 1)
                        res += (int)Math.Pow(2, str1.Length - 1 - i);
                    else if (val > 1)
                        throw new Exception("Invalid!");
                }
                catch
                {
                    throw new Exception("Invalid!");
                }
            }
            return res;
        }
    }
}
