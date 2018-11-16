using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server 
{
    class ClientTest : BufferUtilites
    {
        private static void Main(string[] args)
        {
            Client c = new Client();
            try
            {
                Console.WriteLine("Client");
                c.buffer = new byte[8];
                c.Createclient();
                c.CreateStream();
                Console.WriteLine("Connected");
                for (int i = 0; i < c.buffer.Length; i++)
                {
                    c.buffer[i] = 0x00;
                }
                c.Write(ref c.buffer);
                Console.WriteLine("Message sended: {0}", ReadMessage(c.buffer));
                c.Read(ref c.buffer);
                Console.WriteLine("Message received: {0}", ReadMessage(c.buffer));
                c.Exit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                c.Exit();
            }
            Console.ReadKey();
        }
    }
}
