using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Comparers
{
    public class ItemByMarkEqualityComparer<T> : IEqualityComparer<T> where T : IItem
    {
        public bool Equals(T? x, T? y)
        {
            return x.Marking == y.Marking;
        }

        public int GetHashCode([DisallowNull] T obj)
        {
            return obj.GetHashCode();
        }
    }
}
