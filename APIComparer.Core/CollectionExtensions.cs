namespace System.Collections.Generic
{
    static class CollectionExtensions
    {
        public static int IndexOf<T>(this IList<T> list, T item, IEqualityComparer<T> comparer)
        {
            if (list == null)
                throw new ArgumentNullException("list", "list is null.");
            if (comparer == null)
                throw new ArgumentNullException("comparer", "comparer is null.");

            for (var i = 0; i < list.Count; i++)
            {
                if (comparer.Equals(item, list[i]))
                    return i;
            }

            return -1;
        }
    }
}