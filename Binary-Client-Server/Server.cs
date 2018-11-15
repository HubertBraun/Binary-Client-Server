using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{
    class Server : Host
    {
        public Server()
        {
            _IP = System.Net.IPAddress.Parse("127.0.0.1");
        }


        public override bool Exit()
        {
            throw new NotImplementedException();
        }
    }
}
