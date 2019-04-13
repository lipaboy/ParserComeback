using System.Collections.Generic;

namespace PupaParserComeback.Export.Decorators
{
    public class Enumerable<T>
    {
        private readonly IEnumerator<T> _enumerator;

        public Enumerable(IEnumerable<T> source)
        {
            _enumerator = source.GetEnumerator();
        }

        public T Next(int count = 1)
        {
            while (count-- != 0)
            {
                if (!_enumerator.MoveNext()) return default(T);
            }

            return _enumerator.Current;
        }
    }
}
