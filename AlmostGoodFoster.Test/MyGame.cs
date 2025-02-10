using AlmostGoodFoster.Scenes;
using AlmostGoodFoster.Test.Scenes;

namespace AlmostGoodFoster.Test
{
    public class MyGame : AlmostGoodGame
    {

        public MyGame()
        {
        }

        protected override void LoadContent()
        {
            base.LoadContent();
            SceneManager.AddScene(new MyScene(GraphicsDevice));
        }

        protected override void Startup()
        {
            base.Startup();
        }
    }
}
