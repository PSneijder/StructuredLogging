using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace StructuredLogging.Desktop.Utilities.Extensions
{
   public static class Extensions
    {
        public static void Sort<T>(this ObservableCollection<T> observable)
            where T : IComparable
        {
            List<T> sorted = observable.OrderBy(x => x).ToList();
            for (int i = 0; i < sorted.Count; i++)
            { 
                observable.Move(observable.IndexOf(sorted[i]), i);
            }
        }
    }
}