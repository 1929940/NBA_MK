using System;
using System.Globalization;
using System.Windows.Data;

namespace NBA_MK.ValueConverters
{
    public class HeightToCmConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //1ft = 30.48cm
            //1in = 2.54cm
            //input formats: 0, 0-0, 0-00

            if (value is null)
            {
                return null;
            }
            string val = value.ToString();

            double feet = double.Parse(val[0].ToString());
            double inch = 0;

            if (val[1] == '-')
            {
                inch = double.Parse(val.Substring(2));
            }

            return (int)(feet * 30.48 + inch * 2.54) + " cm";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
