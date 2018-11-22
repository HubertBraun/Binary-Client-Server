using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Binary_Client_Server
{




    class SegmentRepaired
    {

        private string _operation;//pole operacji
        private string _status;//pole statusu
        private string _data_length;//dlugosc pola danych
        private string _id;
        private string _fac;
        private string _ptrto_arg1_size;
        private string _arg_1;//liczba1
        private string _arg_2;//liczba2

        public byte[] _BitAR;
        public SegmentRepaired(string[] arguments)
        {
            if (arguments[0] == "exit")
            {
                CreateBuffer(0, 0, Operation.Adding, Status.exit, ID.defined, Factorial.Calculate);
                return;
            }

            if (arguments.Length < 2 || arguments.Length > 3)
                throw new ArgumentException("Nieprawidłowa liczba argumentów");
            Operation tempOperation = Operation.Adding;
            if (arguments.Length == 3)
            {
                switch (arguments[1])
                {
                    case "+":
                        tempOperation = Operation.Adding;
                        break;
                    case "-":
                        tempOperation = Operation.Substracting;
                        break;
                    case "*":
                        tempOperation = Operation.Multiplicating;
                        break;
                    case "/":
                        tempOperation = Operation.Dividing;
                        break;
                    case "==":
                        tempOperation = Operation.Comparing;
                        break;
                    case "^":
                        tempOperation = Operation.XOR;
                        break;
                    case "&":
                        tempOperation = Operation.AND;
                        break;
                    case "|":
                        tempOperation = Operation.OR;
                        break;
                    default:
                        throw new ArgumentException("Nierozpoznana operacja matematyczna");

                }
                CreateBuffer(Int32.Parse(arguments[0]), Int32.Parse(arguments[2]), tempOperation, Status.autorized, ID.defined, Factorial.notCalculate);
            }
            else if (arguments.Length == 2 && arguments[1] == "!")
            {
                tempOperation = Operation.Adding;
                CreateBuffer(Int32.Parse(arguments[0]), 0, tempOperation, Status.autorized, ID.defined, Factorial.Calculate);
            }
            else
                throw new ArgumentException("Nierozpoznana operacja matematyczna");

        }
        public void CreateBuffer(int a, int b, Operation o, Status s, ID iden, Factorial f)   // dla dwoch liczb
        {

            _arg_1 = Convert.ToString(a, 2);
            _arg_2 = Convert.ToString(b, 2);
            _status = Convert.ToString(Convert.ToInt32(s), 2);
            _operation = Convert.ToString(Convert.ToInt32(o), 2);
            _id = Convert.ToString(Convert.ToInt32(iden), 2);
            _fac = Convert.ToString(Convert.ToInt32(f), 2);
            _ptrto_arg1_size = Convert.ToString(_arg_1.Length, 2);
            _data_length = Convert.ToString(_arg_1.Length + _arg_2.Length + 8, 2);

            //zamina BitArray na string 5
            string bufer = "";
            
            if (_operation.Length < 3) _operation = _operation.PadLeft(3, '0');
            bufer += _operation;
            bufer += _status;
            if (_data_length.Length < 32) _data_length = _data_length.PadLeft(32, '0');
            bufer += _data_length;
            bufer += _id;
            bufer += _fac;
            if (_ptrto_arg1_size.Length < 5) _ptrto_arg1_size = _ptrto_arg1_size.PadLeft(5, '0');
            bufer += _ptrto_arg1_size;
            bufer += _arg_1;
            bufer += _arg_2;
            
            _BitAR = Encoding.ASCII.GetBytes(bufer);
            
        }


        public void CreateBuffer(int a, Operation o, Status s, ID iden, Factorial f)      // dla jednej liczby
        {
            //SSSS OOO DATA32PTR DATA 
            _arg_1 = Convert.ToString(a, 2);
            _status = Convert.ToString(Convert.ToInt32(s), 2);
            _operation = Convert.ToString(Convert.ToInt32(o), 2);
            _id = Convert.ToString(Convert.ToInt32(iden), 2);
            _fac = Convert.ToString(Convert.ToInt32(f), 2);
            _ptrto_arg1_size = Convert.ToString(_arg_1.Length, 2);
            _data_length = Convert.ToString(_arg_1.Length +  8, 2);

            //zamina BitArray na string 5
            string bufer = "";

            if (_operation.Length < 3) _operation = _operation.PadLeft(3, '0');
            bufer += _operation;
            bufer += _status;
            if (_data_length.Length < 32) _data_length = _data_length.PadLeft(32, '0');
            bufer += _data_length;
            bufer += _id;
            bufer += _fac;
            if (_ptrto_arg1_size.Length < 5) _ptrto_arg1_size = _ptrto_arg1_size.PadLeft(5, '0');
            bufer += _ptrto_arg1_size;
            bufer += _arg_1;
      
            _BitAR = Encoding.ASCII.GetBytes(bufer);


        }

        public void CreateBuffer(Operation o, Status s, ID iden, Factorial f)   // bez argumentow
        {
            _status = Convert.ToString(Convert.ToInt32(s), 2);
            _operation = Convert.ToString(Convert.ToInt32(o), 2);
            _id = Convert.ToString(Convert.ToInt32(iden), 2);
            _fac = Convert.ToString(Convert.ToInt32(f), 2);
            _ptrto_arg1_size = Convert.ToString(0, 2);
            _data_length = Convert.ToString(8, 2);

            //zamina BitArray na string 5
            string bufer = "";

            if (_operation.Length < 3) _operation = _operation.PadLeft(3, '0');
            bufer += _operation;
            bufer += _status;
            if (_data_length.Length < 32) _data_length = _data_length.PadLeft(32, '0');
            bufer += _data_length;
            bufer += _id;
            bufer += _fac;
            if (_ptrto_arg1_size.Length < 5) _ptrto_arg1_size = _ptrto_arg1_size.PadLeft(5, '0');
            bufer += _ptrto_arg1_size;
            
            _BitAR = Encoding.ASCII.GetBytes(bufer);
        }

        public String[] ReturnEncoder()
        {
            var toReturn = new String[8];
            var ar  = System.Text.Encoding.UTF8.GetString(_BitAR);
            toReturn[0] = ar.Substring(0, 3);//operacja
            toReturn[1] = ar.Substring(3, 4);//stan
            toReturn[2] = ar.Substring(7, 32);//dlugosc danych
            toReturn[3] = ar.Substring(39, 2);//id
            toReturn[4] = ar.Substring(41, 1);//factorial
            toReturn[5] = ar.Substring(42, 5);//wskaznik danych arg1
            int index_ptr = 0; int length_value = 0;//dl liczby1 ; dl liczby 1 i 2
            index_ptr = StringUtilities.ConvertStringtoInt(toReturn[5]); length_value = StringUtilities.ConvertStringtoInt(toReturn[2]);
            toReturn[6] = ar.Substring(47, index_ptr);//liczba1
            toReturn[7] = ar.Substring(47 + index_ptr, length_value - index_ptr - 8);//liczba2

            return toReturn;
        }
    }


}
