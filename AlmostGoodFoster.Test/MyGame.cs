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
            SceneManager.AddScene(new ClickerScene());
            SceneManager.AddScene(new InteriousScene());

            SceneManager.SetActive(typeof(InteriousScene).Name);
        }

        protected override void Startup()
        {
            //GraphicsDevice.SamplerCount = 8;
            base.Startup();
        }
    }
}
