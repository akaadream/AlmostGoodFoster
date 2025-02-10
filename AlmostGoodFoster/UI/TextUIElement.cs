using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI
{
    public abstract class TextUIElement(string text, SpriteFont font, UIElement? parent, UIContainer container) : UIElement(parent, container)
    {
        /// <summary>
        /// The text of the element
        /// </summary>
        public string Text { get; set; } = text;

        /// <summary>
        /// Font used to draw the element
        /// </summary>
        public SpriteFont Font { get; set; } = font;

        /// <summary>
        /// Color of the text
        /// </summary>
        public Color TextColor { get; set; } = Color.White;

        /// <summary>
        /// Color of the text when the mouse hover the element
        /// </summary>
        public Color HoverTextColor { get; set; } = Color.White;
    }
}
