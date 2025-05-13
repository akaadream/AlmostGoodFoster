using AlmostGoodFoster.Scenes;
using Foster.Framework;
using AlmostGoodFoster.Components.Particles;
using AlmostGoodFoster.UI.Menu.MainMenu;
using AlmostGoodFoster.UI.Timeline;
using AlmostGoodFoster.UI.Window;
using AlmostGoodFoster.UI.Controls;

namespace AlmostGoodFoster.Test.Scenes
{
    internal class MyScene : Scene
    {
        //PathFollow path;

        public MyScene(GraphicsDevice graphicsDevice)
        {
            //Entity entity = CreateEntity();
            //Sprite sprite = new(GraphicsDevice, "character.png");
            //entity.
            //    Register(new SpritesheetAnimation(sprite, new RectInt(0, 0, 16, 19), 4, 0.1f)).
            //    Register(new Player());
            //entity.Transform = new(new Vector2(100), new Vector2(4f), 0f);
            //CreateEntity().Register(new Camera(new RectInt(0, 0, Window.Width, Window.Height)));
            //CreateEntity().Register(new SceneLogger(GraphicsDevice));

            var mainMenu = new MainMenu(UIContainer);
            var menuItem = new MenuItem("File", mainMenu, UIContainer);
            var menuItem2 = new MenuItem("Edit", mainMenu, UIContainer);
            var menuItem3 = new MenuItem("Tools", mainMenu, UIContainer);
            var menuItem4 = new MenuItem("Debug", mainMenu, UIContainer);
            var menuItem5 = new MenuItem("Windows", mainMenu, UIContainer);
            mainMenu.AddChild(menuItem);
            mainMenu.AddChild(menuItem2);
            mainMenu.AddChild(menuItem3);
            mainMenu.AddChild(menuItem4);
            mainMenu.AddChild(menuItem5);

            UIContainer.AddChild(mainMenu);
            UIContainer.AddChild(new Timeline(UIContainer));

            var window = new SubWindow(800, 400, UIContainer)
            {
                Left = 150,
                Top = 80
            };

            var window2 = new SubWindow(500, 300, UIContainer)
            {
                Left = 300,
                Top = 100
            };

            var window3 = new SubWindow(500, 300, UIContainer)
            {
                Left = 450,
                Top = 80
            };

            Texture eyeTexture = new(graphicsDevice, new Image("Assets/Icons/opened_eye.png"));
            var button = new Button(eyeTexture, null, UIContainer)
            {
                Left = 10,
                Top = 42
            };

            UIContainer.AddChild(button);
            UIContainer.AddChild(window);
            UIContainer.AddChild(window2);
            UIContainer.AddChild(window3);

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