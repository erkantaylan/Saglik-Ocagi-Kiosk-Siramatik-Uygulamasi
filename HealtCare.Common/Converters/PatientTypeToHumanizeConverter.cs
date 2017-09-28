using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;

namespace HealtCare.Common.Converters {

    public class PatientTypeToHumanizeConverter : IValueConverter {
        private readonly Dictionary<string, string> patientTypes = new Dictionary<string, string> {
            {
                "Pregnant", "HAMİLE"
            }, {
                "Child", "5 YAŞ ALTI"
            }, {
                "Disabled", "ENGELLİ"
            }, {
                "Old", "65 YAŞ ÜSTÜ"
            }, {
                "Normal", "Normal"
            }, {
                "Visitor", "ZİYARETÇİ"
            }
        };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            string o = value.ToString();
            return patientTypes[o];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }

}