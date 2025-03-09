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
            Draw.FillColor = Color.Red;
            Draw.Rectangle(Position, Size);
        }

        // Add this static method to initialize hazards for a given level
        public static Hazard[] InitializeHazards(int level)
        {
            switch (level)
            {
                case 1: //Level one 
                    return new Hazard[]
                    {
                        new Hazard(new Vector2(300, 380), new Vector2(50, 20))
                    };
                case 2: //Level two
                    return new Hazard[]
                    {
                        new Hazard(new Vector2(350,470), new Vector2(50,20)),
                        new Hazard(new Vector2(280,350), new Vector2(50,20)),
                        new Hazard(new Vector2(350, 240), new Vector2(50,20))
                    };
                   
                case 3:
                    return new Hazard[]
                    {
                        new Hazard(new Vector2(200,430), new Vector2(50,20)),
                        new Hazard(new Vector2(150,330), new Vector2(50,20)),
                        
                        new Hazard(new Vector2(500,230), new Vector2(50,20)),

                        new Hazard(new Vector2(300,550), new Vector2(500,20))

                    };
                    
                default:
                    return new Hazard[0]; // Returning it so I can stop getting Null issues lol
            }
        }
    }
}