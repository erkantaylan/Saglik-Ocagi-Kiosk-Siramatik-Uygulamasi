using HealtCare.Common.Controllers;
using Newtonsoft.Json;

namespace HealtCare.Common.Models {

    public class Options : ISaveble {
        [JsonProperty("saglik_ocagi_adi")]
        public string HealtCareCenterName { get; set; }

        [JsonProperty("kapatma_sifresi")]
        public string ExitPassword { get; set; }

        public void Save() {
            Saver<Options>.Save(this, MagicStrings.OptionsTxtLocation);
        }

        public static Options Load() {
            return Loader<Options>.Load(MagicStrings.OptionsTxtLocation);
        }
    }

}