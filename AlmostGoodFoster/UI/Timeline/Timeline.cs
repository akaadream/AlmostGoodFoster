using System.Numerics;
using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI.Timeline
{
    public class Timeline : UIElement
    {
        SpriteFont _font;

        public int MetricLineHeight { get; set; } = 8;
        public Color LineColor = Color.DarkGray;
        public Color ResizeLineColor = new(120, 120, 120, 255);
        public Color PrimaryColor = new(47, 92, 204, 255);
        public Color OddItemColor = new(60, 60, 60, 255);

        public int SidebarWidth { get; set; } = 300;
        public int ItemHeight { get; set; } = 26;

        private int MagnetizationRange = 6;

        public bool ResizeBorderHovered { get; set; } = false;

        public Timeline(SpriteFont font, UIContainer container) : base(null, container)
        {
            _font = font;
            BackgroundColor = new Color(32, 32, 32, 255);

            _width = container.Width;
            _height = 200;

            Padding = 10;

            Anchor = Anchor.BottomLeft;
        }

        public override void OnResized(int width, int height)
        {
            _width = Container.Width;
            ComputePosition();
        }

        public override void HandleInputs(Input input)
        {
            base.HandleInputs(input);
            ResizeBorderHovered = input.Mouse.Y >= Y - MagnetizationRange && input.Mouse.Y <= Y + MagnetizationRange;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            base.Render(batcher, deltaTime);

            // Background
            batcher.Rect(new Rect(X, Y, Width, Height), BackgroundColor);
            batcher.Line(new Vector2(X, Y), new Vector2(X + Width, Y), 2f, ResizeBorderHovered ? PrimaryColor : ResizeLineColor);

            // Items
            for (int i = 0; i < 4; i++)
            {
                int rectY = 2 + Y + Padding + 20 + MetricLineHeight + ItemHeight * i;
                batcher.RectRounded(Padding, rectY, Width - Padding * 2, ItemHeight, 6, 6, 6, 6, i % 2 == 0 ? Color.White * 0.05f : Color.Transparent);

                string title = "";
                // Draw text
                switch (i)
                {
                    case 0:
                        title = "Position";
                        break;
                    case 1:
                        title = "Rotation";
                        break;
                    case 2:
                        title = "Scale";
                        break;
                }
                Vector2 titleSize = _font.SizeOf(title);

                batcher.Text(_font, title, new Vector2(Padding * 2, rectY + ItemHeight / 2 - (int)titleSize.Y / 2), Color.White * 0.5f);
            }

            // Metric
            for (int i = 0; i <= 100; i++)
            {
                int x = (Width - SidebarWidth - Padding) / 100 * i;

                // Text
                string text = $"{i}";
                int textWidth = (int)_font.WidthOf(text);
                int textHeigh = (int)_font.HeightOf(text);

                if (i % 5 == 0)
                {
                    // Big vertical line
                    batcher.Text(_font, text, new Vector2(x + SidebarWidth - textWidth / 2, Y + Padding), LineColor);
                    batcher.PushBlend(BlendMode.Add);
                    batcher.Line(new Vector2(x + SidebarWidth, Y + Padding + textHeigh + 5), new Vector2(x + SidebarWidth, Y + Height - Padding), 1f, LineColor * 0.75f);
                    batcher.PopBlend();
                }
                else
                {
                    batcher.PushBlend(BlendMode.Add);
                    // Little vertical line
                    batcher.Line(new Vector2(x + SidebarWidth, Y + Padding + 20), new Vector2(x + SidebarWidth, Y + Padding + 20 + MetricLineHeight), 1f, LineColor * 0.5f);
                    batcher.PopBlend();
                }

                if (i == 12)
                {
                    // Current time
                    int currentX = (Width - SidebarWidth - Padding) / 100 * 12;

                    batcher.Line(
                        new Vector2(currentX + SidebarWidth, Y + Padding + textHeigh + 5),
                        new Vector2(currentX + SidebarWidth, Y + Height - Padding),
                        2f,
                        PrimaryColor);

                    batcher.Triangle(
                        new Vector2(currentX - 5 + SidebarWidth, Y + Padding + textHeigh + 5 - 7),
                        new Vector2(currentX + 5 + SidebarWidth, Y + Padding + textHeigh + 5 - 7),
                        new Vector2(currentX + SidebarWidth, Y + Padding + textHeigh + 5),
                        PrimaryColor);
                }
            }

            // Horizontal separator
            int separatorY = Y + Padding + 20 + MetricLineHeight;
            batcher.Line(new Vector2(X + SidebarWidth, separatorY), new Vector2(X + Width - Padding, separatorY), 1f, LineColor * 0.5f);

            // Items keys
            for (int i = 0; i < 4; i++)
            {
                if (i == 1)
                {
                    int currentX = (Width - SidebarWidth - Padding) / 100 * 25;
                    int rectY = 2 + Y + Padding + 20 + MetricLineHeight + ItemHeight * i;
                    batcher.Circle(new Vector2(currentX + SidebarWidth, rectY + ItemHeight / 2), 4f, 10, new Color(218, 195, 38, 255));
                }
            }

            batcher.RectRounded(new Rect(X + Padding, Y + Padding, 50, 24), 6f, Color.DarkGray);
            batcher.RectRounded(new Rect(X + Padding * 1.5f + 50, Y + Padding, 50, 24), 6f, Color.DarkGray);
            batcher.RectRounded(new Rect(X + Padding * 2 + 100, Y + Padding, 50, 24), 6f, Color.DarkGray);
        }
    }
}
