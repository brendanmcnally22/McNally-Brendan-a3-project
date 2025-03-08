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
}
