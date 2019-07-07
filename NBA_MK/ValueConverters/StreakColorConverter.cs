using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace NBA_MK.ValueConverters
{
    class StreakColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return System.Windows.Media.Brushes.Black;

            if ((int)value > 0)
            {
                return System.Windows.Media.Brushes.DarkGreen;
            }
            else
            {
                return System.Windows.Media.Brushes.DarkRed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
