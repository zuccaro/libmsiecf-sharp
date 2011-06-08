using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var f in args.Select(_ => _.Trim()))
            {
                var fi = new FileInfo(f);

                try
                {
                    var str = fi.OpenRead();
                    var foo = new libmsiecf.IndexDat(fi);

                    foreach (var item in foo.URLItems)
                    {
                        if (item.Location.IndexOf("") > -1)
                            Console.WriteLine(item.Location);
                    }
                    str.Close();
                }
                catch (IOException)
                {
                }
            }

            Console.WriteLine("done.");
        }
    }
}
