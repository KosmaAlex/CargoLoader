﻿using CargoLoader.Domain.Attributes;
using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork.Services.Export
{
    public class ExportService : IExportService 
    {
        private readonly IDataService _dataService;

        
        
        public ExportService(IDataService dataService)
        {
            _dataService = dataService;
        }

        public async Task ExportToCSV<T>(string filePath)where T : DomainObject, IItem
        {
            IEnumerable<T> items = await _dataService.GetAll<T>();
            string _commonFileName = $@"{typeof(T).Name}{DateTime.Today.ToString().Remove(10)}";
            string _imageFolder = $@"{typeof(T).Name}Image\";
            string fileName = _commonFileName + ".csv";

            PropertyInfo[] props = typeof(T).GetProperties();
            StringBuilder sb = new StringBuilder();
            char separator = Constants.SeparatorCSV;
            Directory.CreateDirectory(filePath + _imageFolder);

            using (StreamWriter writer = new StreamWriter(filePath + fileName, false))
            {
                sb.AppendLine(string.Join(Constants.SeparatorCSV, props.Select(p => p.Name)));

                foreach(T item in items)
                {
                    foreach (PropertyInfo prop in props)
                    {
                        if(prop.GetCustomAttribute(typeof(ImageAttribute)) != null)
                        {
                            string path = filePath + _imageFolder + item.Marking + Constants.ImageSaveFormat;
                            byte[] thumbnail = (byte[])prop.GetValue(item, null);
                            File.WriteAllBytes(path, thumbnail);
                            sb.Append(@".\" + _imageFolder + item.Marking + Constants.ImageSaveFormat);
                        }                        
                        else
                        {
                            string temp = prop.GetValue(item, null).ToString().Replace(Constants.Comma, Constants.Dot);
                            
                            sb.Append(temp);
                        }

                        if(props.Last() != prop)
                        {
                            sb.Append(separator);
                        }
                    }
                    sb.AppendLine();
                }
                writer.Write(sb.ToString());
            }
        }        
    }
}
