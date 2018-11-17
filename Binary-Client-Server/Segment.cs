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
        private BitArray _arg_1;//liczba1
        private BitArray _arg_2;//liczba2
        private Operation _operation;//pole operacji
        private Status _status;//pole statusu
        private BitArray _data_length;//dlugosc pola danych
        private BitArray _dynamic_data;//pole danych o dymanicznym rozmiarze

        

        private byte[] _bitAR;

 
        public Segment(byte[] buffer)
        {
            _bitAR = buffer;
        }

        private int CalculateSegmentSize() { return 7 + _arg_1.Length + _arg_2.Length + _data_length.Length + _dynamic_data.Length; }

        public void createBuffer(int a, int b, Operation o, Status s)
        {
            //SSSS OOO DATA32PTR DATA 
            BinaryMinimalizer bm = new BinaryMinimalizer();
            _arg_1 = bm.ReturnMinimalizedTable(a);//zminimalizowanie i zamiana liczb na ciag bitow
            _arg_2 = bm.ReturnMinimalizedTable(b);
            _data_length = bm.change(new BitArray(new int[] { _arg_1.Length + _arg_2.Length}));//minimalizacja bitow ptr
            _operation = o;//przypisanie pol
            _status = s;

            _bitAR  = new byte[CalculateSegmentSize()];//tworzenie tablicy bufera o zadanej dlugosci


            bm.change(new BitArray(new int[] {(Int32)_status })).CopyTo(_bitAR, 0);//zmiana enum na bity
            bm.change(new BitArray(new int[] { (Int32)_operation })).CopyTo(_bitAR, 3);
            _data_length.CopyTo(_bitAR, 7);//przypisywanie do tablicy kolejno
            _arg_1.CopyTo(_bitAR, 39);
            _arg_2.CopyTo(_bitAR, 39 + _arg_1.Length);

        }







    }




}
