using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyRSA
{
    public interface ISimplicityTest
    {
        bool CheckSimplicity(BigInteger value,double probabilityOfSimplicity);

        string Name { get; }

        int GetCountRounds(double probabilityOfSimplicity);

        
    }


    public class MillerRabinTest:ISimplicityTest
    {

        public string Name => "Miller-Rabin";
        
        public bool CheckSimplicity(BigInteger value,double probabilityOfSimplicity)
        {
            /*1. Представить n − 1 в виде (2^s)·t, где t нечётно, можно сделать последовательным делением n - 1 на 2.
              2. В цикле по i от 1 до r выполнить:
                  2.1. Выбрать случайное целое число a в отрезке [2, m − 2].
                  2.2. x ← a^t mod n, вычисляется с помощью алгоритма возведения в степень по модулю.
                  2.3. Если x = 1 или x = m − 1, то перейти на следующую итерацию цикла 2.
                  2.4. В цикле по j от 1 до s − 1 выполнить:
                      2.4.1. x ← x^2 mod n.
                      2.4.2. Если x = 1, то вернуть (составное).
                      2.4.3. Если x = m − 1, то перейти на следующую итерацию цикла 2.
                  2.5. Вернуть (составное).
              3. Вернуть (вероятно простое).*/

            if (value == 2 || value == 3)
                return true;
            if (value < 2 || value % 2 == 0)
                return false;
            int T = 10;
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            int s = 0;

            BigInteger t = value - 1;
            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }
            for (int i = 0; i < T; i++)
            {
                BigInteger a = AuxiliaryFunctions.RandomInRange(rnd, 2, value - 2);
                BigInteger x = BigInteger.ModPow(a, t, value);
                if (x == 1 || x == value - 1)
                    continue;
                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, value);
                    if (x == 1)
                        return false;
                    if (x == value - 1)
                        break;
                }
                if (x != value - 1)
                    return false;
            }
            return true;
        }

        public int GetCountRounds(double probabilityOfSimplicity) 
        {
            int count = 0;
            double extra = 1;
            while (1 - extra >= probabilityOfSimplicity)
            {
                count++;
                extra *= 0.25;
            }
            return count;
        }
    }

    public class FermTest : ISimplicityTest
    {

        public string Name => "Ferma";
        
        public bool CheckSimplicity(BigInteger value, double probabilityOfSimplicity)
        {
            /*1. В цикле i от 1 до t выполнить:
                1.1 Выбрать случайное a: 2<A<value-2.
                1.2 Вычислить r=a^(n-1)(mod n)
                1.3 Если r!=1, тогда вернуть («n – составное»).
            2. Вернуть («n – простое»).*/

            if (value == 2 || value == 3)
                return true;
            if (value < 2 || value % 2 == 0)
                return false;

            int T = 10;
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();

            for (int i = 0; i < T; i++)
            {
                BigInteger a = AuxiliaryFunctions.RandomInRange(rnd,2,value-2);
                if (AuxiliaryFunctions.Gcd(a, value) != 1)
                    return false;
                if (BigInteger.ModPow(a, value - 1, value) != 1)
                    return false;
            }


            return true;
        }

        public int GetCountRounds(double probabilityOfSimplicity) 
        {
            int count = 0;
            double extra = 1;
            while (1 - extra <= probabilityOfSimplicity)
            {
                count++;
                extra *= 0.5;
            }
            return count;
        }
    }


    public class SoloveyShtrassenTest : ISimplicityTest
    {
        public string Name => "Solovey-Shtrassen";

        /*1.В цикле i от 1 до k выполнить:
            1.1. Выбрать a  - случайное целое от 2 до n-1 , включительно;
            1.2. Если НОД(a, n) > 1, тогда вернуть (составное);
            1.3.Если Gus21.gif , тогда вернуть (составное).
          2.Вернуть (простое с вероятностью Gus20.gif )*/
        public bool CheckSimplicity(BigInteger value, double probabilityOfSimplicity)
        {
            if (value == 2 || value == 3)
                return true;
            if (value < 2 || value % 2 == 0)
                return false;

            int T = 10;
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();

            for (int i = 0; i < T; i++)
            {
                BigInteger a = AuxiliaryFunctions.RandomInRange(rnd,2, value - 1);
                if (AuxiliaryFunctions.Gcd(a, value) > 1)
                    return false;
                BigInteger y = BigInteger.ModPow(a, (value - 1) / 2,value);
                BigInteger x = LegandrYakobiService.GetYakobiSymbol(a, value);
                if (x < 0)
                    x += value;
                if (y != x % value)
                    return false;
                //BigInteger x = LegandrYakobiService.GetLegandrSymbol(a, value);
                //if ((x == 0) || (y != x % value))
                //return false;
            }

            return true;
        }

        public int GetCountRounds(double probabilityOfSimplicity)
        {
            int count = 0;
            double extra=1;
            while (1 - extra <= probabilityOfSimplicity)
            {
                count++;
                extra *= 0.5;
            }
            return count;
        }
    }
}
