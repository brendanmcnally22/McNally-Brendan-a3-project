using System;
using System.Numerics;
namespace MohawkGame2D
{
    public class Collectible
    {
        public Vector2 Position { get; private set; }
        public float Size { get; private set; }

        private Vector2 originalPosition;
        private bool isPickedUp = false;

        private float shakeAmp = 2.0f;
        private float shakeFreq = 5.0f;
        public Collectible(Vector2 position, float size)
        {
            Position = position;
            originalPosition = position;
            Size = size;
        }
       
        public void PickUp()
        {
            isPickedUp = true;
       
        }

        public void Update(float totalTime)
        {
            if (!isPickedUp)
            { 
                float offsetX = (float)(Math.Sin(totalTime * shakeFreq) * shakeAmp);
                float offsetY = (float)(Math.Cos(totalTime * shakeFreq) * shakeAmp);
                Position = originalPosition + new Vector2(offsetX, offsetY);
            }
            else
            {
                // Once picked up, ensure the position stays at its original location.
                Position = originalPosition;
            }
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
                        new Collectible(new Vector2(400,300),20),
                        new Collectible(new Vector2(680,310),20),
                        new Collectible(new Vector2(150,300),20)

                    };
                case 3:
                    return new Collectible[]
                    {
                        new Collectible(new Vector2(50,300),20),   
                        new Collectible(new Vector2(200,250),20),
                        new Collectible(new Vector2(310,250),20)
                    };
            
                default:
                    return new Collectible[0]; // Return an empty array if no valid level is specified.
            }
        }

    }
}
