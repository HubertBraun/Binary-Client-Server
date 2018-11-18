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


        public static int ConvertStringtoInt(string str1)
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

        public static BitArray BytetoBinTransfer(byte[] bt)
        {
            BitArray b;
            string strBin = string.Empty;
            byte btindx = 0;
            string strAllbin = string.Empty;

            for (int i = 0; i < bt.Length; i++)
            {
                btindx = bt[i];

                strBin = Convert.ToString(btindx, 2); // Convert from Byte to Bin
                strBin = strBin.PadLeft(8, '0');  // Zero Pad

                strAllbin += strBin;
            }
            b = new BitArray(strAllbin.Select(c => c == '1').ToArray());
            return b;
        }
    }
}
