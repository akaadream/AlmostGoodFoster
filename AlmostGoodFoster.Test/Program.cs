using AlmostGoodFoster;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        using var app = new AlmostGoodGame("Almost Good Foster", 1280, 720);
        app.Run();
    }
}