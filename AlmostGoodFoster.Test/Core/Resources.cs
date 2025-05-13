using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlmostGoodFoster.Core;

namespace AlmostGoodFoster.Test.Core
{
    internal class Resources
    {
        internal static readonly string Tileset = "tileset";
        internal static readonly string Character = "character";

        internal ResourcesManager ResourcesManager { get; set; }

        public Resources()
        {
            ResourcesManager = new();
        }

        public void Load()
        {
            ResourcesManager.LoadTexture(Tileset, "tileset.png");
        }
    }
}
