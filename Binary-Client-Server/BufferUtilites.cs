using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{
    static class BufferUtilites    // klasa do operacji na buforze
    {
        public static string ReadBuffer(byte[] buffer)      // zamiana bufora na string
        {
            string tempString = " ";    
            int tempInt;
            string[] signs = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            for (int i = 0; i < buffer.Length; i++)
            {
                tempInt = buffer[i] >> 4;       // pozbycie sie mniej znaczacego bitu
                tempString += signs[tempInt];   // wyswietlenie najbardziej znaczacego bitu
                tempInt = buffer[i] & 0xF;      // pozbycie sie najbardziej znaczacego bitu
                tempString += signs[tempInt] + " "; // wysietlenie mniej znaczacego bitu
            }

            return tempString;
        }

        public static byte[] ToBuffer(BitArray bits)
        {
            byte[] ret = new byte[(bits.Length - 1) / 8 + 1];
            bits.CopyTo(ret, 0);
            return ret;

        }
    }
}
