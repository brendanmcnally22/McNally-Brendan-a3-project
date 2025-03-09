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
        private int trailSize = 20;
        private int drawnTrailCount = 20;
        private Color playerColor = Color.Blue;
        float maxTrailAlpha = 0.5f;

        // Jump cooldown fields
        private float jumpCooldown = 0.5f;      // Delay (in seconds) between jumps
        private float jumpCooldownTimer = 0f;   // Timer that counts down

        public Player()
        {
            Position = new Vector2(100, 500);
            Velocity = Vector2.Zero;
            trailPositions = new Vector2[trailSize];
        }

        public void Update(float deltaTime = 0.016f)
        {
            // Simple gravity
            Velocity.Y += 1f;
            Position += Velocity;

            // Update jump cooldown timer
            if (jumpCooldownTimer > 0)
            {
                jumpCooldownTimer -= deltaTime;
            }

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
            
            // Override horizontal movement if keyboard keys are pressed
            if (Input.IsKeyboardKeyDown(KeyboardInput.A))
            {
                moveX = -1f; // Move left
            }
            else if (Input.IsKeyboardKeyDown(KeyboardInput.D))
            {
                moveX = 1f; // Move right
            }

            //Apply Horizontal Movement

            Velocity.X = moveX * 5;

            if ((Input.IsControllerButtonPressed(0, ControllerButton.LeftTrigger1) || Input.IsKeyboardKeyPressed(KeyboardInput.Space))
                   && !IsJumping && jumpCooldownTimer <= 0)
            {
                Velocity.Y = -20;
                IsJumping = true;
                jumpCooldownTimer = jumpCooldown;  // Reset the cooldown timer
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
            for (int i = 0; i < drawnTrailCount; i++)
            {
                float alpha = maxTrailAlpha * ( 1f - (i / (float)trailSize));

                // Set color to player's color but with decreasing alpha
                Draw.FillColor = new ColorF(
                    playerColor.R / 255f,
                    playerColor.G / 255f,
                    playerColor.B / 255f,
                    alpha
                );

                // Draw a rectangle the same size as the player
                Draw.Rectangle(trailPositions[i], new Vector2(Width, Height));
            }
        }
    }
}
