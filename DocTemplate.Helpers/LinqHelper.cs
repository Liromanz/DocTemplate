using System.Collections.Generic;
using System.Linq;

namespace DocTemplate.Helpers
{
    public static class LinqHelper
    {
        /// <summary>
        /// Метод для поиска всех индексов значения в листе
        /// </summary>
        /// <typeparam name="T">Изначальный тип, где нужно искать</typeparam>
        /// <param name="values">Коллекция где искать</param>
        /// <param name="val">Значение, которое нужно искать</param>
        /// <returns>Все индексы вхождения значения в коллекцию</returns>
        public static int[] FindAllIndexof<T>(this IEnumerable<T> values, T val)
        {
            return values.Select((b, i) => object.Equals(b, val) ? i : -1).Where(i => i != -1).ToArray();
        }
    }
}
