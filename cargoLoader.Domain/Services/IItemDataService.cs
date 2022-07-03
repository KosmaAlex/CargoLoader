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
        Task<IEnumerable<T>> GetByLength(double maxLength, double minLength);
        Task<IEnumerable<T>> GetByWidth(double maxWidth, double minWidth);
        Task<IEnumerable<T>> GetByHeight(double maxHeight, double minHeight);
        Task<IEnumerable<T>> GetByVolume(double volume, double minVolume);
        Task<IEnumerable<T>> GetByWeight (double weight, double minWeight);
        Task<IEnumerable<T>> GetByIsFragile(bool isFragile);
        Task<IEnumerable<T>> GetByIsRotatable(bool isRotatable);
        Task<IEnumerable<T>> GetByIsProp(bool isProp);
        Task<IEnumerable<T>> GetByIsContainer(bool isContainer);
        Task<IEnumerable<T>> GetByCustomProperty(string propertyName, double parameter, double minParameter);
        Task<IEnumerable<T>> GetByCustomProperty(string propertyName, bool parameter);
        Task<int> GetTableCountAsync();
        Task<IEnumerable<T>> GetPageAsync(int page, int pageSize);
    }
}
