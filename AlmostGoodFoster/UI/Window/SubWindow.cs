using System;
using System.Collections.Generic;
using System.Numerics;
using AlmostGoodFoster.Fonts;
using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI.Window
{
    public class SubWindow : UIElement
    {
        public Color WindowBarColor { get; set; } = new(0x151515);
        public int WindowBarHeight { get; set; } = 32;

        private SpriteFont _font;

        public SubWindow(int width, int height, UIContainer container) : base(null, container)
        {
            Width = width;
            Height = height;

            BackgroundColor = new(0x242424);
            _font = FontManager.Get("default", 12);
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            base.Render(batcher, deltaTime);

            string closeText = "X";
            string reduceText = "__";
            Vector2 closeTextSize = _font.SizeOf(closeText);
            Vector2 reduceTextSize = _font.SizeOf(reduceText);

            // Background
            batcher.RectRounded(new Rect(X, Y, Width, Height), 6f, BackgroundColor);

            // Title bar
            batcher.RectRounded(X, Y, Width, 32, 6f, 6f, 0, 0, WindowBarColor);

            // Buttons
            batcher.RectRounded(X + Width - 8 - 22, Y + 32 / 2 - 22 / 2, 22, 22, 6f, BackgroundColor);
            batcher.Text(_font, closeText, new Vector2(X + Width - 8 - 22 / 2 - closeTextSize.X / 2, Y + 32 / 2 - closeTextSize.Y / 2), Color.Gray);

            batcher.RectRounded(X + Width - 16 - 22 - 22, Y + 32 / 2 - 22 / 2, 22, 22, 6f, BackgroundColor);
            batcher.Text(_font, reduceText, new Vector2(X + Width - 16 - 22 - 22 / 2 - reduceTextSize.X / 2, Y + 32 / 2 - reduceTextSize.Y / 2), Color.Gray);

            string title = "Title of the window";
            Vector2 titleSize = _font.SizeOf(title);

            batcher.Text(_font, title, new Vector2(X + 8, Y + 32 / 2 - titleSize.Y / 2), Color.Gray);
        }
    }
}
