using System;
using System.Numerics;
using System.Security.Cryptography;

namespace MyRSA
{
	internal class AuxiliaryFunctions
	{

		public static BigInteger Gcd(BigInteger a, BigInteger b)
		{
			if (b == 0)
				return a;
			return Gcd(b, a % b);

		}

		public static BigInteger Mul(BigInteger a, BigInteger b, BigInteger m)
		{
			if (b == 1)
				return a;
			if (b % 2 == 0)
			{
				BigInteger t = Mul(a, b / 2, m);
				return (2 * t) % m;
			}
			return (Mul(a, b - 1, m) + a) % m;
		}

		public static BigInteger Pow(BigInteger a, BigInteger b, BigInteger m)
		{
			if (b == 0)
				return 1;
			if (b % 2 == 0)
			{
				BigInteger t = Pow(a, b / 2, m);
				return Mul(t, t, m) % m;
			}
			return (Mul(Pow(a, b - 1, m), a, m)) % m;
		}


		public static BigInteger RandomInRange(RandomNumberGenerator rng, BigInteger min, BigInteger max)
		{
			if (min > max)
			{
				var buff = min;
				min = max;
				max = buff;
			}

			// offset to set min = 0
			BigInteger offset = -min;
			min = 0;
			max += offset;

			var value = randomInRangeFromZeroToPositive(rng, max) - offset;
			return value;
		}

		private static BigInteger randomInRangeFromZeroToPositive(RandomNumberGenerator rng, BigInteger max)
		{
			BigInteger value;
			var bytes = max.ToByteArray();

			// count how many bits of the most significant byte are 0
			// NOTE: sign bit is always 0 because `max` must always be positive
			byte zeroBitsMask = 0b00000000;

			var mostSignificantByte = bytes[bytes.Length - 1];

			// we try to set to 0 as many bits as there are in the most significant byte, starting from the left (most significant bits first)
			// NOTE: `i` starts from 7 because the sign bit is always 0
			for (var i = 7; i >= 0; i--)
			{
				// we keep iterating until we find the most significant non-0 bit
				if ((mostSignificantByte & (0b1 << i)) != 0)
				{
					var zeroBits = 7 - i;
					zeroBitsMask = (byte)(0b11111111 >> zeroBits);
					break;
				}
			}

			do
			{
				rng.GetBytes(bytes);

				// set most significant bits to 0 (because `value > max` if any of these bits is 1)
				bytes[bytes.Length - 1] &= zeroBitsMask;

				value = new BigInteger(bytes);

				// `value > max` 50% of the times, in which case the fastest way to keep the distribution uniform is to try again
			} while (value > max);

			return value;
		}

	}
}
