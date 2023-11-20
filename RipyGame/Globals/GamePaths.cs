using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLW_Plattformer.RipyGame.Globals
{
    public static class GamePaths
    {
        public static string FontsPath {  get; private set; }
        public static string ArcadeFontPath {  get; private set; }
        public static string BackgroundsPath { get; private set; }
        public static string LevelOneBackgroundPath { get; private set; }
        public static string SpritesPath { get; private set; }
        public static string SpriteSheetsPath { get; private set; }
        public static string TilesetsPath { get; private set; }
        public static string LevelOneTilesetPath { get; private set; }

        public static string DataFilesPath { get; private set; }
        public static string HighscoreDataPath { get; private set; }
        public static string LevelsDataPath { get; private set; }
        public static string LevelOneDataPath { get; private set; }

        public static void InitializePaths()
        {
            // Change below to load in from json file to initialize the values
            //string gamePathsData = "../DataFiles/Globals/gamePathsData.json";

            FontsPath = "Fonts/";
            ArcadeFontPath = FontsPath + "arcadeFont";
            BackgroundsPath = "Backgrounds/";
            LevelOneBackgroundPath = BackgroundsPath + "level_one_background";
            SpritesPath = "Sprites/";
            SpriteSheetsPath = "SpriteSheets/";
            TilesetsPath = "Tilesets/";
            LevelOneTilesetPath = TilesetsPath + "level_one_tileset";

            DataFilesPath = "../DataFiles/";
            HighscoreDataPath = DataFilesPath + "Highscores/highscoreData.json";
            LevelsDataPath = DataFilesPath + "Levels/";
            LevelOneDataPath = LevelsDataPath + "levelOneData.json";
        }
    }
}
