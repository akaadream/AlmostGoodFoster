using AlmostGoodFoster.EC;
using Foster.Framework;

namespace AlmostGoodFoster.Test.Components
{
    public class SceneLogger : Component
    {
        public SpriteFont Font { get; set; }
        public SceneLogger(GraphicsDevice graphicsDevice)
        {
            Font = new(graphicsDevice, new Font("Assets/Fonts/Signika-Bold.ttf"), 24);
        }

        public override void DrawGUI(Batcher batcher, float deltaTime)
        {
            if (Entity == null)
            {
                return;
            }

            batcher.Text(Font, Entity.Scene.Name, new System.Numerics.Vector2(10, 10), Color.White * 0.6f);
        }
    }
}
