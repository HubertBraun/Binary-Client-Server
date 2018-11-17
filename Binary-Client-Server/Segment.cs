using System;
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
    #region enum
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
        autorized = 0b0000, //poprawne dzialanie
        overflow = 0b0001,//przekroczenie zakresu
        notallowed = 0b0010,//niedozwolona operacja np. dzielenie przez zero
        //notdefined = 0b0100

    }
    #endregion

    public class Segment
    {
        private int arg_1;//liczba1
        private int arg_2;//liczba2
        private Operation operation;//pole operacji
        private Status status;//pole statusu
        private string data_length;//dlugosc pola danych
        private BitArray dynamic_data;//pole danych o dymanicznym rozmiarze


        private BitArray bitAR;

        public Segment(byte[] buffer)
        {
            bitAR = new BitArray(buffer);
        }












        public void Serialize()
        {
            Add(operation);
            Add(arg_1);
            Add(arg_2);
            Add(status);
            
        }

        int index = 0;
        int byteIndex = 0;
        private void Add(bool value) //serialization of bool value
        {
            if (index % 8 == 0)
            {
                byteIndex++;
                index = byteIndex * 8;
            }
            index--;//tu chyba powinno byc if(index == 0) index = 0;
            bitAR.Set(index, value);
        }

        private void Add(byte value) //serialization of byte value
        {
            for (int i = 7; i >= 0; i--)
            {
                this.Add((value & (1 << i)) != 0);
            }
        }

        private void Add(Operation op) //serialization of Operation
        {
            foreach (byte by in BitConverter.GetBytes((int)op))
            {
                for (int i = 2; i >= 0; i--)
                {
                    this.Add((by & (1 << i)) != 0);
                }
                break;
            }
        }

   

        private void Add(Status st) //serialization of state
        {
            foreach (byte by in BitConverter.GetBytes((int)st))
            {
                for (int i = 1; i >= 0; i--)
                {
                    this.Add((by & (1 << i)) != 0);
                }
                break;
            }
        }

        private void Add(int value) //serialization of int32
        {
            byte[] binary = BitConverter.GetBytes(value);
            foreach (byte by in binary.Reverse())
            {
                Add(by);
            }
        }






        public void Deserialize()
        {
            operation = (Operation)GetInt(3);
            arg_1 = GetInt(32);
            arg_2 = GetInt(32);
            status = (Status)GetInt(4);
        }

        private int GetInt(int length) //deserialize int32 and add to bitarray
        {
            var result = new int[1];
            BitArray tmp = new BitArray(32, false);
            for (int i = length - 1; i >= 0; i--)
            {
                tmp[i] = getBit();
            }
            tmp.CopyTo(result, 0);
            return result[0];
        }

        private bool getBit() //getter of last bit in bitarray
        {
            if (index % 8 == 0)
            {
                byteIndex++;
                index = byteIndex * 8;
            }
            index--;
            return bitAR[index];
        }





        public byte[] GetBytes() //function that returns byte array with serialized bitarray
        {
            this.Serialize();
            byte[] binary = new byte[(bitAR.Length - 1) / 8 + 1];
            bitAR.CopyTo(binary, 0);
            return binary;
        }

    }




}
