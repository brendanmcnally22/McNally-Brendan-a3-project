using MohawkGame2D;
using System;
using System.Numerics;


public class Platform
{
    public Vector2 Position { get; private set; }
    public Vector2 Size { get; private set; }
    public Vector2 OriginalPosition { get; private set; }

   

    public Platform(Vector2 position, Vector2 size)
    {
        Position = position;
        OriginalPosition = position;
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
            case 1: // Level ONE! 
                return new Platform[]
                {
                    new Platform(new Vector2(100, 450), new Vector2(100,50)),
                    new Platform(new Vector2(250, 400), new Vector2(100,50)),
                    new Platform (new Vector2(400,350), new Vector2(100,50)),
                    new Platform (new Vector2(550,300), new Vector2(100,50)),
                };
            case 2: // LEVEL TWO! 
                return new Platform[]
                {
                    new Platform(new Vector2(300, 480), new Vector2(100, 50)),
                    new Platform(new Vector2(300, 350), new Vector2(100, 50)),
                    new Platform(new Vector2(100,350), new Vector2(100, 50)),
                    new Platform(new Vector2(500,250), new Vector2(100, 50)),
                    new Platform(new Vector2(300,100), new Vector2(100, 50)),
                    new Platform(new Vector2(650,350), new Vector2(100,50))
                };
            case 3: // LEVEL THREE!!!!!!!!
                Platform[] platforms = new Platform[]
                {
                    new Platform(new Vector2(150, 450), new Vector2(100, 50)),
                    new Platform(new Vector2(150, 350), new Vector2(100, 50)),
                    new Platform(new Vector2(300, 300), new Vector2(100, 50)),
                    new Platform(new Vector2(500, 250), new Vector2(50, 50)),
                    new Platform(new Vector2(600, 200), new Vector2(100, 50)),
                   
                };
             
                return platforms;
            default:
                return new Platform[0]; // Return an empty array for invalid level
        }
    }
}
