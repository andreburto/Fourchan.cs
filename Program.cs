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

            string urlb = f.GetPageUrl("co", c[0].page.ToString());
            Console.WriteLine("Url B: {0}", urlb);

            Thread.Sleep(1500);
            Fourchan.Fourchan.Threads page = f.ParsePage(urlb);

            foreach (Fourchan.Fourchan.Posts ps in page.threads)
            {
                Console.WriteLine("Thread:");
                foreach (Fourchan.Fourchan.Post p in ps.posts)
                {
                    Console.WriteLine(": {0} - {1}", p.ToString(), p.no);
                }
            }


            Console.ReadLine();
        }
    }
}
