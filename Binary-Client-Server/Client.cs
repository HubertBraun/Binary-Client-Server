using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{

    class Client : Host
    {
        public Client(string[] args)
        {
            if (args.Length == 1)
            {
                _IP = System.Net.IPAddress.Parse(args[0]);
                Console.WriteLine("Adres IP Servera: {0}", _IP);
            }

            else
            {
                Console.WriteLine("Prosze podac adres servera");
                _IP = System.Net.IPAddress.Parse(Console.ReadLine());
            }
        }


        public override bool Exit()
        {
            throw new NotImplementedException();
        }


    }


}
