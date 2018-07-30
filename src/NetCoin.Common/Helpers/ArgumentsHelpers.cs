using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoin.Common.Helpers
{
    public static class ArgumentsHelpers
    {
        public static bool TryGetArgument(string[] args, string name, out string value)
        {
            value = string.Empty;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Eq(name))
                {
                    value = args[i + 1];
                    return true;
                }
            }
            return false;
        }
    }
}
