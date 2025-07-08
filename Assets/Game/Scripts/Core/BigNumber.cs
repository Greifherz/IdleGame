using System;

namespace Game.Scripts.Core
{
    [Serializable]
    public struct BigNumber : IComparable<BigNumber>
    {
        // A number is represented as Mantissa * 10^Exponent
        // Using double for the mantissa gives us plenty of precision for an idle game.
        public double Mantissa;
        public int Exponent;

        public BigNumber(double mantissa, int exponent)
        {
            Mantissa = mantissa;
            Exponent = exponent;
            Normalize();
        }

        /// <summary>
        /// Normalization ensures the mantissa is kept within a consistent range (e.g., 1 to 10),
        /// which prevents loss of precision and makes arithmetic easier.
        /// </summary>
        private void Normalize()
        {
            if (Mantissa == 0)
            {
                Exponent = 0;
                return;
            }

            while (Mantissa >= 10.0)
            {
                Mantissa /= 10.0;
                Exponent++;
            }

            while (Mantissa < 1.0 && Mantissa != 0)
            {
                Mantissa *= 10.0;
                Exponent--;
            }
        }

        #region Operator Overloads

        // --- Addition ---
        public static BigNumber operator +(BigNumber a, BigNumber b)
        {
            // To add, we must make the exponents match.
            // We adjust the number with the smaller exponent.
            if (a.Exponent > b.Exponent)
            {
                double mantissaB = b.Mantissa * Math.Pow(10, b.Exponent - a.Exponent);
                return new BigNumber(a.Mantissa + mantissaB, a.Exponent);
            }
            else
            {
                double mantissaA = a.Mantissa * Math.Pow(10, a.Exponent - b.Exponent);
                return new BigNumber(mantissaA + b.Mantissa, b.Exponent);
            }
        }
        
        // --- Subtraction ---
        public static BigNumber operator -(BigNumber a, BigNumber b)
        {
            // Subtraction uses the same exponent-matching logic as addition.
            if (a.Exponent > b.Exponent)
            {
                double mantissaB = b.Mantissa * Math.Pow(10, b.Exponent - a.Exponent);
                return new BigNumber(a.Mantissa - mantissaB, a.Exponent);
            }
            else
            {
                double mantissaA = a.Mantissa * Math.Pow(10, a.Exponent - b.Exponent);
                return new BigNumber(mantissaA - b.Mantissa, b.Exponent);
            }
        }

        // --- Multiplication ---
        public static BigNumber operator *(BigNumber a, BigNumber b)
        {
            // (a * 10^e1) * (b * 10^e2) = (a * b) * 10^(e1 + e2)
            return new BigNumber(a.Mantissa * b.Mantissa, a.Exponent + b.Exponent);
        }
        
        // --- Division ---
        public static BigNumber operator /(BigNumber a, BigNumber b)
        {
            if (b.Mantissa == 0) throw new DivideByZeroException();
            // (a * 10^e1) / (b * 10^e2) = (a / b) * 10^(e1 - e2)
            return new BigNumber(a.Mantissa / b.Mantissa, a.Exponent - b.Exponent);
        }

        // Implicit conversions from standard number types make it easy to use.
        public static implicit operator BigNumber(int value) => new BigNumber(value, 0);
        public static implicit operator BigNumber(float value) => new BigNumber(value, 0);
        public static implicit operator BigNumber(double value) => new BigNumber(value, 0);
        
        // --- Comparison Operators ---
        public static bool operator >(BigNumber a, BigNumber b) => a.CompareTo(b) > 0;
        public static bool operator <(BigNumber a, BigNumber b) => a.CompareTo(b) < 0;
        public static bool operator >=(BigNumber a, BigNumber b) => a.CompareTo(b) >= 0;
        public static bool operator <=(BigNumber a, BigNumber b) => a.CompareTo(b) <= 0;

        public int CompareTo(BigNumber other)
        {
            if (Exponent > other.Exponent) return 1;
            if (Exponent < other.Exponent) return -1;
            // If exponents are equal, compare mantissas
            return Mantissa.CompareTo(other.Mantissa);
        }
        
        #endregion
        
        public override string ToString()
        {
            // Default representation is scientific notation
            return $"{Mantissa:F2}e{Exponent}";
        }
    }
}