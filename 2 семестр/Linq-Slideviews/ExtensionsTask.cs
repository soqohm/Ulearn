using System;
using System.Collections.Generic;
using System.Linq;

namespace linq_slideviews
{
    public static class ExtensionsTask
    {
        /// <summary>
        /// Медиана списка из нечетного количества элементов — это серединный элемент списка.
        /// Медиана списка из четного количества элементов — среднее арифметическое двух серединных элементов списка.
        /// </summary>
        /// <exception cref="InvalidOperationException">Если последовательность не содержит элементов</exception>
        public static double Median(this IEnumerable<double> items)
        {
            var sorted = items
                .OrderBy(n => n)
                .ToArray();

            if (sorted.Count() < 1) throw new InvalidOperationException();
            var c = sorted.Count();
            if (c % 2 == 1) return sorted[c / 2];
            else return 0.5 * (sorted[c / 2 - 1] + sorted[c / 2]);
        }

        /// <returns>
        /// Возвращает последовательность, состоящую из пар соседних элементов.
        /// Например, по последовательности {1,2,3} метод должен вернуть две пары: (1,2) и (2,3).
        /// </returns>
        public static IEnumerable<Tuple<T, T>> Bigrams<T>(this IEnumerable<T> items)
        {
            var queue = new Queue<T>();
            foreach (var e in items)
            {
                queue.Enqueue(e);
                if (queue.Count == 2)
                    yield return Tuple.Create(queue.Dequeue(), queue.Peek());
            }
        }
    }
}