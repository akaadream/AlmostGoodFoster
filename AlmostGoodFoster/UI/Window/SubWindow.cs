using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using AlmostGoodFoster.Fonts;
using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI.Window
{
    public class SubWindow : UIElement
    {
        public Color WindowBarColor { get; set; } = new(0x151515);
        public Color HoveredWindowBarColor { get; set; } = new(0x000000);
        public Color FocusedWindowBorderColor { get; set; } = new(47, 92, 204, 255);

        public int WindowBarHeight { get; set; } = 32;

        private bool _isTitleBarHovered = false;
        private bool _isTitleBarClicked = false;

        private bool _isDragging = false;
        private bool _wasDragging = false;
        private int _startDragX = 0;
        private int _startDragY = 0;

        public bool IsFocused { get; set; } = false;

        public SubWindow(int width, int height, UIContainer container) : base(null, container)
        {
            Width = width;
            Height = height;

            BackgroundColor = new(0x242424);
            Radius = 6f;
        }

        public override void HandleInputs(Input input)
        {
            base.HandleInputs(input);

            if (!IsFocused && Container.FocusedWindow != null)
            {
                return;
            }

            _isTitleBarHovered = IsHovered && input.Mouse.Y > Top && input.Mouse.Y < Top + WindowBarHeight;
            _isTitleBarClicked = _isTitleBarHovered && input.Mouse.LeftPressed;
            _isDragging = (_isTitleBarHovered || _isDragging) && input.Mouse.LeftDown;

            if (_isDragging)
            {
                if (!_wasDragging)
                {
                    // Start drag
                    _startDragX = (int)(input.Mouse.X - Left);
                    _startDragY = (int)(input.Mouse.Y - Top);
                }
                else
                {
                    // Was already dragging
                    Left = (int)(input.Mouse.X - _startDragX);
                    Top = (int)(input.Mouse.Y - _startDragY);
                }
            }

            _wasDragging = _isDragging;
        }

        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            base.Render(batcher, deltaTime);

            string closeText = "X";
            string reduceText = "__";
            Vector2 closeTextSize = Font.SizeOf(closeText);
            Vector2 reduceTextSize = Font.SizeOf(reduceText);

            // Background
            if (IsFocused)
            {
                batcher.RectRoundedLine(new Rect(X - 5, Y - 5, Width + 10, Height + 10), Radius + 5, 3f, FocusedWindowBorderColor);
            }
            batcher.RectRounded(new Rect(X, Y, Width, Height), 6f, BackgroundColor * 0.8f);

            // Title bar
            batcher.RectRounded(X, Y, Width, 32, Radius, Radius, 0, 0, _isTitleBarHovered ? HoveredWindowBarColor : WindowBarColor);

            // Buttons
            batcher.RectRounded(X + Width - 8 - 22, Y + 32 / 2 - 22 / 2, 22, 22, Radius, BackgroundColor);
            batcher.Text(Font, closeText, new Vector2(X + Width - 8 - 22 / 2 - closeTextSize.X / 2, Y + 32 / 2 - closeTextSize.Y / 2), Color.Gray);

            batcher.RectRounded(X + Width - 16 - 22 - 22, Y + 32 / 2 - 22 / 2, 22, 22, Radius, BackgroundColor);
            batcher.Text(Font, reduceText, new Vector2(X + Width - 16 - 22 - 22 / 2 - reduceTextSize.X / 2, Y + 32 / 2 - reduceTextSize.Y / 2), Color.Gray);

            string title = "Title of the window";
            Vector2 titleSize = Font.SizeOf(title);

            batcher.Text(Font, title, new Vector2(X + 8, Y + 32 / 2 - titleSize.Y / 2), Color.Gray);
        }
    }
}
