using System.Numerics;

namespace AlmostGoodFoster.UI.Timeline
{
    public sealed class Vector3Keyframe(Vector3 value, int frame) : Keyframe<Vector3>(value, frame)
    {
    }
}
