using AlmostGoodFoster.Components;
using AlmostGoodFoster.Components.Animations;
using AlmostGoodFoster.Components.Graphics;
using AlmostGoodFoster.EC;
using AlmostGoodFoster.Scenes;
using AlmostGoodFoster.Test.Components;
using Foster.Framework;
using System.Numerics;

namespace AlmostGoodFoster.Test
{
    public class MyGame : AlmostGoodGame
    {

        public MyGame()
        {
        }

        protected override void Startup()
        {
            Scene scene = new("Sample scene");
            Entity entity = scene.CreateEntity();
            Sprite sprite = new(GraphicsDevice, "character.png");
            AnimatedSprite animatedSprite = new();
            animatedSprite.Register("idle", SpritesheetAnimation.FromRange(sprite, 0.12f, 0, 0, 16, 19, 4));
            entity.
                Register(animatedSprite).
                Register(new Player());
            entity.Position = new Vector2(100);
            entity.Scale = new Vector2(4f);
            scene.CreateEntity().Register(new Camera(GraphicsDevice, new RectInt(0, 0, Window.Width, Window.Height)));
            scene.CreateEntity().Register(new SceneLogger(GraphicsDevice));

            SceneManager.AddScene(scene);
            base.Startup();
        }
    }
}
