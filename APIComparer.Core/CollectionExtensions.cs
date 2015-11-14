using System.Collections.Generic;

internal static class CollectionExtensions
{
    public static int IndexOf<T>(this List<T> list, T item, IEqualityComparer<T> comparer)
    {
        for (var i = 0; i < list.Count; i++)
        {
            if (comparer.Equals(item, list[i]))
            {
                return i;
            }
        }

        return -1;
    }
}