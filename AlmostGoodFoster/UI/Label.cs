using System.Numerics;
using AlmostGoodFoster.Fonts;
using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI
{
    public class Label : UIElement
    {
        public Label(string text, UIElement? parent, UIContainer container): base(parent, container)
        {
            Text = text;
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
