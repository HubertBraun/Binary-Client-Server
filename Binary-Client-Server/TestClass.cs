

#define ClientMode 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{


    class TestClass
    {
        static string ReadBuffer(byte[] buffer)
        {
            string temp = " ";
            string[] signs = new string[]{"0","1","2","3","4","5","6","7","8","9","A","B","C","D","E","F"};
            for (int i=0; i<buffer.Length; i++)
            {
                temp += signs[buffer[i]];
            }

            return temp;
        }

        private static void Main(string[] args)
        {
            byte[] buffer = new byte[8];

        #if (ClientMode)
            {
                Client c = new Client();
                c.Createclient();
                c.CreateStream();
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = 0x0;
                }
                try
                {
                    c.Write(ref buffer);
                    Console.WriteLine("Message sended: {0}", ReadBuffer(buffer));
                    c.Read(ref buffer);
                    Console.WriteLine("Message received: {0}", ReadBuffer(buffer));
                    c.Exit();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                }
#else
            {
                Server s = new Server();
                Console.WriteLine("Waiting for connection");
                s.CreateListener();
                s.StartListen();
                s.AccecptClient();
                Console.WriteLine("Client connected");
                s.CreateStream();
                try
                {
                    s.Read(ref buffer);
                    Console.WriteLine("Message received: {0}", ReadBuffer(buffer));
                    for (int i = 0; i < buffer.Length; i++)
                    {
                        buffer[i] = 0xF;
                    }
                    s.Write(ref buffer);
                    s.Exit();
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
#endif
            Console.ReadKey();
        }

    }
}
