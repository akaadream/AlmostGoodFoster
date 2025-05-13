using System.Numerics;
using Foster.Framework;

namespace AlmostGoodFoster.Graphics
{
    /// <summary>
    /// Creates a new texture region using the specified source texture.
    /// </summary>
    /// <param name="texture"></param>
    /// <param name="sourceRectangle"></param>
    public class TextureRegion(Texture texture, RectInt sourceRectangle)
    {
        /// <summary>
        /// Gets or sets the source texture this texture region is part of.
        /// </summary>
        public Texture Texture { get; set; } = texture;

        /// <summary>
        /// Gets or sets the source rectangle boundary of this texture region within the source texture.
        /// </summary>
        public RectInt SourceRectangle { get; set; } = sourceRectangle;

        /// <summary>
        /// Gets the width, in pixels, of the texture region.
        /// </summary>
        public int Width => Texture.Width;

        /// <summary>
        /// Gets the height, in pixels, of the texture region.
        /// </summary>
        public int Height => Texture.Height;

        /// <summary>
        /// Creates a new texture region using the specified source texture.
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public TextureRegion(Texture texture, int x, int y, int width, int height):
            this(texture, new RectInt(x, y, width, height)) { }

        public void Draw(Batcher batcher, Vector2 position, Color color) => Draw(batcher, position, color, 0f, Vector2.Zero, 1f);

        public void Draw(Batcher batcher, Vector2 position, Color color, float rotation, Vector2 origin, float scale)
        {
            batcher.Image(Texture, SourceRectangle, position, origin, new Vector2(scale), rotation, color);
        }
    }
}
