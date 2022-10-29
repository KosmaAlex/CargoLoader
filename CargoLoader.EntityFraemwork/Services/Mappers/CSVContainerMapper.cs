using CargoLoader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.EntityFraemwork.Services.Mappers
{
    public class CSVContainerMapper
    {
        public IEnumerable<Container> Map(string filePath = default)
        {
            //filePath = @"C:\Users\AlexK\source\repos\KosmaAlex\CargoLoader\Demo\Res\ContainersList.csv";

            List<Container> result = new List<Container>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Container container = CreateEntity(line);

                    if(container != null)
                    {
                        result.Add(container);
                    }
                }
            }
            return result;
        }

        private Container CreateEntity(string line)
        {
            string[] properties = line.Split(',');

            byte[] image = default;

            string marking = properties[0];
            string name = properties[1];
            bool widthResolve = decimal.TryParse(properties[2],
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture, out decimal width);

            bool heightResolve = decimal.TryParse(properties[3],
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture, out decimal height);

            bool lengthResolve = decimal.TryParse(properties[4],
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture, out decimal length);

            bool volumeResolve = decimal.TryParse(properties[5],
                System.Globalization.NumberStyles.Number, 
                System.Globalization.CultureInfo.InvariantCulture, out decimal volume);

            bool weightResolve = decimal.TryParse(properties[6],
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture, out decimal weight);

            bool capacityResolve = decimal.TryParse(properties[7],
                System.Globalization.NumberStyles.Number,
                System.Globalization.CultureInfo.InvariantCulture, out decimal capacity);

            bool isFragile = false;
            bool isRotatableResolve = PermissionTo(properties[9], out bool isRotatable);
            bool isPropResolve = PermissionTo(properties[10], out bool isProp);
            bool imageResolve = GetThumbnail(marking, ref image);


            if(widthResolve && heightResolve && lengthResolve && volumeResolve && weightResolve && 
                capacityResolve && isRotatableResolve && isPropResolve && imageResolve)
            {
                Container container = new Container
                {
                    Marking = marking,
                    Name = name,
                    Width = width,
                    Height = height,
                    Length = length,
                    Volume = volume,
                    Weight = weight,
                    Capacity = capacity,
                    IsFragile = isFragile,
                    IsRotatable = isRotatable,
                    IsContainer = true,
                    IsProp = isProp,
                    Thumbnail = image,
                };
                return container;
            }
            return null;
        }

        private bool GetThumbnail(string name, ref byte[] image)
        {
            string path = @"C:\Users\AlexK\source\repos\KosmaAlex\CargoLoader\Demo\Res\ContThum\";

            //DirectoryInfo dirInfo = 
            //    new DirectoryInfo(@"C:\Users\AlexK\source\repos\KosmaAlex\CargoLoader\Demo\Res\ContThum\");

            string[] images = Directory.GetFiles(path);

            //FileInfo[] fileInfos = dirInfo.GetFiles();

            //foreach(FileInfo fileInfo in fileInfos)
            //{
            //    byte[] image = File.ReadAllBytes(fileInfo.FullName);
            //}
            
            try
            {
                image = File.ReadAllBytes(images
                    .FirstOrDefault(p => p.Contains(name, StringComparison.InvariantCultureIgnoreCase)));
            }

            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            return image != null;
        }

        private bool PermissionTo(string property, out bool result)
        {
            bool allowed = Constants.Allowed
                .Any(s => property.Contains(s, StringComparison.InvariantCultureIgnoreCase));
            bool forbidden = Constants.Forbidden
                .Any(s => property.Contains(s, StringComparison.InvariantCultureIgnoreCase));

            if(allowed || forbidden)
            {
                result = allowed;
                return true;
            }
            result = default;
            return false;
        }
    }
}
