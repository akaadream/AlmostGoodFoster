namespace AlmostGoodFoster.Components
{
    [Flags]
    public enum ActorMasks
    {
        None = 0,
        Solid = 1 << 0,
        JumpThrough = 1 << 1,
        Player = 1 << 2,
        Enemy = 1 << 3
    }
}
