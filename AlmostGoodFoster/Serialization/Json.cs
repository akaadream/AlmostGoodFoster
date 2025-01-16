using System.Text.Json;

namespace AlmostGoodFoster.Serialization
{
    public static class Json
    {
        public static async Task Save<T>(T instance, string filename)
        {
            JsonSerializerOptions serializerOptions = new();

#if DEBUG
            serializerOptions.WriteIndented = true;
#else
            serializerOptions.WriteIntended = false;
#endif
            await using FileStream fileStream = File.Open(filename, FileMode.OpenOrCreate);
            await JsonSerializer.SerializeAsync(fileStream, instance, serializerOptions);
        }

        public static async Task<T?> Load<T>(string filename)
        {
            using FileStream fileStream = File.OpenRead(filename);
            T? instance = await JsonSerializer.DeserializeAsync<T>(fileStream);
            return instance;
        }
    }
}
