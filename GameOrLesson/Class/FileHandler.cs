using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GameOrLesson.Class
{
    class FileHandler
    {
        private static readonly string FileContainingData = "infos.bin";
        public static void SerializeDataToFile(List<Base> Infos)
        {
            using (Stream stream = File.Open(FileContainingData, FileMode.Create))
            {
                var bformatter = new BinaryFormatter();
                bformatter.Serialize(stream, Infos);
            }
        }

        public static List<Base> Deserialize()
        {
            if (File.Exists(FileContainingData))
            {
                using (Stream stream = File.Open(FileContainingData, FileMode.Open))
                {
                    var bformatter = new BinaryFormatter();
                    return (List<Base>)bformatter.Deserialize(stream);
                }
            }
            return new List<Base>();

        }
    }
}
