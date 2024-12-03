using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Online_Exercises.TopInterview150
{
    public class _125_ValidPalindrome
    {
        public static bool IsPalindrome(string s)
        {
            var mexLength = 2 * Math.Pow(10,5);
            if (1 > s.Length || s.Length > mexLength) return false;
            if(string.IsNullOrWhiteSpace(s)) return true;
            if(!Ascii.IsValid(s)) return false;

            string alfanumericString = new string(s.Where(c => char.IsLetterOrDigit(c)).ToArray());

            List<char> cleanChars = new();
            var chars = alfanumericString.ToLower().ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                char currentChar = chars[i];
                char oppositeChar = chars[chars.Length - i - 1];

                if(currentChar != oppositeChar) return false;
            }

            return true;
        }

        public static void Test()
        {
            // Test 1
            var isPalidrome = IsPalindrome("A man, a plan, a canal: Panama");
            if (!isPalidrome) throw new Exception();

            // Test 2
            isPalidrome = IsPalindrome("race a car");
            if (isPalidrome) throw new Exception();

            // Test 3
            isPalidrome = IsPalindrome(" ");
            if (!isPalidrome) throw new Exception();

            // Test 4
            isPalidrome = IsPalindrome("0P");
            if (isPalidrome) throw new Exception();

            Console.WriteLine("Test 125 passed");
        }
    }
}
