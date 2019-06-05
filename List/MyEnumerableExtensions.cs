using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace List
{

    class SkipEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> collection;
        private readonly int countOfIetmsToSkip;
        public SkipEnumerable(IEnumerable<T> collection, int countOfIetmsToSkip)
        {
            this.collection = collection;
            this.countOfIetmsToSkip = countOfIetmsToSkip;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var en = collection.GetEnumerator();
            for (int i = 0; i < countOfIetmsToSkip; i++)         
                en.MoveNext();
            return en;          
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    class TakeEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> collection;
        private readonly int countOfIetmsToTake;

        public TakeEnumerable(IEnumerable<T> collection, int countOfIetmsToTake)
        {
            this.collection = collection;
            this.countOfIetmsToTake = countOfIetmsToTake;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new TakeEnumerator<T>(collection, countOfIetmsToTake);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class TakeEnumerator<T> : IEnumerator<T>
    {
        private readonly IEnumerable<T> collection;
        private readonly int countOfIetmsToTake;

        private IEnumerator<T> en;
        private int i;

        public TakeEnumerator(IEnumerable<T> collection, int countOfIetmsToTake)
        {
            this.collection = collection;
            this.countOfIetmsToTake = countOfIetmsToTake;
            Reset();
        }

        public T Current => en.Current;

        object IEnumerator.Current => Current;

        public void Dispose()
        { }

        public bool MoveNext()
        {
            i++;
            return en.MoveNext() && i <= countOfIetmsToTake;
        }

        public void Reset()
        {
            en = collection.GetEnumerator();
        }
    }

    static class MyEnumerableExtensions
    {
        public static IEnumerable<T> Skip<T>(IEnumerable<T> collection , int countOfIetmsToSkip)
        {
            return new SkipEnumerable<T>(collection, countOfIetmsToSkip);
        }


        public static IEnumerable<T> Take<T>(IEnumerable<T> collection, int countOfIetmsToTake)
        {
            return new TakeEnumerable<T>(collection, countOfIetmsToTake);
        }

    }
}
