using AlmostGoodFoster.Components.Animations;
using AlmostGoodFoster.Components.Graphics;
using AlmostGoodFoster.Components;
using AlmostGoodFoster.EC;
using AlmostGoodFoster.Scenes;
using AlmostGoodFoster.Test.Components;
using Foster.Framework;
using System.Numerics;

namespace AlmostGoodFoster.Test.Scenes
{
    internal class MyScene : Scene
    {
        PathFollow path;

        public MyScene(): base("my_scene")
        {
            //Entity entity = CreateEntity();
            //Sprite sprite = new(GraphicsDevice, "character.png");
            //entity.
            //    Register(new SpritesheetAnimation(sprite, new RectInt(0, 0, 16, 19), 4, 0.1f)).
            //    Register(new Player());
            //entity.Transform = new(new Vector2(100), new Vector2(4f), 0f);
            //CreateEntity().Register(new Camera(new RectInt(0, 0, Window.Width, Window.Height)));
            //CreateEntity().Register(new SceneLogger(GraphicsDevice));


            path = new()
            {
                Value = 0.0f,
                Loop = true
            };
            //path.AddPoint(new Vector2(20, 20));
            //path.AddPoint(new Vector2(350, 78));
            //path.AddPoint(new Vector2(343, 234));
            //path.AddPoint(new Vector2(18, 220));

            Rng rng = new(Guid.NewGuid().Variant);

            for (int i = 0; i < 8; i++)
            {
                path.AddPoint(new Vector2(rng.Float(20, 1000), rng.Float(20, 800)));
            }
            CreateEntity().Register(path);
            //Sprite sprite = new Sprite("worldmap");
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void HandleInputs(Input input)
        {
            base.HandleInputs(input);

            if (input.Keyboard.Pressed(Keys.N))
            {
                path.GoToNextPoint();
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            base.Render(batcher, deltaTime);

            // Test GUI
            
        }
    }
}