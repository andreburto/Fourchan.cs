using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using Fourchan;

namespace ConsoleApplication1
{


    class Program
    {
        static void Main(string[] args)
        {
            Fourchan.Fourchan f = new Fourchan.Fourchan();
            string url = f.GetCatalogUrl("co");
            List<Fourchan.Fourchan.Catalog> c = f.ParseCatalog(url);

            Console.WriteLine("Pages: {0}", c.Count);

            foreach (Fourchan.Fourchan.Catalog cat in c)
            {
                Console.WriteLine("Page {0} has {1} threads.", cat.page, cat.threads.Length);
            }
            Console.ReadLine();
        }
    }
}
