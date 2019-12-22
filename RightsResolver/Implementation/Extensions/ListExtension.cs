using System.Collections.Generic;
using System.Security.Policy;

namespace RightsResolver.Implementation.Extensions
{
    public static class ListExtension
    {
        public static void ConcatOrCreate<T>(this List<T> list1, List<T> list2)
        {
            if (list1 == null) list1 = list2;
            else
            {
                list1.AddRange(list2);
            }
        }
    }
}