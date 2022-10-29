using CargoLoader.Domain.Comparers;
using CargoLoader.Domain.Exceptions;
using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.GalacentreAPI.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.GalacentreAPI.Services
{
    public class GalacentreMappingService
    {

        private readonly GalacentreHttpService _httpService;
        private readonly IItemDataService<Product> _productDataService;

        public GalacentreMappingService(IItemDataService<Product> productDataService, GalacentreHttpService httpClient)
        {
            _httpService = httpClient;
            _productDataService = productDataService;
        }

        //TODO: remove httpClient to service
        public async Task LoadDataToDb()
        {
            IEnumerable<Product> products = await GetProductsAsync();

            foreach (Product product in products)
            {
                Product temp = await _productDataService.GetByMarkAsync(product.Marking);

                if (temp != null)
                {
                    _productDataService.Update(temp.Id, product);
                }
                else
                {
                    _productDataService.Create(product);
                }
            }
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            List<Product> products = new List<Product>();
            GalacentreResponse response = await _httpService.GetDataAsync();

            List<GalacentreDataObject> dataObjects = response.Data;
            List<GalacentreDataObject> sortedObjects = new List<GalacentreDataObject>();

            sortedObjects = dataObjects.Where(o => o.Specifications != null && o.Specifications.Count >= 3)
                .Where(o => o.Specifications.Any(s => s.Contains(Constants.SpecMaterial))
                        && o.Specifications.Any(s => s.Contains(Constants.SpecWeight))
                        && o.Specifications.Any(s => s.Contains(Constants.SpecDimensions)))
                .Where(o => o.Props != null && o.Props.Count >= 2)
                .Where(o => o.Props.Any(p => p.Contains(Constants.PropsType))
                        && o.Props.Any(p => p.Contains(Constants.PropsBrand)))
                .ToList();

            foreach (GalacentreDataObject dataObject in sortedObjects)
            {
                Product product = await Map(dataObject);

                if(product != null)
                {
                    products.Add(product);
                }
            }

            if(products.Count == 0)
            {
                throw new InvalidMappingException(Assembly.GetExecutingAssembly().FullName, typeof(Product)); 
            }

            return products;
        }

        private async Task<Product> Map(GalacentreDataObject dataObject)
        {
            decimal[] dimensions = DimensionsResolver(dataObject, out bool dimensionResolve);
            decimal weight = WeightResolver(dataObject, out bool weightResolve);
            bool isFragile = FragileResolver(dataObject, out bool fragileResolve);
            bool isRotatable = RotateResolver(dataObject);
            bool isProp = PropResolver(dataObject, out bool propResolve);
            string name = NameResolver(dataObject);
            byte[] thumbnail = await ThumbnailResolverAsync(dataObject);

            if (fragileResolve && dimensionResolve && propResolve && weightResolve)
            {
                Product product = new Product
                {
                    Marking = dataObject.Id,
                    Name = name,
                    Length = dimensions[0],
                    Width = dimensions[1],
                    Height = dimensions[2],
                    Weight = weight,
                    Volume = dimensions.Aggregate((a, b) => a * b),
                    IsFragile = isFragile,
                    IsRotatable = isRotatable,
                    IsProp = isProp,
                    Thumbnail = thumbnail
                };
                return product;
            }    
            return null;
        }


        private async Task<byte[]> ThumbnailResolverAsync(GalacentreDataObject dataObject)
        {
            byte[] thumbnail;

            if (!string.IsNullOrEmpty(dataObject.Image))
            {
                byte[] temp =  await _httpService.GetImageAsync(dataObject.Image);
                
                thumbnail = CreateThumbnail(temp, dataObject);

                return thumbnail;
            }
            else
            {
                string filePath = @"C:\Users\AlexK\source\repos\KosmaAlex\CargoLoader\cargoLoader.Domain\Resources\no_image.jpeg";

                thumbnail = File.ReadAllBytes(filePath);

                return thumbnail;
            }
        }

        private byte[] CreateThumbnail(byte[] image, GalacentreDataObject dataObject)
        {
            byte[] thumbnail;
            using (MemoryStream stream = new MemoryStream(), resizedStream = new MemoryStream())
            {
                stream.Write(image, 0, image.Length);
                Bitmap bitmap = new Bitmap(stream);
                int size = 200;
                Bitmap resizedBitmap = new Bitmap(bitmap, size, size);
                //TODO: forgot why is this here
                resizedBitmap.Save(resizedStream, ImageFormat.Png);
                resizedBitmap.Save(@"C:\Users\AlexK\source\repos\KosmaAlex\CargoLoader\Demo\Res\ProdThum\" + dataObject.Id + ".jpg", ImageFormat.Jpeg);
                thumbnail = resizedStream.ToArray();
                return thumbnail;
            }
        }

        private decimal WeightResolver(GalacentreDataObject dataObject, out bool weightResolve)
        {
            string temp = String.Format("{0:0.###}", dataObject.Specifications
                .FirstOrDefault(s => s.Contains(Constants.SpecWeight))
                .Replace(Constants.SpecWeight, String.Empty)
                .Replace("кг", String.Empty));

            weightResolve = decimal.TryParse(temp, out decimal result);

            return result;
        }

        private string NameResolver(GalacentreDataObject dataObject)
        {
            string firstPart = dataObject.Props
                .FirstOrDefault(s => s.Contains(Constants.PropsType))
                .Replace(Constants.PropsType, String.Empty);
            string secondPart = dataObject.Props
                .FirstOrDefault(s => s.Contains(Constants.PropsBrand))
                .Replace(Constants.PropsBrand, String.Empty);

            return $"{firstPart} {secondPart}";
        }

        private bool PropResolver(GalacentreDataObject dataObject, out bool propResolve)
        {            
            string material = dataObject.Specifications
                .FirstOrDefault(s => s.Contains(Constants.SpecMaterial));

            bool nonPropMaterial = Constants.NonPropMaterials
                .Any(s => material.Contains(s, StringComparison.InvariantCultureIgnoreCase));
            bool PropMaterial = Constants.PropMaterials
                .Any(s => material.Contains(s, StringComparison.InvariantCultureIgnoreCase));

            if (nonPropMaterial || PropMaterial)
            {
                propResolve = true;

                if (PropMaterial)
                {
                    return true;
                }
                return false;
            }

            propResolve = false;
            return false;
        }

        private bool RotateResolver(GalacentreDataObject dataObject)
        {
            bool rotateble = !dataObject.Specifications
                .Any(s => s.Contains(Constants.SpecPower, StringComparison.InvariantCultureIgnoreCase));

            return rotateble;
        }

        private decimal[] DimensionsResolver(GalacentreDataObject dataObject, out bool dimensionResolve)
        {
            string[] dimensions = dataObject.Specifications
                .FirstOrDefault(s => s.Contains(Constants.SpecDimensions))
                .Replace(Constants.SpecDimensions, String.Empty)
                .Replace("см", String.Empty)
                .Split(new[] { 'x', 'х' });

            decimal[] result = new decimal[dimensions.Length];

            if (dimensions.Length != 3)
            {
                dimensionResolve = false;
                return result;
            }

            for (int i = 0; i < dimensions.Length; i++)
            {
                bool parseCheck = decimal.TryParse(dimensions[i], out decimal parseResult);
                if (parseCheck)
                {
                    result[i] = parseResult;
                }
                else
                {
                    dimensionResolve = false;
                    return result;
                }
            }

            dimensionResolve = true;
            return result;
        }

        private bool FragileResolver(GalacentreDataObject dataObject, out bool fragileResolve)
        {            
            string material = dataObject.Specifications
                .FirstOrDefault(s => s.Contains(Constants.SpecMaterial));

            bool nonFragileMaterial = Constants.NonFragileMaterials
                .Any(s => material.Contains(s, StringComparison.InvariantCultureIgnoreCase));
            bool FragileMaterial = Constants.FragileMaterials
                .Any(s => material.Contains(s, StringComparison.InvariantCultureIgnoreCase));

            
            if (FragileMaterial || nonFragileMaterial)
            {
                fragileResolve = true;

                if (FragileMaterial)
                {
                    return true;
                }
                return false;
            }

            fragileResolve = false;
            return false;            
        }
    }
}
