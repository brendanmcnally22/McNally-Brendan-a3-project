
using System.Numerics;


namespace MohawkGame2D;

public class Platform
{
    public Vector2 Position { get; set; }
    public Vector2 Size { get; set; }

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
    

}
