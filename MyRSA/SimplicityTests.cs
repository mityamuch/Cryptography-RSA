using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
            return true;
        }


    }
}
