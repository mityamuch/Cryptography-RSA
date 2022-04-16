using System;
using System.Numerics;

namespace MyRSA
{
    internal class LegandrYakobiService
    {
        
        public static int GetLegandrSymbol(int a, int p)
        {
            if (a == 1)
                return 1;
            else if (a % 2==0)
            {
                return (int)(GetLegandrSymbol(a / 2, p) * Math.Pow(-1, ((p * p - 1) / 8)));
            }
            else
            {
                return (int)(GetLegandrSymbol(p % a, a) * Math.Pow(-1,(a-1)*(p-1)/ 4));
            }

        }


        public static int  GetYakobiSymbol(int a,int n)
        {
            return 1;
        }
       

    }
}
