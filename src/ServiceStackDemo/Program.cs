using ServiceStack.Caching;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStackDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo demo = new Demo();
            //demo.TestByteValue();

            //demo.TestSet();

            demo.TestList();

            //demo.TestHash();
        }

    }
}
