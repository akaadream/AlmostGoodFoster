using System.Numerics;
using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI.Menu.MainMenu
{
    public class MenuItem : TextUIElement
    {
        public MenuItem(string text, SpriteFont font, MainMenu mainMenu, UIContainer container) : base(text, font, mainMenu, container)
        {
            Padding = 10;
            float fontWidth = font.WidthOf(text);
            float fontHeight = font.HeightOf(text);
            _width = (int)(font.WidthOf(text) + Padding * 2);
            _height = Math.Max(Math.Clamp((int)(font.HeightOf(text) + Padding * 2), 0, mainMenu.Height), mainMenu.Height);

            TextColor = new Color(160, 160, 160, 255);
            HoverBackgroundColor = Color.DarkGray;
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            float fontWidth = Font.WidthOf(Text);
            float fontHeight = Font.HeightOf(Text);
            int textX = (int)(X + _width / 2 - fontWidth / 2);
            int textY = (int)(Y + _height / 2 - fontHeight / 2);

            batcher.Rect(new Rect(X, Y, Width, Height), IsHovered ? HoverBackgroundColor : BackgroundColor);
            batcher.Text(Font, Text, new Vector2(textX, textY), IsHovered ? HoverTextColor : TextColor);
        }
    }
}
