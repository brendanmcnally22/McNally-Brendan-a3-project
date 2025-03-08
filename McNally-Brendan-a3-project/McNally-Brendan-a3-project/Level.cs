using System.Numerics;

namespace MohawkGame2D
{
    public class Level
    {
        public Vector2 ExitPosition { get; private set; }
        public Vector2 ExitSize { get; private set; }
        public Vector2[] Collectibles { get; private set; }
        public Vector2[] Hazards { get; private set; }
        public Vector2[] Platforms { get; private set; }

        public Level(Vector2 exitPosition, Vector2 exitSize, Vector2[] collectibles, Vector2[] hazards, Vector2[] platforms)
        {
            ExitPosition = exitPosition;
            ExitSize = exitSize;
            Collectibles = collectibles;
            Hazards = hazards;
            Platforms = platforms;
        }
    }
}
