using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
