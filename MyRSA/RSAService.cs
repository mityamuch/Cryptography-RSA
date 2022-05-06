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
        private KeyGenerator _keyGenerator;
        private BigInteger _d;
        private BigInteger _n;
        private char[] characters = new char[] { '#', 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И',
                                                'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С',
                                                'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ь', 'Ы', 'Ъ',
                                                'Э', 'Ю', 'Я', ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0' };

        public RSAService(SimplifyTestMode mode,double probabilityofsimplicity,int BitLength)
        {
            _keyGenerator = new KeyGenerator(mode,probabilityofsimplicity,BitLength);
        }

        public void Encrypt()
        {

            BigInteger p = _keyGenerator.GeneratePrimeDigit();
            BigInteger q = _keyGenerator.GeneratePrimeDigit();
            string s = "";

            StreamReader sr = new StreamReader("in.txt");

            while (!sr.EndOfStream)
            {
                s += sr.ReadLine();
            }
            sr.Close();
            s = s.ToUpper();
            BigInteger n = p * q;
            BigInteger m = (p - 1) * (q - 1);
            BigInteger d = Calculate_d(m);
            BigInteger e_ = Calculate_e(d, m);

            List<string> result = RSA_EncryptString(s, e_, n);

            StreamWriter sw = new StreamWriter("out1.txt");
            foreach (string item in result)
                sw.WriteLine(item);
            sw.Close();
            _d = d;
            _n = n;

            Process.Start("out1.txt");

        }

        public void Decrypt()
        {

            BigInteger d = _d;
            BigInteger n = _n;

            List<string> input = new List<string>();

            StreamReader sr = new StreamReader("out1.txt");

            while (!sr.EndOfStream)
            {
                input.Add(sr.ReadLine());
            }

            sr.Close();

            string result = RSA_DecryptString(input, d, n);
            StreamWriter sw = new StreamWriter("out2.txt");
            sw.WriteLine(result);
            sw.Close();

            Process.Start("out2.txt");
        }

        private List<string> RSA_EncryptString(string s, BigInteger e, BigInteger n)
        {
            List<string> result = new List<string>();

            BigInteger bi;

            for (int i = 0; i < s.Length; i++)
            {
                int index = Array.IndexOf(characters, s[i]);

                bi = new BigInteger(index);
                bi = BigInteger.Pow(bi, (int)e);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                result.Add(bi.ToString());
            }

            return result;
        }

        private string RSA_DecryptString(List<string> input, BigInteger d, BigInteger n)
        {
            string result = "";

            BigInteger bi;

            foreach (string item in input)
            {
                bi = new BigInteger(Convert.ToDouble(item));
                bi = BigInteger.Pow(bi, (int)d);

                BigInteger n_ = new BigInteger((int)n);

                bi = bi % n_;

                int index = Convert.ToInt32(bi.ToString());

                result += characters[index].ToString();
            }

            return result;
        }

        private BigInteger Calculate_e(BigInteger d, BigInteger m)
        {
            BigInteger e = 1<<16 + 1;

            while (true)
            {
                if ((e * d) % m == 1)
                    break;
                else
                    e++;
            }

            return e;
        }
        private BigInteger Calculate_d(BigInteger m)
        {
            
            BigInteger d = m - 1;
            AuxiliaryFunctions.Gcd(d,m);

            return d;
        }
    }
}
