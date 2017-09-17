using System.IO;
using Newtonsoft.Json;

namespace HealtCare.Common.Controllers {

    internal static class Saver<T> where T : new() {
        public static void Save(T item, string path) {
            string json = JsonConvert.SerializeObject(item);
            File.WriteAllText(path, json);
        }
    }

}