using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


//POLA OPERACJI                         POLA STATUSU            POLE DLUGOSCI DANYCH(32 bity)       POLE DANYCH
//000 - dodawanie						0000                    0x0000000F                          -							
//001 - odejmowanie					    0001                    -                                   -
//010 - mnozenie						0010                    -                                   -
//011 - modulo ew.dzielenie             0011                    -                                   -
//100 - AND                             0100                    -                                   -
//101 - OR                              0101                    -                                   -
//110 - przesuniecie bitowe w prawo	    0110                    -                                   -
//111 - przesuniecie bitowe w lewo	    0111                    0xFFFFFFFF                          -

namespace Binary_Client_Server
{
    abstract class Host
    {
        private const int portNum = 27015;
        protected IPAddress _IP;
        abstract public bool Exit();
    }
}
