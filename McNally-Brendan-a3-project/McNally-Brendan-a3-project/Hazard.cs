using System.Numerics;

namespace MohawkGame2D
{
    public class Hazard
    {
        public Vector2 Position { get; private set; }
        public Vector2 Size { get; private set; }

        public Hazard(Vector2 position, Vector2 size)
        {
            Position = position;
            Size = size;
        }

        public void DrawHazard()
        {
            Draw.FillColor = Color.Magenta;
            Draw.Rectangle(Position, Size);
        }
    }
}
