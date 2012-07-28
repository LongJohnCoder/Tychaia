using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tychaia
{
    public static class Log
    {
        public static List<string> m_Entries = new List<string>();

        public static void WriteLine(string message)
        {
            Console.WriteLine(message);
            Log.m_Entries.Add(message);
        }
    }
}
