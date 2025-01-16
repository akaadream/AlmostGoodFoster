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
            entity.
                Register(new SpritesheetAnimation(sprite, new RectInt(0, 0, 16, 19), 4, 0.1f)).
                Register(new Player());
            entity.Transform = new(new Vector2(100), new Vector2(4f), 0f);
            scene.CreateEntity().Register(new Camera(new RectInt(0, 0, Window.Width, Window.Height)));
            scene.CreateEntity().Register(new SceneLogger(GraphicsDevice));

            SceneManager.AddScene(scene);
            base.Startup();
        }
    }
}
