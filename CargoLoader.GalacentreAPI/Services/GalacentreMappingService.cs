using CargoLoader.Domain.Exceptions;
using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.GalacentreAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CargoLoader.GalacentreAPI.Services
{
    public class GalacentreMappingService
    {

        //TODO: remove httpClient to service
        private readonly GalacentreHttpClient _httpClient;
        private readonly IItemDataService<Product> _productDataService;

        public GalacentreMappingService(IItemDataService<Product> productDataService, GalacentreHttpClient httpClient)
        {
            _httpClient = httpClient;
            _productDataService = productDataService;
        }

        //TODO: remove httpClient to service
        public async Task LoadDataToDb()
        {
            IEnumerable<Product> products = await GetDataAsync();

            foreach (Product product in products)
            {
                _productDataService.Create(product);
            }
        }

        public async Task<IEnumerable<Product>> GetDataAsync()
        {
            List<Product> products = new List<Product>();
            GalacentreResponse response = await _httpClient.GetAsync();

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
                Product product = Mapping(dataObject);

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

        private Product Mapping(GalacentreDataObject dataObject)
        {
            double[] dimensions = DimensionsResolve(dataObject, out bool dimensionResolve);
            double weight = WeightResolve(dataObject, out bool weightResolve);
            bool isFragile = FragileResolve(dataObject, out bool fragileResolve);
            bool isRotatable = RotateResolve(dataObject);
            bool isProp = PropResolve(dataObject, out bool propResolve);
            string name = NameResolve(dataObject);

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
                    IsFragile = isFragile,
                    IsRotatable = isRotatable,
                    IsProp = isProp
                };
                return product;
            }    
            return null;
        }

        private double WeightResolve(GalacentreDataObject dataObject, out bool weightResolve)
        {
            string temp = String.Format("{0:0.###}", dataObject.Specifications
                .FirstOrDefault(s => s.Contains(Constants.SpecWeight))
                .Replace(Constants.SpecWeight, String.Empty)
                .Replace("кг", String.Empty));

            weightResolve = double.TryParse(temp, out double result);

            return result;
        }

        private string NameResolve(GalacentreDataObject dataObject)
        {
            string firstPart = dataObject.Props
                .FirstOrDefault(s => s.Contains(Constants.PropsType))
                .Replace(Constants.PropsType, String.Empty);
            string secondPart = dataObject.Props
                .FirstOrDefault(s => s.Contains(Constants.PropsBrand))
                .Replace(Constants.PropsBrand, String.Empty);

            return $"{firstPart} {secondPart}";
        }

        private bool PropResolve(GalacentreDataObject dataObject, out bool propResolve)
        {            
            string material = dataObject.Specifications
                .FirstOrDefault(s => s.Contains(Constants.SpecMaterial));

            bool nonPropMaterial = Constants.nonPropMaterials
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

        private bool RotateResolve(GalacentreDataObject dataObject)
        {
            bool rotateble = !dataObject.Specifications
                .Any(s => s.Contains(Constants.SpecPower, StringComparison.InvariantCultureIgnoreCase));

            return rotateble;
        }

        private double[] DimensionsResolve(GalacentreDataObject dataObject, out bool dimensionResolve)
        {
            string[] dimensions = dataObject.Specifications
                .FirstOrDefault(s => s.Contains(Constants.SpecDimensions))
                .Replace(Constants.SpecDimensions, String.Empty)
                .Replace("см", String.Empty)
                .Split(new[] { 'x', 'х' });            

            double[] result = new double[dimensions.Length];

            if (dimensions.Length != 3)
            {
                dimensionResolve = false;
                return result;
            }

            for (int i = 0; i < dimensions.Length; i++)
            {
                bool parseCheck = double.TryParse(dimensions[i], out double parseResult);
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

        private bool FragileResolve(GalacentreDataObject dataObject, out bool fragileResolve)
        {            
            string material = dataObject.Specifications
                .FirstOrDefault(s => s.Contains(Constants.SpecMaterial));

            bool nonFragileMaterial = Constants.nonFragileMaterials
                .Any(s => material.Contains(s, StringComparison.InvariantCultureIgnoreCase));
            bool FragileMaterial = Constants.fragileMaterials
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
