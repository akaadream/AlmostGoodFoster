using System.Numerics;
using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI
{
    public class Label : TextUIElement
    {
        public Label(string text, SpriteFont font, UIElement? parent, UIContainer container): base(text, font, parent, container)
        {
            AutoSize = true;
            ComputeAutoSize();
        }

        public override void ComputeAutoSize()
        {
            _width = (int)Font.WidthOf(Text);
            _height = (int)Font.HeightOf(Text);
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            base.Render(batcher, deltaTime);

            batcher.Text(Font, Text, new Vector2(X, Y), TextColor * Opacity);
        }
    }
}
