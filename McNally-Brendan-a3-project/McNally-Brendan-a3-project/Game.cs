﻿using System.Collections;
using System.Numerics;

namespace MohawkGame2D
{
    public class Game
    {
        enum gamestate
        {
            Menu,
            Level1,
            Level2,
            Level3,
            Gameover
        }

        // States
        private gamestate currentState = gamestate.Menu; // start the game in the main menu

        // Managers / Helpers
        private MusicManager musicManager;
        private Borders borders;
        private Player player;
       
        // Some game variables
        private bool hasCollected = false;
        private int score = 0;

        // Collectible
        private Vector2 collectiblePosition = new Vector2(400, 300);
        private float collectibleSize = 20;

        // Hazard
        private Vector2 hazardPosition = new Vector2(500, 500);
        private Vector2 hazardSize = new Vector2(50, 50);

        // Exit
        private Vector2 exitPosition = new Vector2(720, 420);
        private Vector2 exitSize = new Vector2(40, 40);

        // Platform
        private Vector2 platformPosition = new Vector2(150, 450);
        private Vector2 platformSize = new Vector2(100, 50);

        private LevelData levelData;
        public void Setup()
        {
            // Window settings
            Window.SetSize(800, 600);
            Window.SetTitle("Level One Test");

            // Initialize music
            string[] songPaths = {
                "C:\\Users\\brend\\Downloads\\Come as you Are.mp3",
                "C:\\Users\\brend\\Downloads\\The Moment.mp3",
                "C:\\Users\\brend\\Downloads\\Real Gone.mp3"
            };
            string[] songTitles = { "Come as You Are", "The Moment", "Real Gone" };

            musicManager = new MusicManager(songPaths, songTitles);
            musicManager.PlayMusic(0);

            // Create other helpers
            borders = new Borders();
            player = new Player();
            levelData = new LevelData();
        }

        public void Update(float deltaTime = 0.016f)
        {
            Window.ClearBackground(Color.Gray);

            switch (currentState)
            {
                case gamestate.Menu:
                    DrawMenu();
                    HandleMenuInput();
                    break;

                case gamestate.Level1:
                    // Update logic
                    player.HandleInput();
                    player.Update();

                    // Collisions
                    ApplyCollisions();

                    // Drawing
                    borders.DrawBorders();
                    DrawPlatform();
                    player.DrawPlayer();
                    DrawCollectible();
                    DrawHazard();
                    DrawExit();
                    DrawUI();

                    break;

                case gamestate.Gameover:
                    DrawGameOverScreen();
                    HandleGameOverInput();
                    break;
            }
        }

        //--- MENU & GAME OVER ---//

        private void DrawMenu()
        {
            Draw.FillColor = Color.White;
            Text.Draw("Welcome", 200, 200);
            Text.Draw("Press X to Start!", 200, 300);
        }

        private void HandleMenuInput()
        {
            if (Input.IsControllerButtonPressed(0, ControllerButton.RightFaceLeft))
            {
                currentState = gamestate.Level1;
            }
        }

        private void DrawGameOverScreen()
        {
            Draw.FillColor = Color.Red;
            Text.Color = Color.White;
            Text.Size = 20;
            Text.Draw("GAME OVER!", 300, 200);
            Text.Draw("Press X to Restart", 250, 300);
        }

        private void HandleGameOverInput()
        {
            if (Input.IsControllerButtonPressed(0, ControllerButton.RightFaceLeft))
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            player.Health = 100;
            player.Position = new Vector2(100, 500);
            currentState = gamestate.Level1;
        }

        //--- COLLISIONS ---//

        private void ApplyCollisions()
        {
            // Ground collision
            if (player.Position.Y + player.Height > 570)
            {
                player.LandOnGround(570);
            }

            // Left border
            if (player.Position.X < 30)
                player.Position = new Vector2(30, player.Position.Y);

            // Right border
            if (player.Position.X + player.Width > 770)
                player.Position = new Vector2(770 - player.Width, player.Position.Y);

            // Top border
            if (player.Position.Y < 30)
                player.Position = new Vector2(player.Position.X, 30);

            // Platform collision
            CheckPlatformCollision();

            // Hazard collision
            if (Vector2.Distance(player.Position, hazardPosition) < 30)
            {
                player.Health -= 10;
                if (player.Health <= 0)
                {
                    player.Health = 0;
                    currentState = gamestate.Gameover;
                }
            }

            // Collectible collision
            if (!hasCollected && Vector2.Distance(player.Position, collectiblePosition) < 30)
            {
                hasCollected = true;
                score++;
                player.Health += 50;
            }

            // Exit collision
            if (Vector2.Distance(player.Position, exitPosition) < 30)
            {
                currentState = gamestate.Gameover;
            }
        }

        private void CheckPlatformCollision()
        {
            // Player edges
            float playerBottom = player.Position.Y + player.Height;
            float playerTop = player.Position.Y;
            float playerLeft = player.Position.X;
            float playerRight = player.Position.X + player.Width;

            // Platform edges
            float platTop = platformPosition.Y;
            float platBottom = platformPosition.Y + platformSize.Y;
            float platLeft = platformPosition.X;
            float platRight = platformPosition.X + platformSize.X;

            bool isFallingOntoPlatform =
                playerBottom >= platTop &&
                playerTop < platTop &&
                player.Velocity.Y > 0;

            bool isWithinPlatformWidth =
                playerRight > platLeft &&
                playerLeft < platRight;

            if (isFallingOntoPlatform && isWithinPlatformWidth)
            {
                player.Position = new Vector2(player.Position.X, platTop - player.Height);
                player.Velocity = new Vector2(player.Velocity.X, 0);
                player.IsJumping = false;
            }
        }

        //--- DRAWING HELPERS ---//

        private void DrawPlatform()
        {
            Draw.FillColor = Color.DarkGray;
            Draw.Rectangle(platformPosition, platformSize);
        }

        private void DrawCollectible()
        {
            if (!hasCollected)
            {
                Draw.FillColor = Color.Yellow;
                Draw.Circle(collectiblePosition, collectibleSize);
            }
        }

        private void DrawHazard()
        {
            Draw.FillColor = Color.Magenta;
            Draw.Rectangle(hazardPosition, hazardSize);
        }

        private void DrawExit()
        {
            Draw.FillColor = Color.Red;
            Draw.Rectangle(exitPosition, exitSize);
        }

        private void DrawUI()
        {
            // Song UI
            Text.Color = Color.White;
            Text.Size = 16;
            Text.Draw($"Now Playing: {musicManager.CurrentSongTitle}", 50, 10);

            // Score
            Text.Draw($"You've Collected {score} albums!", 550, 10);

            // Health
            Text.Draw($"Health: {player.Health}", 50, 580);
        }
    }
}

