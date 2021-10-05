using System.Collections.Generic;

namespace yield
{
	public static class ExpSmoothingTask
	{
		public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
		{
            var previousValue = 0.0;
            var isFirstValue = true;
            foreach (var value in data)
            {
                if (isFirstValue) 
                {
                    value.ExpSmoothedY = value.OriginalY;
                    isFirstValue = false;
                }
                else value.ExpSmoothedY = alpha * value.OriginalY + (1 - alpha) * previousValue;
                yield return value;
                previousValue = value.ExpSmoothedY;
            }
		}
	}
}