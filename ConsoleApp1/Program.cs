using CargoLoader.Domain.Exceptions;
using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork;
using CargoLoader.EntityFraemwork.Services;
using CargoLoader.EntityFraemwork.Services.ExtensionMethods;
using CargoLoader.GalacentreAPI;
using CargoLoader.GalacentreAPI.Models;
using System.Diagnostics;

namespace ConsoleApp1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            TestDeserealization();

            //IOrderDataService orderDataService = new OrderDataService(new CargoLoaderDbContextFactory());
            //IItemDataService<Product> productDataService = new ItemDataService<Product>(new CargoLoaderDbContextFactory());
            //IItemDataService<Container> containerDataService = new ItemDataService<Container>(new CargoLoaderDbContextFactory());

            ////Fill();

            //List<Container> containers = (List<Container>)containerDataService.GetByCapacityAsync<Container>(40).Result;
            ////List<Container> containers = (List<Container>)containerDataService.GetByCustomProperty("IsFragile", false).Result;

            //foreach(Container container in containers)
            //{
            //    Console.WriteLine($"{container.Name} {container.Id} {container.Capacity}");
            //}

            ////Test();

            //TestException();


            Console.WriteLine("Done");
            Console.ReadKey();
        }

        public static async void TestDeserealization()
        {
            GalacentreHttpClient client = new GalacentreHttpClient(new HttpClient(),
                new GalacentreApiKey(""));

            GalacentreResponse result = await client.GetAsync<string>("&store=msk&select=props,specifications,name");
                  
            foreach(GalacentreDataObject data in result.Data)
            {
                Console.WriteLine(data);
            }
        }

        public async static void Fill()
        {
            IOrderDataService orderDataService = new OrderDataService(new CargoLoaderDbContextFactory());
            IItemDataService<Product> productDataService = new ItemDataService<Product>(new CargoLoaderDbContextFactory());
            IItemDataService<Container> containerDataService = new ItemDataService<Container>(new CargoLoaderDbContextFactory());

            Product productB = new Product()
            {
                Marking = "Block:9x7x5",
                Name = "Block",
                Height = 5,
                Width = 7,
                Length = 9,
                Weight = 3.5,
                IsFragile = false,
                IsContainer = false,
                IsProp = true,
                IsRotatable = true
            };            

            Product productA = new Product()
            {
                Marking = "Brick:2x2x4",
                Name = "Brick",
                Height = 4,
                Width = 2,
                Length = 2,
                Weight = 3.5,
                IsFragile = false,
                IsContainer = false,
                IsProp = true,
                IsRotatable = true
            };

            await productDataService.Create(productB);
            await productDataService.Create(productA);

            Container containerB = new Container()
            {
                Marking = "Carton:8x4x4",
                Name = "Carton",
                Height = 4,
                Width = 4,
                Length = 8,
                Weight = 0.5,
                IsFragile = false,
                IsContainer = true,
                IsProp = true,
                IsRotatable = true,
                Capacity = 20
            };

            Container containerA = new Container()
            {
                Marking = "Box:16x8x10",
                Name = "Box",
                Height = 10,
                Width = 8,
                Length = 16,
                Weight = 0.5,
                IsFragile = false,
                IsContainer = true,
                IsProp = true,
                IsRotatable = true,
                Capacity = 40
            };

            await containerDataService.Create(containerA);
            await containerDataService.Create(containerB);
        }

        public static void Test()
        {
            IOrderDataService orderDataService = new OrderDataService(new CargoLoaderDbContextFactory());
            IItemDataService<Product> productDataService = new ItemDataService<Product>(new CargoLoaderDbContextFactory());
            IItemDataService<Container> containerDataService = new ItemDataService<Container>(new CargoLoaderDbContextFactory());

            TestProductRangeParametr(productDataService.GetByHeight, 5);
            TestContainerRangeParametr(containerDataService.GetByHeight, 12, 2);
            Console.WriteLine("-------------------");

            TestProductRangeParametr(productDataService.GetByLength, 9);
            TestContainerRangeParametr(containerDataService.GetByLength, 18, 6);
            Console.WriteLine("-------------------");

            TestProductRangeParametr(productDataService.GetByWidth, 7);
            TestContainerRangeParametr(containerDataService.GetByWidth, 10, 2);
            Console.WriteLine("-------------------");

            TestProductRangeParametr(productDataService.GetByVolume, 315);
            TestContainerRangeParametr(containerDataService.GetByVolume, 1300, 120);
            Console.WriteLine("-------------------");

            TestProductRangeParametr(productDataService.GetByWeight, 3.5);
            TestContainerRangeParametr(containerDataService.GetByWeight, 10, 0);
            Console.WriteLine("-------------------");

            TestProduct<bool>(productDataService.GetByIsRotatable, false);
            TestContainer<bool>(containerDataService.GetByIsRotatable, true);
            Console.WriteLine("-------------------");

            TestProduct<bool>(productDataService.GetByIsProp, true);
            TestContainer<bool>(containerDataService.GetByIsProp, false);
            Console.WriteLine("-------------------");

            TestProduct<bool>(productDataService.GetByIsContainer, false);
            TestContainer<bool>(containerDataService.GetByIsContainer, true);
            Console.WriteLine("-------------------");

            TestProduct<bool>(productDataService.GetByIsFragile, true);
            TestContainer<bool>(containerDataService.GetByIsFragile, false);
            Console.WriteLine("-------------------");

            TestProduct<string>(productDataService.GetByName, "ck");
            TestContainer<string>(containerDataService.GetByName, "Box");
            Console.WriteLine("-------------------");

            TestProductMarking(productDataService.GetByMarking, "Block:9x7x");
            TestContainerMarking(containerDataService.GetByMarking, "Carton:8x4x4");
            Console.WriteLine("-------------------");
        }
        public static async void TestException()
        {
            IOrderDataService orderDataService = new OrderDataService(new CargoLoaderDbContextFactory());
            IItemDataService<Product> productDataService = new ItemDataService<Product>(new CargoLoaderDbContextFactory());
            IItemDataService<Container> containerDataService = new ItemDataService<Container>(new CargoLoaderDbContextFactory());

            Order order = new Order();
            order.OrderNumber = "111";

            try
            {
                await orderDataService.Delete(3);
            }
            catch (EntityDoesNotExistException ex)
            {
                Console.WriteLine(ex.EntityName);
                Console.WriteLine(ex.RequestedParameter);
                Console.WriteLine(ex.ParameterValue);
            }
            Console.WriteLine(order.Id);
            //try
            //{
            //    await containerDataService.GetByHeight(11);
            //}
            //catch (ItemNotFoundException ex)
            //{
            //    Console.WriteLine(ex.RequestedParameter);
            //}
            //catch(Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}

            //try
            //{
            //    await containerDataService.GetByWidth(11);
            //}
            //catch (ItemNotFoundException ex)
            //{
            //    Console.WriteLine(ex.RequestedParameter);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
        }

        #region TestMethods
        delegate Task<IEnumerable<IItem>> GetBy(double parameter, double minParameter);
        public static void TestProductRangeParametr<T>(Func<double, double ,Task<IEnumerable<T>>> func,
            double parameter, double minParameter = default)
        {
            Console.WriteLine(func.Method.Name);

            List<Product> products = (List<Product>)func(parameter, minParameter).Result;

            foreach (Product product in products)
            {
                Console.WriteLine($"{product.Id}, {product.Name}," +
                    $" {product.Marking}, {product.Volume} by {func.Method.Name} {parameter}");
            }
        }

        public static void TestContainerRangeParametr<T>(Func<double, double, Task<IEnumerable<T>>> func,
            double parameter, double minParameter = default)
        {
            Console.WriteLine(func.Method.Name);

            List<Container> containers = (List<Container>)func(parameter, minParameter).Result;

            foreach (Container container in containers)
            {
                Console.WriteLine($"{container.Id}, {container.Name}, {container.Marking}," +
                    $" {container.Volume}, {container.Capacity} by {func.Method.Name} {parameter}");
            }
        }

        public static void TestProduct<T>(Func<T, Task<IEnumerable<Product>>> func, T parameter)
        {
            Console.WriteLine(func.Method.Name);

            List<Product> products = (List<Product>)func(parameter).Result;

            foreach(Product product in products)
            {
                Console.WriteLine($"{product.Id}, {product.Name}," +
                    $" {product.Marking}, {product.Volume} by {func.Method.Name} {parameter}");
            }
        }

        public static void TestContainer<T>(Func<T, Task<IEnumerable<Container>>> func, T parameter)
        {
            Console.WriteLine(func.Method.Name);

            List<Container> Containers = (List<Container>)func(parameter).Result;

            foreach (Container container in Containers)
            {
                Console.WriteLine($"{container.Id}, {container.Name}," +
                    $" {container.Marking}, {container.Volume} by {func.Method.Name} {parameter}");
            }
        }

        public static void TestProductMarking(Func<string, Task<Product>> func, string parameter)
        {
            Console.WriteLine(func.Method.Name);

            try
            {
                Product product = (Product)func(parameter).Result;

                Console.WriteLine($"{product.Id}, {product.Name}," +
                $" {product.Marking}, {product.Volume} by {func.Method.Name} {parameter}");
            }
            catch (ItemNotFoundException e)
            {
                Debug.WriteLine(e.RequestedParameter);
                Debug.WriteLine("-------------");
            }    
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException.Message);
            }
        }

        public static void TestContainerMarking(Func<string, Task<Container>> func, string parameter)
        {
            Console.WriteLine(func.Method.Name);

            Container Container = (Container)func(parameter).Result;

            Console.WriteLine($"{Container.Id}, {Container.Name}," +
                $" {Container.Marking}, {Container.Volume} by {func.Method.Name} {parameter}");
        }
        #endregion
    }
}