using System.Numerics;
using System.Reflection;

namespace AlmostGoodFoster
{
    public static class Settings
    {
        #region App

        public static string Title { get; set; } = "Almost Good Game";
        public static string Description { get; set; } = "An super almost good game!";
        public static string Author { get; set; } = "Unknown";
        public static Version Version { get; set; } = Assembly.GetExecutingAssembly().GetName().Version ?? new(1, 0, 0);

        #endregion

        #region Window

        public static int Width { get; set; } = 1280;

        public static int Height { get; set; } = 720;

        #endregion

        #region Custom resolution

        public static bool UseCustomResolution { get; set; } = false;
        public static int ResolutionWidth { get; set; } = 320;
        public static int ResolutionHeight { get; set; } = 180;
        public static int Scale { get; set; } = 1;
        public static int Padding { get; set; } = 0;

        #endregion

        #region Physics 2D

        public static float DefaultGravity { get; set; } = 980f;

        public static Vector2 GravityDirection { get; set; } = Vector2.UnitY;

        #endregion

        #region Tilesets

        public static int TileSize { get; set; } = 16;

        #endregion

    }
}
