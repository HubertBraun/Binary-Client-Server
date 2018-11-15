

//#define ClientMode 

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
            string tempString = " ";
            int tempInt;
            string[] signs = new string[]{"0","1","2","3","4","5","6","7","8","9","A","B","C","D","E","F"};
            for (int i=0; i<buffer.Length; i++)
            {
                tempInt = buffer[i] >> 4;
                tempString += signs[tempInt];
                tempInt = buffer[i] & 0xF;
                tempString += signs[tempInt] + " ";
            }

            return tempString;
        }

        static private void ClientTest(ref Client c)
        {
            Console.WriteLine("Server");

            c.buffer = new byte[8];
            c.Createclient();
            c.CreateStream();
            Console.WriteLine("Connected");
            for (int i = 0; i < c.buffer.Length; i++)
            {
                c.buffer[i] = 0x00;
            }
            c.Write(ref c.buffer);
            Console.WriteLine("Message sended: {0}", ReadBuffer(c.buffer));
            c.Read(ref c.buffer);
            Console.WriteLine("Message received: {0}", ReadBuffer(c.buffer));
            c.Exit();
        }

        static private void ServerTest(ref Server s)
        {
            Console.WriteLine("Client");
            s.buffer = new byte[8];
            Console.WriteLine("Waiting for connection");
            s.CreateListener();
            s.StartListen();
            s.AccecptClient();
            Console.WriteLine("Client connected");
            s.CreateStream();

            s.Read(ref s.buffer);
            Console.WriteLine("Message received: {0}", ReadBuffer(s.buffer));
            for (int i = 0; i < s.buffer.Length; i++)
            {
                s.buffer[i] = 0xF0;
            }
            s.Write(ref s.buffer);
            Console.WriteLine("Message sended: {0}", ReadBuffer(s.buffer));
            s.Exit();
        }



        private static void Main(string[] args)
        {

#if (ClientMode)
            Client c = new Client();
            try
            {
                ClientTest(ref c);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                c.Exit();
            }

#else
            Server s = new Server();
            try
            {
                ServerTest(ref s);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                s.Exit();
            }
#endif

            Console.ReadKey();
        }

    }
}
