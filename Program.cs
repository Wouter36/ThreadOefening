using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadOefening
{
    class Program
    {
        static async Task Main(string[] args)
        {
            List<string> monkeyNames = new List<string>();
            monkeyNames.Add("Albert");
            monkeyNames.Add("Einstein");
            monkeyNames.Add("David");
            monkeyNames.Add("Freddie");
            monkeyNames.Add("Sonja");
            Forest forest = new Forest(monkeyNames, 400, 200, 200);
            //Forest forest1 = new Forest(monkeyNames, 424, 200, 200);
            await forest.Start();
            //await forest1.Start();
            Console.Read();
        }
    }
}
