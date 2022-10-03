using CargoLoader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork.Services.ExtensionMethods
{
    public static class QueryByThumbnailExtension
    {
        public static void QueryByThumbnail<T>(this IItemDataService<T> dataService, bool? withThumbnail)
        {
            string propertyName = "Thumbnail";
            dataService.QueryByCustomProperty<bool?>(propertyName, withThumbnail);
        }
    }
}
