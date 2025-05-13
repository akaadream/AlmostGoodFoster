namespace AlmostGoodFoster.Utils
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CommandAttribute(string name, string description) : Attribute
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
    }
}
