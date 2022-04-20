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


        public static BigInteger GetYakobiSymbol(BigInteger a, BigInteger b)
        {

            if (AuxiliaryFunctions.Gcd(a, b) != 0)
                return 0;
            int r = 1;
            if (a < 0)
            {
                a = -a;
                if (b % 4 == 3)
                    r = -r;
            }
            do
            {
                int t = 0;
                while (a % 2 == 0)
                {
                    t++;
                    a = a / 2;
                }
                if (t % 2 == 1)
                {
                    if (b % 8 == 3 || b % 8 == 5)
                        r = -r;
                }

                if (a % 4 == b % 4)
                {
                    r = -r;
                    var c = a;
                    a = b % c;
                    b = c;
                }
            }while (a != 0);
            return r;
        }
       

    }
}
