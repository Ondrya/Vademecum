using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Helpers
{
    public static class Extensions
    {
        /// <summary>
        /// Содержит ли строка заданную подстроку регистронезависимо
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="stringComparison"></param>
        /// <returns></returns>
        public static bool CaseInsensitiveContains(this string text, string value,
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }


        /// <summary>
        /// Отсортировать ObservableCollection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void SortObservableCollection<T>(this ObservableCollection<T> collection) where T : IComparable
        {
            List<T> sorted = collection.OrderBy(x => x).ToList();
            for (int i = 0; i < sorted.Count(); i++)
                collection.Move(collection.IndexOf(sorted[i]), i);
        }
    }
}
