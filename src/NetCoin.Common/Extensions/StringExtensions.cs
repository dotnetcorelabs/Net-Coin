using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace System
{
    public static class StringExtensions
    {
        public static bool Eq(this string @object, string other)
        {
            return string.Compare(@object, other, false) == 0;
        }

        public static bool Eq(this string @object, string other, bool ignoreCase)
        {
            return string.Compare(@object, other, ignoreCase) == 0;
        }
    }
}
