using System;
using System.Globalization;
using System.Windows.Data;

namespace NBA_MK.ValueConverters
{
    public class WeightToKgConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // 1 pound = 0.453592kg

            if (value is null) return null;

            return (int)(System.Convert.ToDouble(value) * 0.453592) + " kg";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
