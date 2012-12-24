using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Flights
{
    class Utilities
    {

        public static string RemoveNonBreakingSpace(string input)
        {
            string output = input;
            const string NON_BREAKING_SPACE = "&nbsp;";

            int index = input.IndexOf(NON_BREAKING_SPACE);

            if (index != -1)
            {
                output = input.Remove(index, NON_BREAKING_SPACE.Length);
            }
            return output;
        }

        public static string ExtractNumberAndCharacter(string input)
        {
            string newString = Regex.Replace(input, "[^0-9A-Za-z]", "");
            return newString;
        }

        public static string ExtractNumber(string input)
        {
            string newString = Regex.Replace(input, "[^0-9]", "");
            return newString;
        }

        public static string JestaExtractNumber(string input)
        {
            string[] words = input.Split('x');
            if (words.Length != 2)
            {
                return "";
            }

            string newString = Regex.Replace(words[1], "[^0-9]", "");
            return newString;
        }
    }
}
