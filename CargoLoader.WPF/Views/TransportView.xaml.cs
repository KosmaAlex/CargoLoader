using CargoLoader.Domain.Models;
using CargoLoader.Domain.Services;
using CargoLoader.EntityFraemwork.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CargoLoader.WPF.Views
{
    /// <summary>
    /// Логика взаимодействия для TransportView.xaml
    /// </summary>
    public partial class TransportView : UserControl
    {
        public TransportView()
        {
            InitializeComponent();
        }

        IItemDataService<Container> dataService = new ItemDataService<Container>(
            new EntityFraemwork.CargoLoaderDbContextFactory
            ("Server=(localdb)\\mssqllocaldb;Database=CargoLoader;Trusted_Connection=True;"));
        ObservableCollection<System.Drawing.Image> _images = new ObservableCollection<System.Drawing.Image>();
        ObservableCollection<byte[]> _bImages = new ObservableCollection<byte[]>();
        byte[] bImage = null;
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //IEnumerable<Container> cl = await dataService.GetAll();

            //foreach(var item in cl)
            //{
            //    if(item.Image != null)
            //    {
            //        _bImages.Add(item.Image);
            //    }
            //}

            //listView.ItemsSource = _bImages;

            DirectoryInfo dirInfo = new DirectoryInfo(@"C:\Users\AlexK\Desktop\ContainersImages");

            FileInfo[] imagesFiles = dirInfo.GetFiles();

            foreach (var imageFile in imagesFiles)
            {

                System.Drawing.Image image = System.Drawing.Image.FromFile(imageFile.FullName);

                //System.Drawing.Image thumbnail = image.GetThumbnailImage(120, 120, null, IntPtr.Zero);

                bImage = File.ReadAllBytes(imageFile.FullName);

                //thumbnail.Save(@"C:\Users\AlexK\Desktop\ContThum\" + imageFile.Name.Replace('×', 'х'));

                _bImages.Add(bImage);
                //_images.Add(image);
            }
            listView.ItemsSource = _bImages;

        }
    }
}
