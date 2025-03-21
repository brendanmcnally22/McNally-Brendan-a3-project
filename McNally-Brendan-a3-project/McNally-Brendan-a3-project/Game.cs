﻿using System;
using System.Collections;
using System.Numerics;
using MohawkGame2D;

namespace MohawkGame2D
{
    public class Game
    { 
        enum gamestate // An Enumerator to make sure we Store infomation in these screens 
        {
            Menu,
            Level1,
            Level2,
            Level3,
            Gameover,
            Winner
        }
        

        // States
        private gamestate currentState = gamestate.Menu; // start the game in the main menu


        // Managers / Helpers
        private MusicManager musicManager;
        private Borders borders;
        private Player player;
       

        // Player Score
        private int score = 0;

      
        // Exit
        private Vector2 exitPosition;
        private Vector2 exitSize = new Vector2(40, 40);


        // Array for Platforms, Hazards and Collectibles
        private Platform[] platforms = new Platform[10];
        private Hazard[] hazards;
        private Collectible[] collectibles;

        private float totalElapsedTime = 0;

        //Loading in Textures
        private Texture2D level1Background; 
        private Texture2D level2Background;
        private Texture2D level3Background;
        private Texture2D menuScreen;
        private Texture2D gameOverScreen;
        private Texture2D winnerScreen;

        public void Setup()
        {
            // Window settings
            Window.SetSize(800, 600);
            Window.SetTitle("Frisson");


            level1Background = Graphics.LoadTexture("../../../../../Assets/graphics/Frisson-LVL1.png");
            level2Background = Graphics.LoadTexture("../../../../../Assets/graphics/Frisson-LVL2.png");
            level3Background = Graphics.LoadTexture("../../../../../Assets/graphics/Frisson-LVL3.png");
            menuScreen = Graphics.LoadTexture("../../../../../Assets/graphics/Frisson-Menu.png");
            gameOverScreen = Graphics.LoadTexture("../../../../../Assets/graphics/Frisson-GameOver.png");
            winnerScreen = Graphics.LoadTexture("../../../../../Assets/graphics/Frisson-Winner.png");

            // Initialize music
            string[] songPaths = {

                "../../../../../Assets/music/Moonlight Serenade.mp3",
                "../../../../../Assets/music/Come as you Are.mp3",
                "../../../../../Assets/music/Real Gone.mp3",
                "../../../../../Assets/music/The Moment.mp3",


            };
            // Titles of The songs
            string[] songTitles = 
                {

                "Moonlight Serenade",
                "Come as You Are",
                "Real Gone", 
                "The Moment",
                
            };
 

            musicManager = new MusicManager(songPaths, songTitles);
         
                musicManager.PlayMusic(0);

            

            // Create other helpers

            borders = new Borders();
            player = new Player();
            platforms = Platform.InitializePlatforms(1);  // Load up them platforms! 
            collectibles = Collectible.InitializeCollectibles(1); // Collectibles
            hazards = Hazard.InitializeHazards(1);          // <--- Initialize hazards!

        }

        public void Update(float deltaTime = 0.016f)
        {
            Window.ClearBackground(Color.Gray);

            totalElapsedTime += deltaTime;

            switch (currentState) // Switch to change our current State of the game.
            {
                case gamestate.Menu: // Main Menu

                    if (musicManager.CurrentSongTitle != "Moonlight Serenade")
                    {
                        musicManager.PlayMusic(0);
                    }
                    DrawMenu();
                    HandleMenuInput();
                    break;

                case gamestate.Level1: // Level one
                    // Update logic
                    player.HandleInput();
                    player.Update();
                    // Collisions
                    ApplyCollisions();
                    // Drawing
                    DrawBackground();
                    platforms = Platform.InitializePlatforms(1);
                    DrawPlatforms(platforms);
                    borders.DrawBorders();
                    player.DrawPlayer();
                    DrawCollectibles();
                    DrawHazards();
                    DrawExit();
                    DrawUI();

                    foreach (Collectible col in collectibles) //collisions
                    {
                        if (col != null)
                        {
                            col.Update(totalElapsedTime);
                        }
                    }

                    break;

                case gamestate.Level2: // Level two
                    player.HandleInput();
                    player.Update();
                    ApplyCollisions();
                    DrawBackground();
                    borders.DrawBorders();
                    player.DrawPlayer();
                    DrawCollectibles();
                    DrawHazards();
                    DrawExit();
                    DrawUI();
                    platforms = Platform.InitializePlatforms(2);
                    DrawPlatforms(platforms);
                   

                    foreach (Collectible col in collectibles) //collisions
                    {
                        if (col != null)
                        {
                            col.Update(totalElapsedTime);
                        }
                    }

                    break;
                //level 2 logic! 

                case gamestate.Level3:
                    player.HandleInput();
                    player.Update();
                    ApplyCollisions();
                    DrawBackground();
                    borders.DrawBorders();
                    player.DrawPlayer();
                    DrawCollectibles();
                    DrawHazards();
                    DrawExit();
                    DrawUI();
                    platforms = Platform.InitializePlatforms(3);
                    DrawPlatforms(platforms);
                    
                    foreach (Collectible col in collectibles) //collisions
                    {
                        if (col != null)
                        {
                            col.Update(totalElapsedTime);
                        }
                    }

                    break;

                case gamestate.Gameover: //Game Over Screen
                    DrawGameOverScreen();
                    HandleGameOverInput();
                    break;

                case gamestate.Winner: //Winner Screen
                    DrawWinnerScreen();
                    HandleWinnerInput();
                    break;

            }
        }

        //--- MENU & GAME OVER ---//

        private void DrawMenu()
        {
            // Draw the menu background image.
            if (menuScreen != null && menuScreen.Width > 0 && menuScreen.Height > 0)
            {
              
                Graphics.Draw(menuScreen, new Vector2(0, 0));
            }
            else
            {
               
                Draw.FillColor = Color.DarkGray;
                Draw.Rectangle(new Vector2(0, 0), new Vector2(800, 600));
            }
        }

        private void HandleMenuInput()
        {
            if (Input.IsControllerButtonPressed(0, ControllerButton.RightFaceLeft) || Input.IsKeyboardKeyPressed(KeyboardInput.Enter))
            {
                currentState = gamestate.Level1;
                SetExitPositionForLevel();
                musicManager.PlayMusic(1);
            }
            
        }

        private void DrawGameOverScreen()
        {
            if (gameOverScreen != null && gameOverScreen.Width > 0 && gameOverScreen.Height > 0)
            {
                // Draw the game over screen texture.
                Graphics.Draw(gameOverScreen, new Vector2(0, 0));
            }
        }
        private void DrawWinnerScreen()
        {
            if (winnerScreen != null && winnerScreen.Width > 0 && winnerScreen.Height > 0)
            {
                // Draw the winner screen texture.
                Graphics.Draw(winnerScreen, new Vector2(0, 0));
            }
        }
        private void HandleGameOverInput()
        {
            if (Input.IsControllerButtonPressed(0, ControllerButton.RightFaceLeft) || Input.IsKeyboardKeyPressed(KeyboardInput.Enter))
            {
                RestartGame();
            }
        }

        private void RestartGame()
        {
            player.Health = 100;
            player.Position = new Vector2(100, 500);
            currentState = gamestate.Level1;
            platforms = Platform.InitializePlatforms(1);
            hazards = Hazard.InitializeHazards(1);
            collectibles = Collectible.InitializeCollectibles(1);
            SetExitPositionForLevel();
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

            // Hazard collisions (loop through each hazard)
            foreach (Hazard hazard in hazards)
            {
                if (IsColliding(player.Position, player.Width, player.Height, hazard.Position, hazard.Size))
                {
                    player.Health -= 10;
                    if (player.Health <= 0)
                    {
                        player.Health = 0;
                        currentState = gamestate.Gameover;
                        musicManager.PlayMusic(1);
                    }
                }
            }

            // Collectible collisions (loop through each collectible)
            for (int i = 0; i < collectibles.Length; i++)
            {
                Collectible col = collectibles[i];
                if (col != null && IsColliding(player.Position, player.Width, player.Height, col.Position, new Vector2(col.Size * 2, col.Size * 2)))
                {
                    score++;
                    player.Health += 50;
                    // Remove collectible after collection
                    collectibles[i] = null;
                }
            }

            // Exit collision: advance level or end game
            if (Vector2.Distance(player.Position, exitPosition) < 30)
            {
                if (currentState == gamestate.Level1) // Level 1 Because Menu is 0
                {
                    // Transition from Level1 to Level2
                    currentState = gamestate.Level2;
                    platforms = Platform.InitializePlatforms(2);
                    hazards = Hazard.InitializeHazards(2);
                    collectibles = Collectible.InitializeCollectibles(2);
                    player.Position = new Vector2(100, 500); // Reset player starting position if needed
                    musicManager.PlayMusic(2);
                }
                else if (currentState == gamestate.Level2)
                {
                    // Transition from Level2 to Level3
                    currentState = gamestate.Level3;
                    platforms = Platform.InitializePlatforms(3);
                    hazards = Hazard.InitializeHazards(3);
                    collectibles = Collectible.InitializeCollectibles(3);
                    player.Position = new Vector2(100, 500);
                    musicManager.PlayMusic(3);
                }
                else if (currentState == gamestate.Level3)
                {
                    // Final exit: Winner!
                    currentState = gamestate.Winner;
                    musicManager.PlayMusic(0);
                }

                SetExitPositionForLevel(); //Setting The Exit Position for the Level

                if (IsColliding(player.Position, player.Width, player.Height, exitPosition, exitSize))
                {
                    // Change to the next song when the player touches the exit.
                    musicManager.NextSong();

                }
            }

        }

        private bool IsColliding(Vector2 posA, float widthA, float heightA, Vector2 posB, Vector2 sizeB)
        {
            return posA.X < posB.X + sizeB.X &&
                   posA.X + widthA > posB.X &&
                   posA.Y < posB.Y + sizeB.Y &&
                   posA.Y + heightA > posB.Y;
        }

        private void CheckPlatformCollision()
        {
            // Player edges
            float playerBottom = player.Position.Y + player.Height;
            float playerTop = player.Position.Y;
            float playerLeft = player.Position.X;
            float playerRight = player.Position.X + player.Width;

            // Iterate over the platforms
            foreach (Platform platform in platforms)
            {
                // Platform edges
                float platTop = platform.Position.Y;
                float platBottom = platform.Position.Y + platform.Size.Y;
                float platLeft = platform.Position.X;
                float platRight = platform.Position.X + platform.Size.X;

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
        }

        //--- DRAWING HELPERS ---//
        private void DrawPlatforms(Platform[] platforms)
        {
            foreach (Platform platform in platforms)
            {
                platform.DrawPlatform();
            }
        }

        private void DrawCollectibles()
        {
            foreach (Collectible col in collectibles)
            {
                if (col != null)
                {
                    col.DrawCollectible();
                }
            }
        }
        private void DrawHazards()
        {
            foreach (Hazard hazard in hazards)
            {
                hazard.DrawHazard();
            }
        }

        private void DrawExit()
        {
            Draw.FillColor = Color.Green;
            Draw.Rectangle(exitPosition, exitSize);
        }
        private void SetExitPositionForLevel()
        {
            switch (currentState)
            {
                case gamestate.Level1:
                    exitPosition = new Vector2(650, 250);  
                    break;
                case gamestate.Level2:
                    exitPosition = new Vector2(300, 50);  
                    break;
                case gamestate.Level3:
                    exitPosition = new Vector2(650, 150); 
                    break;
                default:
                    exitPosition = new Vector2(540, 400);  // Fallback position
                    break;
            }
        }
        private void DrawBackground()
        {
            Texture2D currentBg = default(Texture2D); 
            switch (currentState)
            {
                case gamestate.Level1:
                    currentBg = level1Background;
                    break;

                case gamestate.Level2:
                    currentBg = level2Background;
                    break;
                case gamestate.Level3:
                    currentBg = level3Background;
                    break;
        }
            if (currentBg.Width > 0 && currentBg.Height > 0)
            {
                Graphics.Draw(currentBg, new Vector2(0, 0));
            }
            else
            {
                // Fallback: Draw a solid dark gray rectangle
                Draw.FillColor = Color.DarkGray;
                Draw.Rectangle(new Vector2(0, 0), new Vector2(800, 600));
            }
        }

        private void HandleWinnerInput()
        {
            
            if (Input.IsControllerButtonPressed(0, ControllerButton.RightFaceLeft) ||
                Input.IsKeyboardKeyPressed(KeyboardInput.Enter))
                
            {
                RestartGame();
                player.Position = new Vector2(100, 500);
                player.Health = 100;
                score = 0;

                currentState = gamestate.Menu;
               
                musicManager.PlayMusic(0); // Return to menu music
            }
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



