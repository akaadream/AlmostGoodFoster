using System.Numerics;
using Foster.Framework;

namespace AlmostGoodFoster.UI
{
    public class MainMenu : UIElement
    {
        private int _nextX = 0;

        public MainMenu(UIContainer container): base(null, container)
        {
            _width = container.Width;
            _height = 32;
            BackgroundColor = new Color(31, 31, 31, 255);
        }

        public override void OnResized(int width, int height)
        {
            _width = Container.Width;
        }

        public override void AddChild(UIElement child)
        {
            if (child is MenuItem menuItem)
            {
                menuItem.X = _nextX;
                _nextX += menuItem.Width;
                base.AddChild(menuItem);
            }
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            batcher.Rect(new Rect(GetFinalPosition(), new Vector2(Width, Height)), BackgroundColor);

            base.Render(batcher, deltaTime);
        }
    }
}
