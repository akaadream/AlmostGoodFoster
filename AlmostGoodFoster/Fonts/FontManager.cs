using Foster.Framework;

namespace AlmostGoodFoster.Fonts
{
    public class FontManager()
    {
        public Dictionary<string, Font> Fonts { get; private set; } = [];

        public void Register(string name, Font font)
        {
            // Already exists
            if (Fonts.ContainsKey(name))
            {
                Log.Warning($"You cannot register the font {name} because this font already exists in the dictionary!");
                return;
            }

            Fonts.Add(name, font);
        }

        public Font? Get(string name)
        {
            if (Fonts.TryGetValue(name, out var font))
            {
                return font;
            }

            Log.Warning($"The font {name} cannot be found in the fonts dictionary!");
            return default;
        }
    }
}
