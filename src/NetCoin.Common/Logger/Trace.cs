using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoin.Common.Logger
{
    public static class Trace
    {
        public static void TraceException(Exception ex)
        {
            Exception e = ex;
            StringBuilder sb = new StringBuilder();
            while (e != null)
            {
                sb.AppendLine(e.Message);
                sb.AppendLine("------//------");
                e = e.InnerException;
            }
            System.Diagnostics.Trace.TraceError(sb.ToString());
        }

        public static void WriteLine(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }

        public static void TraceInformation(string message) => System.Diagnostics.Trace.TraceInformation(message);

        public static void TraceVerbose(string message) => System.Diagnostics.Trace.TraceInformation(message);

        public static void TraceError(string message) => System.Diagnostics.Trace.TraceError(message);

        public static void TraceWarning(string message) => System.Diagnostics.Trace.TraceWarning(message);
    }
}
