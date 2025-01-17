using AlmostGoodFoster.EC;
using Foster.Framework;

namespace AlmostGoodFoster.Components.Collisions
{
    public class Area(RectInt bounds) : Component
    {
        public RectInt Bounds { get; set; } = bounds;

        public Action<Area>? OnAreaEnter { get; set; }
        public Action<Area>? OnAreaExit { get; set; }

        private List<Area> _overlappingAreas = [];

        public override void FixedUpdate(float fixedDeltaTime)
        {
            if (Entity != null)
            {
                var areas = Entity.FindAll(this);
                foreach (var area in areas)
                {
                    if (area == null)
                    {
                        continue;
                    }

                    if (area.Bounds.Overlaps(Bounds) && !_overlappingAreas.Contains(area))
                    {
                        _overlappingAreas.Add(area);
                        OnAreaEnter?.Invoke(area);
                    }
                    else if (_overlappingAreas.Contains(area))
                    {
                        _overlappingAreas.Remove(area);
                        OnAreaExit?.Invoke(area);
                    }
                }
            }
        }
    }
}
