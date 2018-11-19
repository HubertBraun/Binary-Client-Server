﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//TODO: impelentacja protokolu
// POLA OPERACJI                        POLA STATUSU            POLE DLUGOSCI DANYCH(32 bity)       POLE DANYCH
// 000 - dodawanie						0000                    0x00000002 - 2 zmienne po 1 bicie   zmienna dlugosc                      -							
// 001 - odejmowanie				    0001                    -                                   
// 010 - mnozenie						0010                    -                                   
// 011 - dzielenie                      0011                    -                                   
// 100 - AND                            0100                    -                                   
// 101 - OR                             0101                    -                                   
// 110 - porównywanie	0110                    -                                   
// 111 - potęgowanie   0111                    0xFFFFFFFF - 2 zmienne po 2^31             
namespace Binary_Client_Server
{
    public enum Operation
    {
        Adding = 0b000,
        Substracting = 0b001,
        Multiplicating = 0b010,
        Dividing = 0b011,
        Comparing = 0b110,
        Powering = 0b111,
        AND = 0b100,
        OR = 0b101

    }

    public enum Status
    {
        autorized = 0b1000, //poprawne dzialanie
        overflow = 0b1100,//przekroczenie zakresu
        notallowed = 0b1110,//niedozwolona operacja np. dzielenie przez zero
        //notdefined = 0b1111
        
    }
    public enum ID
    {
        undefined =0b100,
        defined = 0b111
    }
    public class Segment
    {
        private Operation _operation;//pole operacji
        private Status _status;//pole statusu


        private BitArray _data_length;//dlugosc pola danych
        private BitArray _id;
        private BitArray _ptrto_arg1_size;
        private BitArray _arg_1;//liczba1
        private BitArray _arg_2;//liczba2



        public BitArray _bitAR;

        public Segment(byte[] buffer)
        {
            _bitAR = new BitArray(buffer);
            //_bitAR = StringUtilities.BytetoBinTransfer(buffer);
        }

        public BitArray GetBuffer()
        {
            return _bitAR;
        }

        public Segment(int number, Operation op)
        {
            CreateBuffer(number, op, Status.autorized, ID.undefined);

        }

        public Segment(string[] arguments)
        {
            if (arguments.Length != 3)
                throw new ArgumentException("Nieprawidłowa liczba argumentów");
            Operation tempOperation;
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
                    tempOperation = Operation.Powering;
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

            CreateBuffer(Int32.Parse(arguments[0]), Int32.Parse(arguments[2]), tempOperation, Status.autorized, ID.undefined);

        }


            private int CalculateSegmentSize() { return 7 + _arg_1.Length + _arg_2.Length + _data_length.Length + _ptrto_arg1_size.Length; }

        public void CreateBuffer(int a, int b, Operation o, Status s, ID iden )   // dla dwoch liczb
        {
            //SSSS OOO DATA32PTR DATA 
            _id = BinaryMinimalizer.ReturnMinimalizedTable((Int32)iden);
            _arg_1 = BinaryMinimalizer.ReturnMinimalizedTable(a);//zminimalizowanie i zamiana liczb na ciag bitow
            _arg_2 = BinaryMinimalizer.ReturnMinimalizedTable(b);
            _ptrto_arg1_size = BinaryMinimalizer.ReturnMinimalizedTable(_arg_1.Length);
            _data_length = BinaryMinimalizer.Change(new BitArray(new int[] { _arg_1.Length + _arg_2.Length + 8}));//minimalizacja bitow ptr
            _operation = o;//przypisanie pol
            _status = s;
            

            
            //zamina BitArray na string 5
            string bufer ="";
            //Operacja
            string op = BinaryMinimalizer.ReturnMinimalizedTable(Convert.ToInt32(_operation)).ToDigitString();
            if (op.Length < 3) op = op.PadLeft(3, '0');
            bufer += op;

            //Status
            bufer += BinaryMinimalizer.ReturnMinimalizedTable(Convert.ToInt32(_status)).ToDigitString();
            //dane
            bufer += _data_length.ToDigitString();
            //Id
            bufer += _id.ToDigitString();
            //PTR
            string ptr1 = BinaryMinimalizer.ReturnMinimalizedTable(Convert.ToInt32(_arg_1.Length)).ToDigitString();
            if (ptr1.Length < 5) ptr1 = ptr1.PadLeft(5, '0');
            bufer += ptr1;
            //args
            bufer += _arg_1.ToDigitString();
            bufer += _arg_2.ToDigitString();
            //zamina string na bitarray
            var temp = new BitArray(bufer.Select(c => c == '1').ToArray());
            _bitAR = new BitArray(temp);
            
            //OOO SSSS DATA32 III PPPPP ARGS
        }

        public void CreateBuffer(int a, Operation o, Status s, ID iden)      // dla jednej liczby
        {
            //SSSS OOO DATA32PTR DATA 

            
            _id = BinaryMinimalizer.ReturnMinimalizedTable((Int32)iden);
            _arg_1 = BinaryMinimalizer.ReturnMinimalizedTable(a);//zminimalizowanie i zamiana liczb na ciag bitow
            _ptrto_arg1_size = BinaryMinimalizer.ReturnMinimalizedTable(_arg_1.Length);
            _data_length = BinaryMinimalizer.Change(new BitArray(new int[] { _arg_1.Length  + 8 }));//minimalizacja bitow ptr
            _operation = o;//przypisanie pol
            _status = s;

            //zamina BitArray na string 5
            string bufer = "";
            //zmiana enum na bity
            string op = BinaryMinimalizer.ReturnMinimalizedTable(Convert.ToInt32(_operation)).ToDigitString();
            if (op.Length < 3) op = op.PadLeft(3, '0');
            bufer += op;
          
            //Status
            bufer += BinaryMinimalizer.ReturnMinimalizedTable(Convert.ToInt32(_status)).ToDigitString();
            //dane
            bufer += _data_length.ToDigitString();
            //Id
            bufer += _id.ToDigitString();
            //PTR
            string ptr1 = BinaryMinimalizer.ReturnMinimalizedTable(Convert.ToInt32(_arg_1.Length)).ToDigitString();
            if (ptr1.Length < 5) ptr1 = ptr1.PadLeft(5, '0');
            bufer += ptr1;
            //args
            bufer += _arg_1.ToDigitString();
            //bufer += _arg_2.ToDigitString();
            //zamina string na bitarray
            var temp = new BitArray(bufer.Select(c => c == '1').ToArray());
            _bitAR = new BitArray(temp);


        }
        //TODO: Ogarnij te wywolanie Encoding, bo zmienily sie indeksy tablicy
        public string[] Encoding()//zwracanie tablicy stringow po enkodowaniu
        {
            //OOO SSSS DATA32 III PPPPP ARGS
            var temp = new BitArray(_bitAR);
            string ar = temp.ToDigitString();
            string[] toReturn = new string[7];
            toReturn[0] = ar.Substring(0, 3);//operacja
            toReturn[1] = ar.Substring(3, 4);//stan
            toReturn[2] = ar.Substring(7, 32);//dlugosc danych
            toReturn[3] = ar.Substring(39, 3);//id
            toReturn[4] = ar.Substring(42, 5);//wskaznik danych arg1
            int index_ptr = 0; int length_value = 0;//dl liczby1 ; dl liczby 1 i 2
            index_ptr = StringUtilities.ConvertStringtoInt(toReturn[4]); length_value = StringUtilities.ConvertStringtoInt(toReturn[2]);
            toReturn[5] = ar.Substring(47, index_ptr);//liczba1
            toReturn[6] = ar.Substring(47 + index_ptr, length_value - index_ptr - 8);//liczba2


            //for (int i = 0; i < temp.Length; i++)
            //{
            //    if (i < 3) toReturn[0] += Convert.ToInt32(temp.Get(i));
            //    if (i >= 3 && i < 7) toReturn[1] += Convert.ToInt32(temp.Get(i));
            //    if (i >= 7 && i < 39) toReturn[2] += Convert.ToInt32(temp.Get(i));
            //    if (i >= 39 && i < 71) toReturn[3] += Convert.ToInt32(temp.Get(i));
            //    if (i >= 71 && i < (71 + StringUtilities.Convert(toReturn[3]))) toReturn[4] += Convert.ToInt32(temp.Get(i));
            //    if (i >= (71 + index_ptr) && i < (71 + StringUtilities.Convert(toReturn[2]))) toReturn[5] += Convert.ToInt32(temp.Get(i));
            //}
            //Console.WriteLine(StringUtilities.Convert(toReturn[3]) + "   " +  StringUtilities.Convert(toReturn[3]));

            return toReturn;
            
        }



        public string ReadSegment()
        {
            string[] temp = this.Encoding();
            StringBuilder Builder = new StringBuilder();
            int i = 0;
            foreach (string str in temp)
            {

                Builder.Append(i + ": " + str +"   size: " +str.Length + "\n");
                i++;
            }


            return Builder.ToString();
        }




    }




}
