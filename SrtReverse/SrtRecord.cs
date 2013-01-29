using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace SrtReverse
{
    public class SrtRecord
    {
        private readonly string index;
        private readonly string times;
        private readonly string text;

        public SrtRecord(TextReader tr)
        {
            index = tr.ReadLine();
            times = tr.ReadLine();

            List<string> lines = new List<string>();
            string line;
            while (!string.IsNullOrEmpty(line = tr.ReadLine()))
            {
                lines.Add(line);
            }

            text = Reverse(lines);
        }

        private string Reverse(IEnumerable<string> lines)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string line in lines)
            {
                sb.AppendLine(line);
            }

            return sb.ToString();
        }

        public override string ToString()
        {
            return string.Concat(index, Environment.NewLine, times, Environment.NewLine, text);
        }
    }
}
