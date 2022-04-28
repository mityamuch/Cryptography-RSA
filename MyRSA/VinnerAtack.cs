﻿using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.IO;
using System;
using System.Windows;

namespace MyRSA
{
    public  class VinnerAtack
    {
        public struct Pair
        {
            public BigInteger P;
            public BigInteger Q;
        }

       private IEnumerable<Pair> convergents(IEnumerable<BigInteger> cf) 
        {
            BigInteger r = 0; 
            BigInteger s = 1; 
            BigInteger p = 1; 
            BigInteger q = 0;
            foreach (var c in cf) 
            {
                r = p;
                s = q;
                p = c * p + r;
                q = c * q + s;
                Pair pair = new Pair() { P=p,Q=q};
                yield return pair;
            }
        }

        private IEnumerable<BigInteger> contfrac(BigInteger p, BigInteger q) 
        {
            while (q!=0) {
                var n = p / q;
                yield return n;
                q = p - q * n;
                p = q;
            }
        }

        public void Attack(BigInteger isN, BigInteger isE) 
        {
            // по теореме Винера считаем ограничение для секретной экспоненты D
            var limitD = AuxiliaryFunctions.Sqrt(isN);
            limitD = AuxiliaryFunctions.Sqrt(limitD);
            limitD = limitD/3;
            MessageBox.Show("Find D < 1/3*N^^1/4: D < " + limitD);
            var myM = 0x01010101;

            // раскладываем число E/N в непрерывную дробь и проверяем знаменатель
            //каждой подходящей дроби: не является ли он секретным ключом
            IEnumerable<Pair> contf = convergents(contfrac(isE, isN));
            List<Pair> contfList = contf.ToList();
            for (var i=0; i< contfList.Count;i++) 
            {
                var current = contfList[i];
                if (current.Q > limitD)
                    break;
                // С = М^^E mod N
                var isC = BigInteger.ModPow(myM, isE, isN);
                // M = C^^D mod N
                var myM2 = BigInteger.ModPow(isC, current.Q, isN);

                string path = "AtackVinnerProtocol.txt";
                if (myM == myM2)
                {
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLineAsync("is fraction: " + current.P + "/" + current.Q + ". Need check denominator: " + current.Q + ". VALID VALUE");
                    }
                }
                else 
                {
                    using (StreamWriter writer = new StreamWriter(path, true))
                    {
                        writer.WriteLineAsync("is fraction: " + current.P + "/" + current.Q +". Need check denominator: " + current.Q + ". Invalid");
                    }
                }
            }
        }
    }
}
