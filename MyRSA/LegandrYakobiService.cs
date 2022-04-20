using System;
using System.Numerics;

namespace MyRSA
{
    public class LegandrYakobiService
    {
        
        public static BigInteger GetLegandrSymbol(BigInteger a, BigInteger p)
        {
            if( p < 2)
			    throw new Exception("p must not be < 2");
            if (a == 0 || a == 1)
                return a;
            BigInteger r;
            if (a % 2 == 0) {
                r= GetLegandrSymbol(a / 2, p);
                if (((p * p - 1) & 8) != 0)
				    r *= -1;
            }
            else 
            {
                r = GetLegandrSymbol(p % a, a);
                if (((a - 1) * (p - 1) & 4) != 0)
                    r *= -1;
            }
            return r;
        }


        public static int  GetYakobiSymbol(int a,int n)
        {




            return 1;
        }
       

    }
}
