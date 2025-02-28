using System;
using System.Numerics;


namespace MohawkGame2D;



    public class Borders
    {
        // Positions and sizes for each border
        private Vector2 topBorderPos = new Vector2(0, 0);
        private Vector2 topBorderSize = new Vector2(800, 30);

        private Vector2 bottomBorderPos = new Vector2(0, 570);
        private Vector2 bottomBorderSize = new Vector2(800, 30);

        private Vector2 leftBorderPos = new Vector2(0, 0);
        private Vector2 leftBorderSize = new Vector2(30, 600);

        private Vector2 rightBorderPos = new Vector2(770, 0);
        private Vector2 rightBorderSize = new Vector2(30, 600);

        // Draw all four borders
        public void DrawBorders()
        {
            Draw.FillColor = Color.Black;

            Draw.Rectangle(topBorderPos, topBorderSize);
            Draw.Rectangle(bottomBorderPos, bottomBorderSize);
            Draw.Rectangle(leftBorderPos, leftBorderSize);
            Draw.Rectangle(rightBorderPos, rightBorderSize);
        }
    }

