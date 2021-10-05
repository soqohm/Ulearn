using System.Collections.Generic;

namespace yield
{
	public static class MovingAverageTask
	{
		public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
		{
            var sum = 0.0;
            var queue = new Queue<double>();
            foreach (var value in data)
            {
                queue.Enqueue(value.OriginalY);
                sum += value.OriginalY;
                if (queue.Count > windowWidth)
                    sum -= queue.Dequeue();
                value.AvgSmoothedY = sum / queue.Count;
                yield return value;
            }
        }
	}
}