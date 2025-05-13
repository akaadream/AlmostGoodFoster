using System.Xml;
using System.Xml.Linq;
using Foster.Framework;

namespace AlmostGoodFoster.Graphics
{
    public class TextureAtlas
    {
        /// <summary>
        /// Gets or sets the texture of this texture atlas.
        /// </summary>
        public Texture? Texture { get; set; }

        private readonly Dictionary<string, TextureRegion> _regions = [];

        /// <summary>
        /// Creates a new texture atlas.
        /// </summary>
        public TextureAtlas()
        {
            _regions = [];
        }

        /// <summary>
        /// Create a new texture atlas using the given texture.
        /// </summary>
        /// <param name="texture"></param>
        public TextureAtlas(Texture texture)
        {
            Texture = texture;
        }

        /// <summary>
        /// Creates a new region and adds it to this texture atlas.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public void AddRegion(string name, int x, int y, int width, int height)
        {
            if (Texture == null)
            {
                return;
            }

            _regions.Add(name, new(Texture, x, y, width, height));
        }

        /// <summary>
        /// Gets the region from this texture atlas with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TextureRegion GetRegion(string name)
        {
            return _regions[name];
        }

        /// <summary>
        /// Gets the region from this texture atlas with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TextureRegion this[string name]
        {
            get
            {
                return GetRegion(name);
            }
        }

        /// <summary>
        /// Removes the region from this texture atlas with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool RemoveRegion(string name)
        {
            return _regions.Remove(name);
        }

        /// <summary>
        /// Removes all regions from this texture atlas.
        /// </summary>
        public void Clear()
        {
            _regions.Clear();
        }

        /// <summary>
        /// Creates a new texture atlas based on the xml configuratiokn file provided.
        /// </summary>
        /// <param name="graphicsDevice"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static TextureAtlas FromFile(GraphicsDevice graphicsDevice, string filename)
        {
            TextureAtlas textureAtlas = new();

            using Stream stream = File.OpenRead(filename);
            using XmlReader reader = XmlReader.Create(stream);

            XDocument document = XDocument.Load(reader);
            XElement? root = document.Root;

            if (root != null)
            {
                string texturePath = root.Element("Texture")?.Value ?? "";
                if (!string.IsNullOrEmpty(texturePath))
                {
                    textureAtlas.Texture = new Texture(graphicsDevice, new Image(texturePath));
                }

                var regions = root.Element("Regions")?.Elements("Region");

                if (regions != null)
                {
                    foreach (var region in regions)
                    {
                        string name = region.Attribute("name")?.Value ?? "";
                        int x = int.Parse(region.Attribute("x")?.Value ?? "0");
                        int y = int.Parse(region.Attribute("y")?.Value ?? "0");
                        int width = int.Parse(region.Attribute("width")?.Value ?? "0");
                        int height = int.Parse(region.Attribute("height")?.Value ?? "0");

                        if (!string.IsNullOrEmpty(name))
                        {
                            textureAtlas.AddRegion(name, x, y, width, height);
                        }
                    }
                }
            }

            

            return textureAtlas;
        }
    }
}
