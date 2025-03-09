using System.Collections;
using System.Numerics;

namespace MohawkGame2D
{
    public class Collectible
    {
        public Vector2 Position { get; private set; }
        public float Size { get; private set; }

        public Collectible(Vector2 position, float size)
        {
            Position = position;
            Size = size;
        }

        public void DrawCollectible()
        {
            Draw.FillColor = Color.Yellow;
            Draw.Circle(Position, Size);
        }

        public static Collectible[] InitializeCollectibles(int level)
        {
            switch (level)
            {
                case 1:
                    return new Collectible[]
                    {
                        new Collectible(new Vector2(400,300),20)
                    };
                case 2:
                    return new Collectible[]
                    {
                        new Collectible(new Vector2(400,300),20)
                        
                        };
            
                default:
                    return new Collectible[0]; // Return an empty array if no valid level is specified.
            }
        }

    }
}
