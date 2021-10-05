using System;
using System.Collections;
using System.Collections.Generic;

namespace hashes
{
    public class ReadonlyBytes : IEnumerable<byte>
    {
        private byte[] array;
        private int hash = -1;

        public ReadonlyBytes(params byte[] array)
        {
            this.array = array ?? throw new ArgumentNullException();
        }

        public override bool Equals(object obj)
        {
            if (obj is null || (obj.GetType() != GetType()))
                return false;
            var arr = obj as ReadonlyBytes;
            if (arr.Length != array.Length)
                return false;
            for (var i = 0; i < array.Length; i++)
                if (arr[i] != array[i])
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                if (hash != -1) 
                    return hash;
                hash = 1;
                for (var i = 0; i < array.Length; i++)
                    hash = (hash + array[i]) * 1023;
                return hash;
            }
        }

        public override string ToString()
        {
            var result = "";
            for (var i = 0; i < array.Length; i++)
            {
                if (i == array.Length - 1)
                    result += array[i].ToString();
                else
                    result += array[i].ToString() + ", ";
            }
            return "[" + result + "]";
        }

        public int Length { get { return array.Length; } }

        public IEnumerator<byte> GetEnumerator() { foreach (var e in array) yield return e; }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public byte this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) throw new IndexOutOfRangeException();
                return array[index];
            }
        }
    }
}