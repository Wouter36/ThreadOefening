using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadOefening
{
    class TxtLog
    {
        private static readonly object LockObj = new object();

        public static async Task WriteText(string filePath, string text)
        {
            lock (LockObj) 
            {
                string t = text + Environment.NewLine;
                byte[] encodedText = Encoding.Unicode.GetBytes(t);

                using (FileStream sourceStream = new FileStream(filePath,
                FileMode.Append, FileAccess.Write, FileShare.None,
                bufferSize: 4096, useAsync: true))
                {
                    sourceStream.WriteAsync(encodedText, 0, encodedText.Length);
                };
            }
        }
    }
}
