using System;
using System.Collections.Generic;
using System.Reflection;

namespace Reflection.Randomness
{
    public class Generator<T>
        where T : new()
    {
        static readonly List<Tuple<PropertyInfo, FromDistribution>> tuples;

        static Generator()
        {
            tuples = new List<Tuple<PropertyInfo, FromDistribution>>();

            foreach (var property in typeof(T).GetProperties())
            {
                if (Attribute.GetCustomAttribute(property, typeof(FromDistribution)) 
                    is FromDistribution attribute)
                {
                    tuples.Add(Tuple.Create(property, attribute));
                }
            }
        }

        public T Generate(Random rnd)
        {
            var result = new T();

            foreach (var tuple in tuples)
            {
                tuple.Item1.SetValue(result, tuple.Item2.Create().Generate(rnd));
            }
            return result;
        }
    }

    public class FromDistribution : Attribute
    {
        public readonly Type Type;
        public readonly object[] Numbers;

        public FromDistribution(Type type, params object[] numbers)
        {
            Type = type;
            Numbers = numbers;
        }

        public IContinuousDistribution Create()
        {
            try
            {
                return (IContinuousDistribution)Activator.CreateInstance(Type, Numbers);
            }
            catch
            {
                throw new ArgumentException(Type.FullName);
            }
        }
    }
}