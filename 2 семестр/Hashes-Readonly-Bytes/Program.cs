using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using NUnit.Framework;

namespace hashes
{
	class Program
	{
		static void Main()
		{
            var test = new ReadonlyBytes(new byte[] {  });
            Console.WriteLine(test.Equals(null));
            Console.WriteLine(test.Equals("string"));
            Console.WriteLine(test.Equals(new ReadonlyBytesTests.DerivedFromReadonlyBytes()));
            Console.ReadKey();
		}
	}
}
