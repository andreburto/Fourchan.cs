using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using Fourchan;

namespace ConsoleApplication1
{


    class Program
    {
        static void Main(string[] args)
        {
            Fourchan.Fourchan f = new Fourchan.Fourchan();
            string urla = f.GetCatalogUrl("co");
            
            List<Fourchan.Fourchan.Catalog> c = f.ParseCatalog(urla);

            Console.WriteLine("Url A: {0}", urla);
            Console.WriteLine("Pages: {0}", c.Count);

            foreach (Fourchan.Fourchan.Catalog cat in c)
            {
                Console.WriteLine("Page: {0}", cat.page);
                foreach (Fourchan.Fourchan.Post p in cat.threads)
                {
                    Console.WriteLine("{0} by {1}", p.no, p.name);
                }
            }

            Console.ReadLine();
        }
    }
}
