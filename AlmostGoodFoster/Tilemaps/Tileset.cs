using System.Numerics;
using Foster.Framework;

namespace AlmostGoodFoster.Tilemaps
{
    public class Tileset
    {
        public Texture Texture { get; set; }

        public List<Tile> Tiles { get; set; }
        public Color[]? _pixelData;

        public Tileset(GraphicsDevice graphicsDevice, string filename, bool autoCreateTiles = true, int tileSize = 0)
        {
            Texture = new(graphicsDevice, new Image(filename));
            Tiles = [];

            if (autoCreateTiles)
            {
                _pixelData = new Color[Texture.Width * Texture.Height];
                Texture.GetData<Color>(_pixelData);
                AutoCreateTiles(tileSize);
            }
        }

        public void AutoCreateTiles(int tileSize)
        {
            Tiles.Clear();

            HashSet<string> uniqueTiles = [];
            int guessedTileSize = tileSize != 0 ? tileSize : GuessTileSize();

            for (int y = 0; y < Texture.Height; y += guessedTileSize)
            {
                for (int x = 0; x < Texture.Width; x += guessedTileSize)
                {
                    var tile = new Tile(x, y, guessedTileSize, guessedTileSize);
                    string tileHash = GetTileHash(tile);
                    if (!uniqueTiles.Contains(tileHash))
                    {
                        Tiles.Add(tile);
                        uniqueTiles.Add(tileHash);
                    }
                }
            }
        }

        private int GuessTileSize()
        {
            for (int y = 1; y < Texture.Height; y++)
            {
                if (IsRowEmpty(y))
                {
                    return y;
                }
            }

            return 32;
        }

        private bool IsRowEmpty(int y)
        {
            if (_pixelData == null)
            {
                return false;
            }

            for (int x = 0; x < Texture.Width; x++)
            {
                if (_pixelData[y * Texture.Width + x].A != 0)
                {
                    return false;
                }
            }

            return false;
        }

        private string GetTileHash(Tile tile)
        {
            if (_pixelData == null)
            {
                return "";
            }

            List<Color> colors = [];
            for (int y = tile.Y; y < tile.Y + tile.Height; y++)
            {
                for (int x = tile.X; x < tile.X + tile.Width; x++)
                {
                    colors.Add(_pixelData[y * Texture.Width + x]);
                }
            }

            return string.Join(",", colors);
        }

        public void RenderTile(Batcher batcher, int x, int y, int index)
        {
            if (index >= Tiles.Count)
            {
                throw new Exception("The given tile index is out of bounds");
            }

            var tile = Tiles[index];
            batcher.Image(Texture, new RectInt(tile.X, tile.Y, tile.Width, tile.Height), new Vector2(x, y), Color.White);
        }
    }
}
