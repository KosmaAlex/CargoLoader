using CargoLoader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace CargoLoader.WPF.Controls
{
    public class GenericListView : ListView
    {
        
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property.Name == "ItemsSource"
                && e.OldValue != e.NewValue
                && e.NewValue != null)
            {
                CreateColumn(this);
            }
        }

        private static void CreateColumn(GenericListView genericListView)
        {
            GridView gridView = new GridView { AllowsColumnReorder =true};

            PropertyInfo[] properties = genericListView.ItemsSource.GetType().GetGenericArguments()[0].GetProperties();

            foreach(PropertyInfo property in properties)
            {
                BrowsableAttribute attributes = (BrowsableAttribute)property.GetCustomAttributes(true)
                    .FirstOrDefault(a => a is BrowsableAttribute);

                if(attributes != null && !attributes.Browsable)
                {
                    continue;
                }

                Binding binding = new Binding { Path = new PropertyPath(property.Name), Mode = BindingMode.OneWay };                
                GridViewColumn gridViewColumn = new GridViewColumn() { Header = property.Name, DisplayMemberBinding = binding };
                gridView.Columns.Add(gridViewColumn);
            }
            genericListView.View = gridView;
        }
    }
}
