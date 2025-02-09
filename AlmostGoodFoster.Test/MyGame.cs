using AlmostGoodFoster.Components;
using AlmostGoodFoster.Components.Animations;
using AlmostGoodFoster.Components.Graphics;
using AlmostGoodFoster.EC;
using AlmostGoodFoster.Scenes;
using AlmostGoodFoster.Test.Components;
using AlmostGoodFoster.Test.Scenes;
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
            SceneManager.AddScene(new MyScene(GraphicsDevice));
            base.Startup();
        }
    }
}
