using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace JlgCommon.Logic
{
    public class ByteArrayConverter
    {
        public byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
            {
                return null;
            }                

            var bf = new BinaryFormatter();
            var ms = new MemoryStream();
            bf.Serialize(ms, obj);

            return ms.ToArray();
        }

        public object ByteArrayToObject(byte[] arrBytes)
        {

            if (arrBytes == null)
            {
                return null;
            }

            var memStream = new MemoryStream();
            var binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = binForm.Deserialize(memStream);

            return obj;
        }

    }
}
