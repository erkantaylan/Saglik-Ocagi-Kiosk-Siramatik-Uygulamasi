using System.IO;
using Newtonsoft.Json;

namespace HealtCare.Common.Controllers {

    internal static class Loader<T> where T : new() {
        public static T Load(string path) {
            if (!File.Exists(path)) {
                return new T();
            }
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }

}