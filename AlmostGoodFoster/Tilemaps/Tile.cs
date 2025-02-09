namespace AlmostGoodFoster.Tilemaps
{
    public struct Tile(int x, int y, int width, int height)
    {
        /// <summary>
        /// X position of the tile
        /// </summary>
        public int X { get; set; } = x;

        /// <summary>
        /// Y position of the tile
        /// </summary>
        public int Y { get; set; } = y;

        /// <summary>
        /// Width of the tile
        /// </summary>
        public int Width { get; set; } = width;

        /// <summary>
        /// Height of the tile
        /// </summary>
        public int Height { get; set; } = height;
    }
}
