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
        void QueryByMarking(string? marking);
        void QueryByName(string? name);
        void QueryByLength(decimal? maxLength, decimal? minLength);
        void QueryByWidth(decimal? maxWidth, decimal? minWidth);
        void QueryByHeight(decimal? maxHeight, decimal? minHeight);
        void QueryByVolume(decimal? maxVolume, decimal? minVolume);
        void QueryByWeight (decimal? maxWeight, decimal? minWeight);
        void QueryByIsFragile(bool? isFragile);
        void QueryByIsRotatable(bool? isRotatable);
        void QueryByIsProp(bool? isProp);
        void QueryByIsContainer(bool? isContainer);
        Task<IEnumerable<T>> GetByCustomProperty(string propertyName, decimal parameter, decimal minParameter);
        Task<IEnumerable<T>> GetByCustomProperty(string propertyName, bool parameter);
        Task<int> GetTableCountAsync();
        Task<IEnumerable<T>> GetPageAsync(int page, int pageSize);
        Task<(IEnumerable<T> filteredPage, int filteredPageCount)> ExecuteFilteringQuery(int pageNumber, int pageSize);
    }
}
