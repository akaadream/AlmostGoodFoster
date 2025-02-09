using AlmostGoodFoster.Components.Graphics;
using AlmostGoodFoster.EC;
using Foster.Framework;
using System.Numerics;

namespace AlmostGoodFoster.Components.Animations
{
    public class SpritesheetAnimation(Sprite sprite, float speed) : Component
    {
        /// <summary>
        /// The sprite associated with the animation
        /// </summary>
        public Sprite Sprite { get; set; } = sprite;

        /// <summary>
        /// Frames
        /// </summary>
        public List<RectInt> Frames { get; set; } = [];

        /// <summary>
        /// The frame index
        /// </summary>
        public int FrameIndex { get; private set; } = 0;

        /// <summary>
        /// The speed of the animation
        /// </summary>
        public float Speed { get; set; } = speed;

        /// <summary>
        /// Loop the animation
        /// </summary>
        public bool IsLooping { get; set; } = true;

        // Timer used for the animation
        private float timer = 0f;

        public override void LateUpdate(float deltaTime)
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
            if (IsLooping && FrameIndex > Frames.Count - 1)
            {
                FrameIndex = 0;
            }
        }

        public void RegisterFrame(RectInt rect)
        {
            Frames.Add(rect);
        }

        public void RegisterFramesRange(int startX, int startY, int frameWidth, int frameHeight, int count)
        {
            for (int i = 0; i < count; i++)
            {
                RegisterFrame(new RectInt(startX + frameWidth * i, startY, frameWidth, frameHeight));
            }
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            if (Entity == null)
            {
                Log.Error($"The component (GUID: {Guid}) is not attached to an entity");
                return;
            }

            Render(batcher, Entity.Position, Entity.Scale);
        }

        public void Render(Batcher batcher, Vector2 position, Vector2 scale)
        {
            if (Sprite.Texture == null)
            {
                return;
            }

            batcher.Image(Sprite.Texture, Frames[FrameIndex], position, Vector2.Zero, scale * Sprite.Scale, 0f, Color.White);
        }
    }
}
