using System.Collections.Generic;
using System.Linq;

namespace yield
{
	public static class MovingMaxTask
	{
		public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
		{
            var link = new LinkedList<double>();
            var linkMax = new LinkedList<double>();

            foreach (var value in data)
            {
                link.AddLast(value.OriginalY);
                if (link.Count > windowWidth)
                {
                    if (link.First() == linkMax.First())
                        linkMax.RemoveFirst();
                    link.RemoveFirst();
                }
                while (linkMax.Count != 0 && linkMax.Last.Value < value.OriginalY)
                {
                    linkMax.RemoveLast();
                }
                linkMax.AddLast(value.OriginalY);
                value.MaxY = linkMax.First();
                yield return value;
            }
        }
	}
}