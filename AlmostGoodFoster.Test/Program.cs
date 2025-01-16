using AlmostGoodFoster;
using AlmostGoodFoster.Test;

public class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        using var app = new MyGame();
        app.Run();
    }
}