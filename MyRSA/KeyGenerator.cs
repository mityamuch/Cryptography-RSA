using System;
using System.Numerics;

namespace MyRSA
{
    #region Nested

    public enum SimplifyTestMode
    {
        MillerRabin,
        Ferm,
        SoloveyShtrasen

    };

    #endregion

    internal class KeyGenerator
    {

        private SimplifyTestMode _mode;
        private double _probabilityOfSimplicity;
        private int _length;
        private Random _random;

        


        public KeyGenerator(SimplifyTestMode mode, double probabilityOfSimplicity, int BitLength)
        {
            _mode = mode;
            _probabilityOfSimplicity = probabilityOfSimplicity;
            _length = BitLength;
            _random = new Random();
        }

        public BigInteger GeneratePrimeDigit()
        {
            BigInteger digit = GetRandCount(_length);
            BigInteger minpq =new BigInteger(1) << _length-1;


            switch (_mode)
            {
                case SimplifyTestMode.MillerRabin:
                    {
                        MillerRabinTest test = new MillerRabinTest();
                        
                        while (digit<=minpq&&!test.CheckSimplicity(digit,_probabilityOfSimplicity))
                        {
                            digit = GetRandCount(_length);
                        }
                    }
                    break;
                case SimplifyTestMode.Ferm:
                    {
                        FermTest test = new FermTest();
                        while (digit <= minpq&&!test.CheckSimplicity(digit, _probabilityOfSimplicity))
                        {
                            digit = GetRandCount(_length);
                        }
                    }
                    break;
                case SimplifyTestMode.SoloveyShtrasen:
                    {
                        SoloveyShtrassenTest test = new SoloveyShtrassenTest();
                        while (digit <= minpq&&!test.CheckSimplicity(digit, _probabilityOfSimplicity))
                        {
                            digit = GetRandCount(_length);
                        }
                    }
                    break;
                default:
                    break;
            }
            return digit;
        }



        private BigInteger GetRandCount(int bits)
        {
            byte[] count = new byte[bits / 8];
            _random.NextBytes(count);
            BigInteger result = new BigInteger(count);
            return result > 0 ? result : -result;
        }
    }
}
