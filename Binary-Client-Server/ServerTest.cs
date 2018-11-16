using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{
    class ServerTest : BufferUtilites
    {
        private static void Main(string[] args)
        {
            Server s = new Server();
            try
            {
                Console.WriteLine("Server");
                s.buffer = new byte[8];
                Console.WriteLine("Waiting for connection");
                s.CreateListener();
                s.StartListen();
                s.AccecptClient();
                Console.WriteLine("Client connected");
                s.CreateStream();

                s.Read(ref s.buffer);
                Console.WriteLine("Message received: {0}", ReadMessage(s.buffer));
                for (int i = 0; i < s.buffer.Length; i++)
                {
                    s.buffer[i] = 0xF0;
                }
                s.Write(ref s.buffer);
                Console.WriteLine("Message sended: {0}", ReadMessage(s.buffer));
                s.Exit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                s.Exit();
            }
            Console.ReadKey();
        }
    }

}
