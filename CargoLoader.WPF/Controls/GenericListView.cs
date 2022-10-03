using CargoLoader.Domain.Attributes;
using CargoLoader.WPF.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;

namespace CargoLoader.WPF.Controls
{
    public class GenericListView : ListView
    {
        private readonly IListingPageViewModel _listingPage;

        public GenericListView(IListingPageViewModel listingPage)
        {
            _listingPage = listingPage;
            CreateColumn(this);
        }

        
        private void CreateColumn(GenericListView genericListView)
        {
            GridView gridView = new GridView { AllowsColumnReorder = true};            

            PropertyInfo[] properties = _listingPage.GetType().GetGenericArguments()[0].GetProperties();
                       
            foreach(PropertyInfo property in properties)
            {
                BrowsableAttribute attributes = (BrowsableAttribute)property.GetCustomAttributes(true)
                    .FirstOrDefault(a => a is BrowsableAttribute);

                if(attributes != null && !attributes.Browsable)
                {
                    continue;
                }

                if (property.GetCustomAttribute(typeof(ImageAttribute)) != null)
                {
                    FrameworkElementFactory factory = new FrameworkElementFactory(typeof(Image));

                    Binding binding = new Binding()
                    {
                        Path = new PropertyPath(property.Name),
                        Mode = BindingMode.OneWay
                    };

                    factory.SetBinding(Image.SourceProperty, binding);


                    GridViewColumn column = new GridViewColumn()
                    {
                        CellTemplate = new DataTemplate() { VisualTree = factory }
                    };
                    column.Header = property.Name;
                    column.Width = 80;

                    gridView.Columns.Add(column);
                }
                else
                {
                    Binding binding = new Binding
                    {
                        Path = new PropertyPath(property.Name),
                        Mode = BindingMode.OneWay
                    };

                    GridViewColumn column = new GridViewColumn()
                    {
                        Header = property.Name,
                        DisplayMemberBinding = binding
                    };                                      

                    gridView.Columns.Add(column);

                }               

            }
            genericListView.View = gridView;
        }
    }
}
