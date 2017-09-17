using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace HealtCare.Common.Converters {

    public class PathToImageSourceConverter : IValueConverter {
        #region Implementation of IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string path = value as string;
            if (path != null) {
                BitmapImage img = new BitmapImage(new Uri(path));
                return img;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }

        #endregion
    }

}