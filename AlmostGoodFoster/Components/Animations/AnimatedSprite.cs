using AlmostGoodFoster.EC;
using Foster.Framework;

namespace AlmostGoodFoster.Components.Animations
{
    public class AnimatedSprite() : Component
    {
        public Dictionary<string, SpritesheetAnimation> Animations { get; private set; } = [];
        private string _current = "";

        public override void Start()
        {
            Play(_current);
        }

        public void Play(string animationName)
        {
            if (Animations.TryGetValue(@animationName, out var _))
            {
                _current = animationName;
            }
        }

        public void Register(string animationName, SpritesheetAnimation animation)
        {
            if (Animations.TryGetValue(@animationName, out var _))
            {
                return;
            }

            Animations.Add(animationName, animation);
            if (!Animations.TryGetValue(_current, out var _))
            {
                _current = animationName;
            }
        }

        public override void LateUpdate(float deltaTime)
        {
            if (Animations.TryGetValue(_current, out var animation))
            {
                animation?.LateUpdate(deltaTime);
            }
        }

        public override void Render(Batcher batcher, float deltaTime)
        {
            if (Entity == null)
            {
                return;
            }

            if (Animations.TryGetValue(_current, out var animation))
            {
                animation?.Render(batcher, Entity.Position, Entity.Scale);
            }
        }
    }
}
