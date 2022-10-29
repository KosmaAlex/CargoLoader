using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork.Services.Export
{
    public interface IExportService<T>
    {
        Task ExportToCSV(string filePath);
    }
}
