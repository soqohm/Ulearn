using System;

namespace Incapsulation.Weights
{
	public class Indexer
    {
        readonly double[] range;
        readonly int start;

        public int Length { get; }

        public Indexer(double[] array, int start, int length)
        {
            if (start < 0 || length < 0 || array.Length - start - length < 0)
                throw new ArgumentException();

            range = array;
            this.start = start;
            Length = length;
        }

        public double this[int n]
        {
            get { return range[start + Check(n)]; }

            set { range[start + Check(n)] = value; }
        }

        int Check(int index)
        {
            if (index < 0 || index > Length - 1)
                throw new IndexOutOfRangeException();
            return index;
        }
    }
}