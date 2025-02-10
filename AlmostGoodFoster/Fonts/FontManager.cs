using Foster.Framework;

namespace AlmostGoodFoster.Fonts
{
    public static class FontManager
    {
        public static Dictionary<string, Font> Fonts { get; private set; } = [];
        public static Dictionary<string, SpriteFont> SpriteFonts { get; private set; } = [];
        public static GraphicsDevice? GraphicsDevice { get; private set; }

        public static void Startup(GraphicsDevice graphicsDevice)
        {
            GraphicsDevice = graphicsDevice;
        }

        public static void Register(string name, Font font)
        {
            // Already exists
            if (Fonts.ContainsKey(name))
            {
                Log.Warning($"You cannot register the font {name} because this font already exists in the dictionary!");
                return;
            }

            Fonts.Add(name, font);
        }

        public static SpriteFont Register(string name, SpriteFont spriteFont)
        {
            // Already exists
            if (SpriteFonts.ContainsKey(name))
            {
                throw new InvalidOperationException($"You cannot register the sprite font {name} because this font already exists in the dictionary!");
            }

            SpriteFonts.Add(name, spriteFont);
            return spriteFont;
        }

        public static SpriteFont Get(string name, int fontSize)
        {
            if (Fonts.TryGetValue(name, out var font))
            {
                if (SpriteFonts.TryGetValue($"{name}-{fontSize}", out var spriteFont))
                {
                    return spriteFont;
                }
                else
                {
                    if (GraphicsDevice == null)
                    {
                        throw new InvalidOperationException($"Not possible to create a font without the graphics device instance.");
                    }

                    return Register($"{name}-{fontSize}", new SpriteFont(GraphicsDevice, font, fontSize));
                }
            }
            throw new InvalidOperationException($"The font {name} cannot be found in the fonts dictionary!");
        }
    }
}
