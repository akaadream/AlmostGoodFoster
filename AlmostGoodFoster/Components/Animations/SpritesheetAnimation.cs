using AlmostGoodFoster.Components.Graphics;
using AlmostGoodFoster.EC;
using Foster.Framework;
using System.Numerics;

namespace AlmostGoodFoster.Components.Animations
{
    public class SpritesheetAnimation(Sprite sprite, RectInt cell, int frames, float speed) : Component
    {
        /// <summary>
        /// The sprite associated with the animation
        /// </summary>
        public Sprite Sprite { get; set; } = sprite;

        public RectInt Cell { get; set; } = cell;

        public int Frames { get; set; } = frames;

        public int FrameIndex { get; set; } = 0;

        public float Speed { get; set; } = speed;

        private float timer = 0f;

        public override void LateUpadte(float deltaTime)
        {
            timer += deltaTime;
            if (timer >= Speed)
            {
                NextFrame();
                timer -= Speed;
            }
        }

        private void NextFrame()
        {
            FrameIndex++;
            if (FrameIndex > Frames - 1)
            {
                FrameIndex = 0;
            }
        }

        private RectInt CurrentFrame()
        {
            int textureWidth = Sprite.Texture.Width;
            int textureHeight = Sprite.Texture.Height;

            int frameX = Cell.Width * FrameIndex + Cell.X;
            int frameY = Cell.Height * FrameIndex + Cell.Y;

            return new(frameX, frameY, Cell.Width, Cell.Height);
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            if (Entity == null)
            {
                Log.Error($"The component (GUID: {Guid}) is not attached to an entity");
                return;
            }
            batcher.Image(Sprite.Texture, CurrentFrame(), Entity.Transform.Position, Vector2.Zero, Entity.Transform.Scale * Sprite.Scale, 0f, Color.White);
        }
    }
}
