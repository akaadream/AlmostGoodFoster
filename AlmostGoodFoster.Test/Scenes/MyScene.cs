using AlmostGoodFoster.Scenes;
using Foster.Framework;
using AlmostGoodFoster.UI;
using AlmostGoodFoster.Components.Particles;

namespace AlmostGoodFoster.Test.Scenes
{
    internal class MyScene : Scene
    {
        //PathFollow path;

        private SpriteFont _font;

        public MyScene(GraphicsDevice graphicsDevice): base("my_scene")
        {
            //Entity entity = CreateEntity();
            //Sprite sprite = new(GraphicsDevice, "character.png");
            //entity.
            //    Register(new SpritesheetAnimation(sprite, new RectInt(0, 0, 16, 19), 4, 0.1f)).
            //    Register(new Player());
            //entity.Transform = new(new Vector2(100), new Vector2(4f), 0f);
            //CreateEntity().Register(new Camera(new RectInt(0, 0, Window.Width, Window.Height)));
            //CreateEntity().Register(new SceneLogger(GraphicsDevice));

            _font = new(graphicsDevice, new Font("Assets/Fonts/Signika-Bold.ttf"), 14);

            var mainMenu = new MainMenu(UIContainer);
            var menuItem = new MenuItem("File", _font, mainMenu, UIContainer);
            var menuItem2 = new MenuItem("Edit", _font, mainMenu, UIContainer);
            var menuItem3 = new MenuItem("Tools", _font, mainMenu, UIContainer);
            var menuItem4 = new MenuItem("Debug", _font, mainMenu, UIContainer);
            var menuItem5 = new MenuItem("Windows", _font, mainMenu, UIContainer);
            mainMenu.AddChild(menuItem);
            mainMenu.AddChild(menuItem2);
            mainMenu.AddChild(menuItem3);
            mainMenu.AddChild(menuItem4);
            mainMenu.AddChild(menuItem5);

            UIContainer.Elements.Add(mainMenu);


            var texture = new Texture(graphicsDevice, 1, 1);
            var entity = CreateEntity();
            entity.Position = new Point2(100, 100);
            var particlesEmitter = new ParticleEmitter(texture, 20)
            {
                Entity = entity
            };
            AddEntity(entity.Register(particlesEmitter));

            //path.AddPoint(new Vector2(20, 20));
            //path.AddPoint(new Vector2(350, 78));
            //path.AddPoint(new Vector2(343, 234));
            //path.AddPoint(new Vector2(18, 220));

            //Rng rng = new(Guid.NewGuid().Variant);

            //for (int i = 0; i < 8; i++)
            //{
            //    path.AddPoint(new Vector2(rng.Float(20, 1000), rng.Float(20, 800)));
            //}
            //CreateEntity().Register(path);
            //Sprite sprite = new Sprite("worldmap");
        }

        public override void LoadContent()
        {
            base.LoadContent();
        }

        public override void HandleInputs(Input input)
        {
            base.HandleInputs(input);

            //if (input.Keyboard.Pressed(Keys.N))
            //{
            //    path.GoToNextPoint();
            //}
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