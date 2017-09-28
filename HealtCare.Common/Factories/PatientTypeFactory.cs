using System.Collections.Generic;

namespace HealtCare.Common.Factories {

    public class PatientTypeFactory {
        public readonly Dictionary<string, string> PatientTypes = new Dictionary<string, string> {
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
    }

}