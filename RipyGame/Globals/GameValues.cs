using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using TLW_Plattformer.RipyGame.Models;

namespace TLW_Plattformer.RipyGame.Globals
{
    public static class GameValues
    {
        public static KeyboardState NewKeyboardState {  get; set; }
        public static KeyboardState OldKeyboardState {  get; set; }
        public static MouseState NewMouseState { get; set; }
        public static MouseState OldMouseState { get; set; }

        public static bool DebugColorsActive { get; private set; }
        public static bool UseCustomLevels { get; private set; }

        public static Keys P1_MoveLeftKey { get; private set; }
        public static Keys P1_MoveRightKey { get; private set; }
        public static Keys P1_JumpKey { get; private set; }
        public static Keys P1_CrouchKey { get; private set; }

        public static int TileRowCount { get; private set; }
        public static int TileColumnCount { get; private set; }
        public static int TileWidth { get; private set; }
        public static int TileHeight { get; private set; }
        public static Vector2 TileScale { get; private set; }
        public static int ColumnWidth { get; private set; }
        public static int RowHeight { get; private set; }
        public static Vector2 WindowSize { get; private set; }
        public static Vector2 WindowCenter { get; private set; }
        public static Rectangle WindowBounds { get; private set; }
        public static Vector2 HudStartPos { get; private set; }
        public static Vector2 HudEndPos { get; private set; }
        public static Vector2 LevelStartPos { get; set; }
        public static Vector2 LevelEndPos { get; set; }
        public static Rectangle LevelBounds { get; set; }

        public static float MapDrawLayer { get; private set; }
        public static float ItemDrawLayer { get; private set; }
        public static float EnemyDrawLayer { get; private set; }
        public static float PlayerDrawLayer { get; private set; }

        public static Vector2 PlayerScale { get; private set; }
        public static Rectangle PlayerBounds { get; private set; }
        public static float PlayerMoveSpeed { get; private set; }
        public static float PlayerJumpSpeed { get; private set; }
        public static float PlayerFallSpeed { get; private set; }
        public static Vector2 EnemyScale { get; private set; }

        public static SpriteFont ArcadeFont { get; private set; }

        public static float Time { get; private set; }

        public static void UpdateStart(GameTime gameTime)
        {
            Time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            NewKeyboardState = Keyboard.GetState();
            NewMouseState = Mouse.GetState();
        }
        public static void UpdateEnd()
        {
            OldKeyboardState = NewKeyboardState;
            OldMouseState = NewMouseState;
        }

        public static void InitializeValues(ContentManager Content)
        {
            // Change below to load in from json file to initialize the values
            //string gameValuesData = "../DataFiles/Globals/gameValuesData.json";

            DebugColorsActive = true;
            UseCustomLevels = false;

            P1_MoveLeftKey = Keys.A;
            P1_MoveRightKey = Keys.D;
            P1_JumpKey = Keys.Space;
            P1_CrouchKey = Keys.S;

            TileRowCount = 9;
            TileColumnCount = 16;
            TileWidth = 32;
            TileHeight = 32;
            TileScale = new Vector2(2, 2);
            ColumnWidth = TileWidth * (int)TileScale.X;
            RowHeight = TileHeight * (int)TileScale.Y;

            int WindowSizeX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int WindowSizeY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            WindowSize = new Vector2(WindowSizeX, WindowSizeY);
            WindowCenter = new Vector2(WindowSizeX / 2, WindowSizeY / 2);
            WindowBounds = new Rectangle(0, 0, WindowSizeX, WindowSizeY);

            LevelStartPos = new Vector2(0, 0);
            LevelEndPos = new Vector2(WindowSizeX, WindowSizeY);
            LevelBounds = new Rectangle(LevelStartPos.ToPoint(), LevelEndPos.ToPoint());
            //LevelBounds = new Rectangle(0, 0, 1920*2, 1080);

            MapDrawLayer = 0.01F;
            ItemDrawLayer = 0.5F;
            EnemyDrawLayer = 0.6F;
            PlayerDrawLayer = 0.10F;

            Vector2 tempPlayerStartPos = new Vector2(0, 0);
            PlayerScale = new Vector2(4, 10);
            PlayerBounds = new Rectangle((int)tempPlayerStartPos.X, (int)tempPlayerStartPos.Y, TileWidth * (int)PlayerScale.X, TileHeight * (int)PlayerScale.Y);

            ArcadeFont = Content.Load<SpriteFont>(GamePaths.ArcadeFontPath);
        }

        public static bool IsKeyPressed(Keys key)
        {
            if (NewKeyboardState.IsKeyDown(key) && OldKeyboardState.IsKeyUp(key))
            {
                return true;
            }
            return false;
        }

        public static bool IsLeftMouseClicked()
        {
            if (NewMouseState.LeftButton == ButtonState.Pressed && OldMouseState.LeftButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }
        public static bool IsRightMouseClicked()
        {
            if (NewMouseState.RightButton == ButtonState.Pressed && OldMouseState.RightButton == ButtonState.Released)
            {
                return true;
            }
            return false;
        }

        public static MoveableDirections GetCollisionDirection(GameObject mainObj, GameObject otherObj)
        {
            MoveableDirections collisionDirX = MoveableDirections.None;
            MoveableDirections collisionDirY = MoveableDirections.None;
            MoveableDirections leastDistanceDir = MoveableDirections.None;

            float rightDistance = mainObj.Bounds.Right - otherObj.Bounds.Left;
            float leftDistance = otherObj.Bounds.Right - mainObj.Bounds.Left;
            float bottomDistance = mainObj.Bounds.Bottom - otherObj.Bounds.Top;
            float topDistance = otherObj.Bounds.Bottom - mainObj.Bounds.Top;

            // Check which X and Y direction the collision is occuring
            if (rightDistance > leftDistance)
            {
                collisionDirX = MoveableDirections.Left;
            }
            else if (leftDistance > rightDistance)
            {
                collisionDirX = MoveableDirections.Right;
            }
            if (bottomDistance > topDistance)
            {
                collisionDirY = MoveableDirections.Up;
            }
            else if (topDistance > bottomDistance)
            {
                collisionDirY = MoveableDirections.Down;
            }

            // Set the direction of the collision to the least distance between the X and the Y collision directions
            if (collisionDirX == MoveableDirections.Left)
            {
                if (collisionDirY == MoveableDirections.Up)
                {
                    if (leftDistance > topDistance)
                    {
                        leastDistanceDir = MoveableDirections.Up;
                    }
                    else
                    {
                        leastDistanceDir = MoveableDirections.Left;
                    }
                }
                else if (collisionDirY == MoveableDirections.Down)
                {
                    if (leftDistance > bottomDistance)
                    {
                        leastDistanceDir = MoveableDirections.Down;
                    }
                    else
                    {
                        leastDistanceDir = MoveableDirections.Left;
                    }
                }
            }
            else if (collisionDirX == MoveableDirections.Right)
            {
                if (collisionDirY == MoveableDirections.Up)
                {
                    if (rightDistance > bottomDistance)
                    {
                        leastDistanceDir = MoveableDirections.Up;
                    }
                    else
                    {
                        leastDistanceDir = MoveableDirections.Right;
                    }
                }
                else if (collisionDirY == MoveableDirections.Down)
                {
                    if (rightDistance > bottomDistance)
                    {
                        leastDistanceDir = MoveableDirections.Down;
                    }
                    else
                    {
                        leastDistanceDir = MoveableDirections.Right;
                    }
                }
            }

            return leastDistanceDir;
        }
    }
}
