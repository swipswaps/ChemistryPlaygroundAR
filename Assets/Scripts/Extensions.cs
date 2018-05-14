using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common
{
    public static class Extensions
    {
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> items)
        {
            return new HashSet<T>(items);
        }

        public static bool IsSubsetListOf<K, V>(this List<K> thisList, List<K> thatList, Func<K, V> keyExtractor)
        {
            var thisSorted = thisList.OrderBy(keyExtractor).ToList();
            var thatSorted = thatList.OrderBy(keyExtractor);
            var j = 0;
            foreach (K that in thatSorted)
            {
                if (j == thisList.Count)
                {
                    break;
                }
                if (keyExtractor(thisSorted[j]).Equals(keyExtractor(that)))
                {
                    j++;
                }
            }
            return j >= thisList.Count;
        }

        public static void AddToList<K, V>(this Dictionary<K, List<V>> dict, K code, V node)
        {
            if (dict.ContainsKey(code))
            {
                var list = dict[code];
                list.Add(node);
            }
            else
            {
                var list = new List<V>();
                list.Add(node);
                dict.Add(code, list);
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T t in enumerable)
            {
                action(t);
            }
        }

        public static List<T> listOf<T>(params T[] vals)
        {
            return new List<T>(vals);
        }

        public static IEnumerable<R> Zip<T, K, R>(this IEnumerable<T> thisEnum, IEnumerable<K> thatEnum, Func<T, K, R> function)
        {
            return ZipIterator(thisEnum, thatEnum, function);
        }

        public static IEnumerable<Tuple<T, K>> Zip<T, K>(this IEnumerable<T> thisEnum, IEnumerable<K> thatEnum)
        {
            return ZipIterator(thisEnum, thatEnum, (x, y) => tupled(x, y));
        }

        private static IEnumerable<TResult> ZipIterator<TFirst, TSecond, TResult>(IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> resultSelector)
        {
            using (IEnumerator<TFirst> e1 = first.GetEnumerator())
            using (IEnumerator<TSecond> e2 = second.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    yield return resultSelector(e1.Current, e2.Current);
                }
            }
        }

        public static Tuple<A, B> tupled<A, B>(A a, B b)
        {
            return new Tuple<A, B>(a, b);
        }

        public static Tuple<A, B, C> tupled<A, B, C>(A a, B b, C c)
        {
            return new Tuple<A, B, C>(a, b, c);
        }

        public class Tuple<A, B>
        {
            public readonly A first;
            public readonly B second;
            public Tuple(A first, B second)
            {
                this.first = first;
                this.second = second;
            }
        }
        public class Tuple<A, B, C>
        {
            public readonly A first;
            public readonly B second;
            public readonly C third;
            public Tuple(A first, B second, C third)
            {
                this.first = first;
                this.second = second;
                this.third = third;
            }
        }
    }
}