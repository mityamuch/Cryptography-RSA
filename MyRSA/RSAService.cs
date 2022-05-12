using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyRSA
{
    public class RSAService
    {
        private SimpleGenerator _keyGenerator;
        private BigInteger _d;
        private BigInteger _n;
        private int _bitLength;


        public RSAService(SimplifyTestMode mode,double probabilityofsimplicity,int bitLength)
        {
            _bitLength = bitLength;
            _keyGenerator = new SimpleGenerator(mode,probabilityofsimplicity, bitLength);
        }

        public void Encrypt()
        {

            BigInteger p = _keyGenerator.GeneratePrimeDigit();
            BigInteger q = _keyGenerator.GeneratePrimeDigit();

            BigInteger content = new BigInteger(File.ReadAllBytes("../../../../in.txt"), true);

            if (content.GetBitLength() >= _bitLength)
                throw new Exception("File too long!");


            BigInteger n = p * q;
            BigInteger m = (p - 1) * (q - 1);
            BigInteger e = Calculate_e(m);
            BigInteger d = Calculate_d(e, m);

            BigInteger res = RSA_EncryptString(content, e, n);

            File.WriteAllText("../../../../out1.txt", System.Convert.ToBase64String(res.ToByteArray()));

            _d = d;
            _n = n;

            System.Diagnostics.Process txt = new System.Diagnostics.Process();
            txt.StartInfo.FileName = "notepad.exe";
            txt.StartInfo.Arguments = @"../../../../out1.txt";
            txt.Start();

        }

        public void Decrypt()
        {
            BigInteger d = _d;
            BigInteger n = _n;

            BigInteger content = new BigInteger(System.Convert.FromBase64String(File.ReadAllText("../../../../out1.txt")));

            BigInteger res = RSA_DecryptString(content, d, n);

            File.WriteAllBytes("../../../../out2.txt", res.ToByteArray(true));

            System.Diagnostics.Process txt = new System.Diagnostics.Process();
            txt.StartInfo.FileName = "notepad.exe";
            txt.StartInfo.Arguments = @"../../../../out2.txt";
            txt.Start();
        }

        private BigInteger RSA_EncryptString(BigInteger s, BigInteger e, BigInteger n)
        {
            return BigInteger.ModPow(s, e, n);
        }

        private BigInteger RSA_DecryptString(BigInteger input, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(input, d, n);
        }

        private BigInteger Calculate_e(BigInteger m)
        {
            BigInteger e = (1 << 16) + 1;
            while (AuxiliaryFunctions.Gcd(e, m) != 1)
                e+=2;

            return e;
        }

        private (BigInteger, BigInteger, BigInteger) euclid_ex(BigInteger a, BigInteger b)
        {
            if (a == 0)
        		return (b, 0, 1);

            (BigInteger nod, BigInteger x, BigInteger y) = euclid_ex(b % a, a);
            return (nod, y - (b/a)*x, x);
        }

        private BigInteger Calculate_d(BigInteger e, BigInteger m)
        {
            (BigInteger nod, BigInteger x, BigInteger y) = euclid_ex(e, m);

            if (x > 0)
                return x+e*m;
            else
                return x+(y/e + 1)*m + e * m;
        }
    }
}
