using System;
using System.Linq;

namespace Clones
{
	class Program
	{
        static void Main()
        {
            var n = (Console.ReadLine() ?? "").Split().Select(int.Parse).First();
            var clones = new CloneVersionSystem();
            for (int i = 0; i < n; i++)
            {
                var query = Console.ReadLine();
                if (query == null) continue;
                var result = clones.Execute(query);
                if (result != null)
                    Console.WriteLine(result);
            }
        }

        //static void Main()
        //{
        //    var n = (Console.ReadLine() ?? "").Split().Select(int.Parse).First();
        //    var clones = new CloneVersionSystem();

        //    for (int i = 0; i < n; i++)
        //    {
        //        var query = Console.ReadLine();
        //        if (query == null) continue;
        //        var result = clones.Execute(query);
        //        if (result != null)
        //            Console.WriteLine(result);
        //    }

        //    //clones.Execute("learn 1 1");
        //    //clones.Execute("learn 1 2");
        //    //clones.Execute("learn 1 3");
        //    //clones.Execute("clone 1");
        //    //var result = clones.Execute("check 2");
        //    //clones.Execute("clone 2");
        //    //result = clones.Execute("check 3");
        //}
    }
}
