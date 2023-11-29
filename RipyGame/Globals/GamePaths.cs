using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLW_Plattformer.RipyGame.Globals
{
    public static class GamePaths
    {
        #region Fonts
        public static string FontsPath {  get; private set; }
        public static string ArcadeFontPath {  get; private set; }
        #endregion Fonts

        #region Hud
        public static string HudPath { get; private set; }
        public static string PauseIconPath { get; private set; }
        public static string MenuButtonPath { get; private set; }
        #endregion Hud

        #region Backgrounds
        public static string BackgroundsPath { get; private set; }
        public static string MainMenuBackgroundPath { get; private set; }
        public static string HighscoreMenuBackgroundPath { get; private set; }
        public static string GameOverWinMenuBackgroundPath { get; private set; }
        public static string GameOverLoseMenuBackgroundPath { get; private set; }

        public static string LevelOneBackgroundsPath { get; private set; }
        public static string LevelOneFarBackgroundPath { get; private set; }
        public static string LevelOneMiddleBackgroundPath { get; private set; }
        public static string LevelOneNearBackgroundPath { get; private set; }
        #endregion Backgrounds

        #region Sprites
        public static string SpritesPath { get; private set; }
        #endregion Sprites

        #region SpriteSheets
        public static string SpriteSheetsPath { get; private set; }
        public static string PlayerSpriteSheetPath { get; private set; }
        public static string Emberax_Idle_SpriteSheetPath { get; private set; }
        #endregion SpriteSheets

        #region Tilesets
        public static string TilesetsPath { get; private set; }
        public static string LevelOneTilesetPath { get; private set; }
        public static string StonePlattformTilesetPath { get; private set; }
        #endregion Tilesets

        #region DataFiles
        public static string DataFilesPath { get; private set; }
        public static string HighscoreDataPath { get; private set; }
        public static string LevelsDataPath { get; private set; }
        public static string LevelOneDataPath { get; private set; }
        #endregion DataFiles

        #region JsonDataFiles
        public static string JsonDataFilesPath { get; private set; }
        public static string JsonLevelDataFilesPath { get; private set; }
        public static string JsonLevelOneDataPath { get; private set; }
        #endregion JsonDataFiles


        public static void InitializePaths()
        {
            // Change below to load in from json file to initialize the values
            //string gamePathsData = "../DataFiles/Globals/gamePathsData.json";

            FontsPath = "Fonts/";
            ArcadeFontPath = FontsPath + "arcadeFont";

            HudPath = "Hud/";
            PauseIconPath = HudPath + "pause_icon";
            MenuButtonPath = HudPath + "menu_button";

            BackgroundsPath = "Backgrounds/";
            MainMenuBackgroundPath = BackgroundsPath + "main_menu_background";
            HighscoreMenuBackgroundPath = BackgroundsPath + "highscore_menu_background";
            GameOverWinMenuBackgroundPath = BackgroundsPath + "game_over_win_background";
            GameOverLoseMenuBackgroundPath = BackgroundsPath + "game_over_lose_background";
            //LevelOneBackgroundPath = BackgroundsPath + "level_one_background_AI"; // Placeholder for now
            LevelOneBackgroundsPath = BackgroundsPath + "LevelOne/";
            LevelOneFarBackgroundPath = LevelOneBackgroundsPath + "level_one_far_background";
            LevelOneMiddleBackgroundPath = LevelOneBackgroundsPath + "level_one_middle_background";
            LevelOneNearBackgroundPath = LevelOneBackgroundsPath + "level_one_near_background";
            //LevelOneBackgroundPath = BackgroundsPath + "level_one_background";

            SpritesPath = "Sprites/";

            SpriteSheetsPath = "Spritesheets/";
            PlayerSpriteSheetPath = SpriteSheetsPath + "player_spritesheet";
            Emberax_Idle_SpriteSheetPath = SpriteSheetsPath + "emberax_idle_spritesheet";

            TilesetsPath = "Tilesets/";
            LevelOneTilesetPath = TilesetsPath + "level_one_tileset";
            StonePlattformTilesetPath = TilesetsPath + "stone_plattform_tileset2";

            DataFilesPath = "../../../RipyGame/DataFiles/";
            HighscoreDataPath = DataFilesPath + "Highscores/highscoreData.xml";
            LevelsDataPath = DataFilesPath + "Levels/";
            LevelOneDataPath = LevelsDataPath + "levelOneData.json";

            JsonDataFilesPath = "JsonDataFiles/";
            JsonLevelDataFilesPath = JsonDataFilesPath + "LevelDataFiles/";
            JsonLevelOneDataPath = JsonLevelDataFilesPath + "level_one_data.json";
        }
    }
}
