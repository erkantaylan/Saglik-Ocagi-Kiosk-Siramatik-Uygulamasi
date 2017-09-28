using System;
using System.Globalization;
using System.Windows.Data;
using HealtCare.Common.Factories;

namespace HealtCare.Common.Converters {

    public class PatientTypeToHumanizeConverter : IValueConverter {
        private readonly PatientTypeFactory patientTypeFactory = new PatientTypeFactory();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (value != null) {
                string o = value.ToString();
                return patientTypeFactory.PatientTypes[o];
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}