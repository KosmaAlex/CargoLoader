using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.Domain.Services
{
    public interface IItemDataService : IDataService<IItem>
    {
        Task<IItem> GetByMarking(string marking);
        Task<IEnumerable<IItem>> GetByName(string name);
        Task<IEnumerable<IItem>> GetByLength(double maxLength, double minLength = default);
        Task<IEnumerable<IItem>> GetByWidth(double maxWidth, double minWidth = default);
        Task<IEnumerable<IItem>> GetByHeight(double maxHeight, double minHeight = default);
        Task<IEnumerable<IItem>> GetByVolume(double volume, double minVolume = default);
        Task<IEnumerable<IItem>> GetByWeight (double weight, double minWeight = default);
        Task<IEnumerable<IItem>> GetByIsFragile(bool isFragile);
        Task<IEnumerable<IItem>> GetByIsRotatable(bool isRotatable);
        Task<IEnumerable<IItem>> GetByIsProp(bool isProp);
        Task<IEnumerable<IItem>> GetByIsContainer(bool isContainer);
        Task<IEnumerable<IItem>> GetByCustomProperty(string propertyName, double parameter, double minParameter = default);
        Task<IEnumerable<IItem>> GetByCustomProperty(string propertyName, bool parameter);
    }
}
