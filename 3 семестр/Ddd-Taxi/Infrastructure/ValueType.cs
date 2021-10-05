using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Ddd.Taxi.Domain;

namespace Ddd.Infrastructure
{
    public class ValueType<T> 
        where T : class
    {
        private List<PropertyInfo> properties;

        public ValueType()
        {
            properties = GetType()
                .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .OrderBy(p => p.Name)
                .ToList();
        }

        public bool Equals(PersonName name) => Equals((object)name);

        public override bool Equals(object obj)
        {
            if (!(obj is ValueType<T> objAsT)) 
                return false;

            foreach (var prop in properties)
            {
                var obj1 = prop.GetValue(this);
                var obj2 = prop.GetValue(objAsT);

                if (obj1 == null && obj2 == null) continue;
                if (obj1 == null || obj2 == null) return false;
                if (!obj1.Equals(obj2)) return false;
            }
            return true;
        }

        public override string ToString()
        {
            var name = GetType().Name;
            var result = new StringBuilder(name + "(");
            foreach (var field in properties)
            {
                result.Append($"{field.Name}: {field.GetValue(this)}; ");
            }
            result.Remove(result.Length - 2, 2).Append(")");
            return result.ToString();
        }

        public override int GetHashCode()
        {
            int result = base.GetHashCode();
            foreach (var prop in properties)
            {
                if (prop.Name == "Name" 
                    || prop.Name == "BirthDate" 
                    || prop.Name == "Height")
                    result = (324567892 ^ prop.GetHashCode());
            }
            return result;
        }
    }
}