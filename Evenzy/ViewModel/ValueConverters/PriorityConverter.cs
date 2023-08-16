using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;

namespace Evenzy.ViewModel.ValueConverters
{
    public class PriorityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            string priority = value.ToString();
            if (priority == "Low")
                return 0;
            if(priority == "Medium")
                return 1;
            if(priority == "High")
                return 2;
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int priority = (int)value;
            if (priority == 0)
                return "Low";
            if (priority == 1)
                return "Medium";
            if (priority == 2)
                return "High";
            return null;
        }
    }
}
