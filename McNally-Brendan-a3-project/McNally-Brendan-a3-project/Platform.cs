using MohawkGame2D;
using System.Numerics;

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
        Draw.FillColor = Color.Black;
        Draw.Rectangle(Position, Size);
    }

    // Method to initialize platforms for a specific level
    public static Platform[] InitializePlatforms(int level)
    {
        switch (level)
        {
            case 1:
                return new Platform[]
                {
                    new Platform(new Vector2(150, 450), new Vector2(100,50)),
                    new Platform(new Vector2(300, 400), new Vector2(100,50)),
                    new Platform (new Vector2(450,350), new Vector2(100,50)),
                    new Platform (new Vector2(600,300), new Vector2(100,50)),
                };
            case 2:
                return new Platform[]
                {
                    new Platform(new Vector2(200, 500), new Vector2(130, 40)),
                    new Platform(new Vector2(400, 350), new Vector2(100, 50))
                };
            case 3:
                return new Platform[]
                {
                    new Platform(new Vector2(250, 470), new Vector2(120, 45)),
                    new Platform(new Vector2(500, 420), new Vector2(140, 50))
                };
            default:
                return new Platform[0]; // Return an empty array for invalid level
        }
    }
}
