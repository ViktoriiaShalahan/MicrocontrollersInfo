using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Common.IO {
    public class GenericBinarySerializationController<T> where T : class {

        public string FileExtension { get; set; }
        public string FileTypeCaption { get; set; }

        public GenericBinarySerializationController() {
            FileExtension = ".dat";
            FileTypeCaption = "Двійкові файли";
        }

        public T Load(string filePath) {
            Path.ChangeExtension(filePath, FileExtension);
            if (!File.Exists(filePath))
                return null;
            BinaryFormatter bFormatter = new BinaryFormatter();
            using (FileStream fSteam = File.OpenRead(filePath)) {
                return (T)bFormatter.Deserialize(fSteam);
            }
        }

        public void Save(T data, string filePath) {
            filePath = Path.ChangeExtension(filePath, FileExtension);
            BinaryFormatter bFormatter = new BinaryFormatter();
            using (var fStream = File.OpenWrite(filePath)) {
                bFormatter.Serialize(fStream, data);
            }
        }
    }
}
