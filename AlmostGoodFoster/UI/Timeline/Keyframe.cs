namespace AlmostGoodFoster.UI.Timeline
{
    public abstract class Keyframe<T>(T value, int frame) where T : struct
    {
        /// <summary>
        /// The value the concerned element must have at the specified frame
        /// </summary>
        public T Value { get; set; } = value;

        /// <summary>
        /// The frame where this keyframe is located
        /// </summary>
        public int Frame { get; set; } = frame;
    }
}
