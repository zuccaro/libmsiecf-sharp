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

                    foreach (var cd in foo.CacheDirectories)
                    {
                        Console.WriteLine(cd);
                    }

                    int ctr = 0;
                    foreach (var item in foo.URLItems)
                    {
                        Console.Write("{0:0000}: {1} - ", ctr, item.CachePath);

                        //if (item.Location.IndexOf("") > -1)
                        //    Console.Write(item.Location);

                        Console.WriteLine();
                        ctr++;
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
