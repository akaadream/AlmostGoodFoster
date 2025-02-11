using System.Numerics;
using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI.Menu.MainMenu
{
    public class MenuItem : UIElement
    {
        public MenuItem(string text, MainMenu mainMenu, UIContainer container) : base(mainMenu, container)
        {
            Text = text;
            Padding = 10;
            float fontWidth = Font.WidthOf(text);
            float fontHeight = Font.HeightOf(text);
            _width = (int)(Font.WidthOf(text) + Padding * 2);
            _height = Math.Max(Math.Clamp((int)(Font.HeightOf(text) + Padding * 2), 0, mainMenu.Height), mainMenu.Height);

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
