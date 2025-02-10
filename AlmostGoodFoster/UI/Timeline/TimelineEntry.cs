using AlmostGoodFoster.UI.Containers;
using Foster.Framework;

namespace AlmostGoodFoster.UI.Timeline
{
    public class TimelineEntry<T> : UIElement where T : struct
    {
        public string Title { get; set; }
        public SpriteFont Font { get; set; }

        public Keyframe<T> Keyframe { get; set; }

        public TimelineEntry(string title, SpriteFont font, Keyframe<T> keyframe, Timeline timeline, UIContainer container) : base(timeline, container)
        {
            Title = title;
            Font = font;
            Keyframe = keyframe;
        }
    }
}
