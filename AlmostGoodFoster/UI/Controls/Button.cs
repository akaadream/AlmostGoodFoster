using System.Numerics;
using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI.Controls
{
    public class Button : UIElement
    {
        private Texture? _texture;
        private int _textureScale = 2;
        
        public Button(string text, UIElement? parent, UIContainer container): base(parent, container)
        {
            Text = text;
            BackgroundColor = Color.DarkGray;
            HoverBackgroundColor = Color.Black;
            Radius = 6f;
            Padding = 8;
            AutoSize = true;
        }

        public Button(Texture texture, UIElement? parent, UIContainer container): this(string.Empty, parent, container)
        {
            _texture = texture;
            ComputeAutoSize();
        }

        public override void ComputeAutoSize()
        {
            if (_texture != null)
            {
                _width = _texture.Width * _textureScale + Padding * 2;
                _height = _texture.Height * _textureScale + Padding * 2;
            }
            else
            {
                _width = (int)Font.WidthOf(Text) + Padding * 2;
                _height = (int)Font.HeightOf(Text) + Padding * 2;
            }
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            base.Render(batcher, deltaTime);

            // Background
            if (Radius <= 0)
            {
                batcher.Rect(X, Y, Width, Height, IsHovered ? HoverBackgroundColor : BackgroundColor);
            }
            else
            {
                batcher.RectRounded(X, Y, Width, Height, Radius, IsHovered ? HoverBackgroundColor : BackgroundColor);
            }

            if (Font == null)
            {
                return;
            }

            if (_texture != null)
            {
                batcher.Image(_texture, new Vector2(X + Width / 2 - _texture.Width * _textureScale / 2, Y + Height / 2 - _texture.Height * _textureScale / 2), Vector2.Zero, new Vector2(_textureScale), 0f, TextColor);
            }
            else
            {
                // Draw text
                Vector2 textSize = Font.SizeOf(Text);
                batcher.Text(Font, Text, new Vector2(X + Width / 2 - textSize.X / 2, Y + Height / 2 - textSize.Y / 2), TextColor);
            }
        }
    }
}
