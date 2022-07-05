using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Services
{
    public interface IItemDataService<T> : IDataService<T>
    {
        Task<T> GetByMarking(string marking);
        Task<IEnumerable<T>> GetByName(string name);
        Task<IEnumerable<T>> GetByLength(decimal maxLength, decimal minLength);
        Task<IEnumerable<T>> GetByWidth(decimal maxWidth, decimal minWidth);
        Task<IEnumerable<T>> GetByHeight(decimal maxHeight, decimal minHeight);
        Task<IEnumerable<T>> GetByVolume(decimal volume, decimal minVolume);
        Task<IEnumerable<T>> GetByWeight (decimal weight, decimal minWeight);
        Task<IEnumerable<T>> GetByIsFragile(bool isFragile);
        Task<IEnumerable<T>> GetByIsRotatable(bool isRotatable);
        Task<IEnumerable<T>> GetByIsProp(bool isProp);
        Task<IEnumerable<T>> GetByIsContainer(bool isContainer);
        Task<IEnumerable<T>> GetByCustomProperty(string propertyName, decimal parameter, decimal minParameter);
        Task<IEnumerable<T>> GetByCustomProperty(string propertyName, bool parameter);
        Task<int> GetTableCountAsync();
        Task<IEnumerable<T>> GetPageAsync(int page, int pageSize);
    }
}
