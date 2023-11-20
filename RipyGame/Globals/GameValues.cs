using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace TLW_Plattformer.RipyGame.Globals
{
    public static class GameValues
    {
        public static bool DebugColorsActive { get; private set; }
        public static bool UseCustomLevels { get; private set; }

        public static int TileRowCount { get; private set; }
        public static int TileColumnCount { get; private set; }
        public static int TileWidth { get; private set; }
        public static int TileHeight { get; private set; }
        public static Vector2 TileScale { get; private set; }
        public static Vector2 WindowSize { get; private set; }
        public static Vector2 WindowCenter { get; private set; }
        public static Rectangle WindowBounds { get; private set; }
        public static Vector2 HudStartPos { get; private set; }
        public static Vector2 HudEndPos { get; private set; }
        public static Vector2 LevelStartPos { get; private set; }
        public static Vector2 LevelEndPos { get; private set; }
        public static Rectangle LevelBounds { get; private set; }

        public static float MapDrawLayer { get; private set; }
        public static float ItemDrawLayer { get; private set; }
        public static float EnemyDrawLayer { get; private set; }
        public static float PlayerDrawLayer { get; private set; }

        public static Vector2 PlayerScale { get; private set; }
        public static Rectangle PlayerBounds { get; private set; }

        public static SpriteFont ArcadeFont { get; private set; }

        public static float Time { get; private set; }

        public static void Update(GameTime gameTime)
        {
            Time = (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static void InitializeValues(ContentManager Content)
        {
            // Change below to load in from json file to initialize the values
            //string gameValuesData = "../DataFiles/Globals/gameValuesData.json";

            DebugColorsActive = true;
            UseCustomLevels = false;

            TileRowCount = 9;
            TileColumnCount = 16;
            TileWidth = 32;
            TileHeight = 32;
            TileScale = new Vector2(1, 1);

            int WindowSizeX = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            int WindowSizeY = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            WindowSize = new Vector2(WindowSizeX, WindowSizeY);
            WindowCenter = new Vector2(WindowSizeX / 2, WindowSizeY / 2);
            WindowBounds = new Rectangle(0, 0, WindowSizeX, WindowSizeY);

            LevelStartPos = new Vector2(0, 0);
            LevelEndPos = new Vector2(WindowSizeX, WindowSizeY);
            LevelBounds = new Rectangle(LevelStartPos.ToPoint(), LevelEndPos.ToPoint());

            MapDrawLayer = 0.1F;
            ItemDrawLayer = 0.2F;
            EnemyDrawLayer = 0.3F;
            PlayerDrawLayer = 0.4F;

            Vector2 tempPlayerStartPos = new Vector2(0, 0);
            PlayerScale = new Vector2(1, 1);
            PlayerBounds = new Rectangle((int)tempPlayerStartPos.X, (int)tempPlayerStartPos.Y, TileWidth * (int)PlayerScale.X, TileHeight * (int)PlayerScale.Y);

            ArcadeFont = Content.Load<SpriteFont>(GamePaths.ArcadeFontPath);
        }
    }
}
