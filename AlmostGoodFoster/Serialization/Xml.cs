using System.Xml.Serialization;

namespace AlmostGoodFoster.Serialization
{
    public static class Xml
    {
        public static async Task Save<T>(T instance, string filename)
        {
            XmlSerializer serializer = new(typeof(T));
            await using FileStream fileStream = File.Open(filename, FileMode.OpenOrCreate);
            await Task.Run(() => serializer.Serialize(fileStream, instance));
        }

        public static async Task<T?> Load<T>(string filename)
        {
            XmlSerializer serializer = new(typeof(T));
            using FileStream fileStream = File.OpenRead(filename);

            return await Task.Run(() =>
            {
                return (T?)serializer.Deserialize(fileStream);
            });
        }
    }
}
