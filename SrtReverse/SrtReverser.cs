using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace SrtReverse
{
    public class SrtReverser
    {
        public enum Results
        {
            Ok,
            UnsupportedFileExtention
        }

        public static Results Reverse(string file, string target)
        {
            List<SrtRecord> records = new List<SrtRecord>();

            if (string.Compare(Path.GetExtension(file), ".srt", true, CultureInfo.InvariantCulture) == 0)
            {
                Encoding fileEncoding;

                using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    fileEncoding = GetEncoding(fs);

                    using (TextReader tr = new StreamReader(fs, fileEncoding))
                    {
                        do
                        {
                            records.Add(new SrtRecord(tr));
                        } while (tr.Peek() != -1);
                    }
                }

                using (FileStream fs = new FileStream(target, FileMode.Create, FileAccess.Write))
                {
                    using (TextWriter tr = new StreamWriter(fs, Encoding.UTF8))
                    {
                        foreach (SrtRecord record in records)
                        {
                            byte[] converted = Encoding.Convert(fileEncoding, Encoding.UTF8, fileEncoding.GetBytes(record.ToString()));

                            tr.Write(Encoding.UTF8.GetString(converted));
                            if (record != records.Last())
                                tr.WriteLine();
                        }
                    }
                }
            }
            else
            {
                return Results.UnsupportedFileExtention;
            }

            return Results.Ok;
        }

        private static Encoding GetEncoding(FileStream fs)
        {
            // *** Use Default of Encoding.Default (Ansi CodePage)
            Encoding enc = Encoding.Default;

            // *** Detect byte order mark if any - otherwise assume default
            byte[] buffer = new byte[5];
            fs.Read(buffer, 0, 5);

            if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
                enc = Encoding.UTF8;
            else if (buffer[0] == 0xfe && buffer[1] == 0xff)
                enc = Encoding.Unicode;
            else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
                enc = Encoding.UTF32;
            else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
                enc = Encoding.UTF7;

            fs.Seek(0, SeekOrigin.Begin);
            return enc;
        }
    }
}
