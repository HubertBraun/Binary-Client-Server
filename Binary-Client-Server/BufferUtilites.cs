using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{
    class BufferUtilites
    {
        public static string ReadBuffer(byte[] buffer)
        {
            string tempString = " ";
            int tempInt;
            string[] signs = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };
            for (int i = 0; i < buffer.Length; i++)
            {
                tempInt = buffer[i] >> 4;
                tempString += signs[tempInt];
                tempInt = buffer[i] & 0xF;
                tempString += signs[tempInt] + " ";
            }

            return tempString;
        }

        public static string ReadMessage(byte[] buffer)
        {
            string tempString = ReadBuffer(buffer);
            tempString += "\n" + buffer.Length + " bytes\n";

            return tempString;
        }

    }
}
