using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foster.Framework;

namespace AlmostGoodFoster.Core
{
    public class ResourcesManager(GraphicsDevice graphicsDevice)
    {
        public Dictionary<string, Texture> Textures { get; set; } = [];

        public void LoadTexture(string name, string filename)
        {
            Textures.Add(name, new(graphicsDevice, new Image(filename)));
        }
    }
}
