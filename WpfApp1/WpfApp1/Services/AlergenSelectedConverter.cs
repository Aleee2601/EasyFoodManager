using EasyFoodManager.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace EasyFoodManager.Services
{
    public class AlergenSelectedConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var list = value as ObservableCollection<Alergen>;
            var current = parameter as Alergen;

            return list?.Any(a => a.Id == current?.Id) == true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isChecked = (bool)value;
            var current = parameter as Alergen;

            if (Application.Current.MainWindow.DataContext is not INotifyPropertyChanged vm) return Binding.DoNothing;

            var prop = vm.GetType().GetProperty("AlergeniSelectati");
            if (prop == null) return Binding.DoNothing;

            var list = prop.GetValue(vm) as ObservableCollection<Alergen>;
            if (list == null || current == null) return Binding.DoNothing;

            if (isChecked && !list.Any(a => a.Id == current.Id))
                list.Add(current);
            else if (!isChecked)
                list.Remove(list.FirstOrDefault(a => a.Id == current.Id));

            return Binding.DoNothing;
        }
    }
}
