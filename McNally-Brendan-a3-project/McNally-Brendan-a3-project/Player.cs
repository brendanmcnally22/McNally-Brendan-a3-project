using System.Numerics;

namespace MohawkGame2D
{
    public class Player
    {
        // Basic player properties
        public Vector2 Position;
        public Vector2 Velocity;
        public float Width = 50, Height = 50;
        public bool IsJumping = false;
        public int Health = 100;

        // For the trail effect
        private Vector2[] trailPositions;
        private int trailSize = 50;
        private Color playerColor = Color.Blue;

        public Player()
        {
            Position = new Vector2(100, 500);
            Velocity = Vector2.Zero;
            trailPositions = new Vector2[trailSize];
        }

        public void Update()
        {
            // Simple gravity
            Velocity.Y += 1f;
            Position += Velocity;

            // Shift the trail positions
            for (int i = trailSize - 1; i > 0; i--)
            {
                trailPositions[i] = trailPositions[i - 1];
            }
            trailPositions[0] = Position;
        }

        // Called by the main game code to handle input
        public void HandleInput()
        {
            float moveX = Input.GetControllerAxis(0, ControllerAxis.LeftX);
            Velocity.X = moveX * 5;

            // Jump
            if (Input.IsControllerButtonPressed(0, ControllerButton.LeftTrigger1) && !IsJumping)
            {
                Velocity.Y = -20;
                IsJumping = true;
            }
        }

        // Called after collisions are checked so we can reset IsJumping, etc.
        public void LandOnGround(float groundY)
        {
            Position.Y = groundY - Height;
            Velocity.Y = 0;
            IsJumping = false;
        }

        public void DrawPlayer()
        {
            // Draw the player
            Draw.FillColor = Color.Blue;
            Draw.Rectangle(Position, new Vector2(Width, Height));

            // Draw the trail effect
            for (int i = 0; i < trailSize; i++)
            {
                float alpha = 1f - (i / (float)trailSize);
                Draw.FillColor = new ColorF(
                    playerColor.R / 255f,
                    playerColor.G / 255f,
                    playerColor.B / 255f,
                    alpha
                );
                Draw.Square(trailPositions[i], 10 - i * 0.5f);
            }
        }
    }
}
