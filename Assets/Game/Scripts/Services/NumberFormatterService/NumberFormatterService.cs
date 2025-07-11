using System;
using Game.Scripts.Core;

namespace Game.Scripts.Services.NumberFormatterService
{
    public class NumberFormatterService : INumberFormatterService
    {
        public void Initialize()
        {
            //Nothing for now
        }
        
        // We only need to define suffixes up to a reasonable point.
        // The system can handle numbers far beyond this list.
        private static readonly string[] Suffixes = 
        {
            "", "K", "M", "B", "T", "aa", "ab", "ac", "ad", "ae", // ...and so on
            "ba", "bb", "bc", "bd", "be", // Add as many as you need
        };

        public string Format(BigNumber number, NumberFormatType formatType = NumberFormatType.Standard, int decimalPlaces = 2)
        {
            // Create the format string dynamically, e.g., "F2"
            string formatSpecifier = "F" + decimalPlaces;

            if (formatType == NumberFormatType.Scientific)
            {
                // Use the format specifier with ToString() before building the final string
                return number.Mantissa.ToString(formatSpecifier) + "e" + number.Exponent;
            }

            // Standard formatting with alphabetical suffixes
    
            if (number.Exponent < 3)
            {
                double standardNumber = number.Mantissa * Math.Pow(10, number.Exponent);
                return standardNumber.ToString("F0");
            }
    
            int suffixIndex = number.Exponent / 3;
            double mantissa = number.Mantissa * Math.Pow(10, number.Exponent % 3);
    
            if (suffixIndex >= Suffixes.Length)
            {
                // Fallback to scientific notation
                return mantissa.ToString(formatSpecifier) + "e" + (suffixIndex * 3);
            }
    
            // The final, corrected string construction
            return mantissa.ToString(formatSpecifier) + Suffixes[suffixIndex];
        }
    }
}