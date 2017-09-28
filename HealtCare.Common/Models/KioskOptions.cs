using System.ComponentModel;
using HealtCare.Common.Controllers;
using Newtonsoft.Json;

namespace HealtCare.Common.Models {

    public class KioskOptions {
        [JsonProperty("saglik_ocagi_adi")]
        public string HealtCareCenterName { get; set; }

        [JsonProperty("kapatma_sifresi")]
        public string ExitPassword { get; set; }

        [JsonProperty("satir_sayisi", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(2)]
        public int Rows { get; set; }

        [JsonProperty("sutun_sayisi", DefaultValueHandling = DefaultValueHandling.Populate)]
        [DefaultValue(3)]
        public int Columns { get; set; }

        public static void Save(KioskOptions o) {
            Saver<KioskOptions>.Save(o, MagicStrings.OptionsTxtLocation);
        }

        public static KioskOptions Load() {
            return Loader<KioskOptions>.Load(MagicStrings.OptionsTxtLocation);
        }
    }

}