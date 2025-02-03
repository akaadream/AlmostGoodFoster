using Foster.Framework;

namespace AlmostGoodFoster.Components.Animations
{
    public class AsepriteAnimation
    {
        public Aseprite Aseprite { get; set; }
        public AsepriteAnimation(string filename)
        {
            Aseprite = new(filename);
        }
    }
}
