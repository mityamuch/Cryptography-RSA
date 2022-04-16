using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyRSA
{
    interface ISimplicityTest
    {
        public bool CheckSimplicity(BigInteger value,double probabilityOfSimplicity);
    }


    internal class MillerRabinTest:ISimplicityTest
    {
        
        public bool CheckSimplicity(BigInteger value, double probabilityOfSimplicity)
        {
            return true;
        }
       

    }


    internal class SoloveyShtrassenTest : ISimplicityTest
    {

        public bool CheckSimplicity(BigInteger value, double probabilityOfSimplicity)
        {
            return true;
        }


    }

    internal class FermTest : ISimplicityTest
    {

        public bool CheckSimplicity(BigInteger value, double probabilityOfSimplicity)
        {
            int T = 100;
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();

            if (value == 2)
                return true;

            for (int i = 0; i < T; i++)
            {
                BigInteger a = AuxiliaryFunctions.RandomInRange(rnd,2,value-2);
                if (AuxiliaryFunctions.Gcd(a, value) != 1)
                    return false;
                if (AuxiliaryFunctions.Pow(a, value - 1, value) != 1)
                    return false;
            }


            return true;
        }


    }
}
