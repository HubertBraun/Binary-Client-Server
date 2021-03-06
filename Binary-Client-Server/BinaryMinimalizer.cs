﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Binary_Client_Server
{
    static class BinaryMinimalizer
    {

        static public BitArray Change(BitArray bits)    // zamiana konca na poczatek, reprezentowanie BitArray w czytelnej formie
        {
            int len = bits.Count;
            BitArray a = new BitArray(bits);
            BitArray b = new BitArray(bits);

            for (int i = 0, j = len - 1; i < len; ++i, --j)     // swap ostatniego bitu z pierwszym
            {
                a[i] = a[i] ^ b[j];
                b[j] = a[i] ^ b[j];
                a[i] = a[i] ^ b[j];
            }

            return a;
        }

        static public BitArray ReturnMinimalizedTable(int x)    // minimalizowanie wielkosci
        {
            BitArray arr = new BitArray(new int[] { x });
            int index = 0;
            for (int i = arr.Length - 1; i >= 0; i--)   // szukanie pierwszego bitu 1
            {
                if (arr[i] == true)
                {
                    index = i;
                    break;
                }
            }
            index++;
            BitArray toReturn = new BitArray(index);
            for (int i = index - 1; i >= 0; i--)    // przepisanie tablicy
            {
                toReturn[i] = arr[i];
            }
            return Change(toReturn);

        }

    }
}
