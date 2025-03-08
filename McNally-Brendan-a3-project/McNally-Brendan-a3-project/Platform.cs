using System.Numerics;

namespace MohawkGame2D;

public class Platform
{
    public Vector2 Position { get; private set; }
    public Vector2 Size { get; private set; }

    public Platform(Vector2 position, Vector2 size)
    {
        Position = position;
        Size = size;
    }

    public void DrawPlatform()
    {
        Draw.FillColor = Color.DarkGray;
        Draw.Rectangle(Position, Size);
    }

    public static Platform[] Level1Platforms; //Level 1 platforms WOO! 
    public static Platform[] Level2Platforms; //Level 2 platforms WOO! 
    public static Platform[] Level3Platforms; // Level 3 Platform WOO!

    public static void InitializePlatform() // Initialize the platforms positions 
    {
        Level1Platforms = new Platform[]
            {
            new Platform(new Vector2(150,450), new Vector2(100,50)),
            new Platform(new Vector2(300,400), new Vector2(120, 50))
            };

        Level2Platforms = new Platform[]
        {
            new Platform(new Vector2(200,500), new Vector2(130,40)),
            new Platform(new Vector2(400,350), new Vector2(100,50))
        };
        Level3Platforms = new Platform[]
        {
            new Platform(new Vector2(250,470), new Vector2(120,45)),
            new Platform(new Vector2(500,420), new Vector2(140,50))
        };

    }
}
