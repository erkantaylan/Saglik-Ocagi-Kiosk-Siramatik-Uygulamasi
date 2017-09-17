using Newtonsoft.Json;

namespace HealtCare.Common.Models {

    public class Options {
        [JsonProperty("saglik_ocagi_adi")]
        public string HealtCareCenterName { get; set; }
        [JsonProperty("kapatma_sifresi")]
        public string ExitPassword { get; set; }
    }

}