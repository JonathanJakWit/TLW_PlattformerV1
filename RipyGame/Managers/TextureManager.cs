using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Managers
{
    public class TextureManager
    {
        #region Extras
        public Texture2D NoneTex { get; private set; }
        public Texture2D FullTex { get; private set; }
        #endregion Extras

        #region HUD
        public Texture2D PauseIcon {  get; private set; }
        public Rectangle PauseIconDestRect { get; private set; }
        public Texture2D MenuButton {  get; private set; }
        #endregion HUD

        #region Backgrounds
        public Texture2D MainMenuBackgroundTex { get; private set; }
        public Texture2D HighscoreMenuBackgroundTex { get; private set; }
        public Texture2D GameOverWinMenuBackgroundTex { get; private set; }
        public Texture2D GameOverLoseMenuBackgroundTex { get; private set; }
        public Texture2D LevelOneBackgroundTex { get; private set; }
        #endregion Backgrounds

        #region Tilesets
        public Texture2D LevelOneTilesetTex { get; private set; }
        #endregion Tilesets

        #region SpriteSheets
        public Texture2D PlayerSpritesheet {  get; private set; }
        #endregion SpriteSheets

        private Rectangle plattformMiddle_1;
        private Rectangle plattformMiddle_2;
        private Rectangle plattformMiddle_3;
        private Rectangle plattformLeft;
        private Rectangle plattformRight;
        private Rectangle plattformPortal;
        private Rectangle plattformBreakable;
        private Rectangle plattformMysteryBox;

        private Rectangle plattformLeftSpikes;
        private Rectangle plattformRightSpikes;
        private Rectangle plattformTopSpikes;
        private Rectangle plattformBottomSpikes;

        public Dictionary<PlattformTextureTypes, List<Rectangle>> PlattformSourceRectangles { get; private set; }

        public TextureManager(ContentManager Content, int currentLevelIndex)
        {
            #region Initialize Textures
            #region Extras
            NoneTex = Content.Load<Texture2D>("noneTex");
            FullTex = Content.Load<Texture2D>("fullTex");
            #endregion Extras

            #region Hud
            PauseIcon = Content.Load<Texture2D>(GamePaths.PauseIconPath);
            MenuButton = Content.Load<Texture2D>(GamePaths.MenuButtonPath);
            #endregion Hud

            #region Backgrounds
            MainMenuBackgroundTex = Content.Load<Texture2D>(GamePaths.MainMenuBackgroundPath);
            HighscoreMenuBackgroundTex = Content.Load<Texture2D>(GamePaths.HighscoreMenuBackgroundPath);
            GameOverWinMenuBackgroundTex = Content.Load<Texture2D>(GamePaths.GameOverWinMenuBackgroundPath);
            GameOverLoseMenuBackgroundTex = Content.Load<Texture2D>(GamePaths.GameOverLoseMenuBackgroundPath);

            LevelOneBackgroundTex = Content.Load<Texture2D>(GamePaths.LevelOneBackgroundPath);
            #endregion Backgrounds

            #region Tilesets
            LevelOneTilesetTex = Content.Load<Texture2D>(GamePaths.LevelOneTilesetPath);
            #endregion Tilesets

            #region Spritesheets
            PlayerSpritesheet = Content.Load<Texture2D>(GamePaths.PlayerSpriteSheetPath);
            #endregion Spritesheets
            #endregion Initialize Textures

            #region Initialize Rectangles
            int tileWidth = 32;
            int tileHeight = 32;

            #region Plattforms
            plattformMiddle_1 = new Rectangle(tileWidth * 0, tileHeight * 0, tileWidth, tileHeight);
            plattformMiddle_2 = new Rectangle(tileWidth * 1, tileHeight * 0, tileWidth, tileHeight);
            plattformMiddle_3 = new Rectangle(tileWidth * 2, tileHeight * 0, tileWidth, tileHeight);

            plattformLeft = new Rectangle(tileWidth * 0, tileHeight * 1, tileWidth, tileHeight);
            plattformRight = new Rectangle(tileWidth * 1, tileHeight * 1, tileWidth, tileHeight);

            plattformPortal = new Rectangle(tileWidth * 0, tileHeight * 2, tileWidth, tileHeight);
            plattformBreakable = new Rectangle(tileWidth * 1, tileHeight * 2, tileWidth, tileHeight);
            plattformMysteryBox = new Rectangle(tileWidth * 2, tileHeight * 2, tileWidth, tileHeight);

            plattformLeftSpikes = new Rectangle(tileWidth * 0, tileHeight * 3, tileWidth, tileHeight);
            plattformRightSpikes = new Rectangle(tileWidth * 1, tileHeight * 3, tileWidth, tileHeight);
            plattformTopSpikes = new Rectangle(tileWidth * 2, tileHeight * 3, tileWidth, tileHeight);
            plattformBottomSpikes = new Rectangle(tileWidth * 3, tileHeight * 3, tileWidth, tileHeight);
            #endregion Plattforms

            #region Hud
            PauseIconDestRect = new Rectangle((int)GameValues.WindowCenter.X, (int)GameValues.WindowCenter.Y, tileWidth * (int)GameValues.TileScale.X, tileHeight * (int)GameValues.TileScale.Y);
            #endregion Hud
            #endregion Initialize Rectangles

            PlattformSourceRectangles = new Dictionary<PlattformTextureTypes, List<Rectangle>>()
            {
                { PlattformTextureTypes.Plattform_Middle, new List<Rectangle>()
                    {
                        plattformMiddle_1, plattformMiddle_2, plattformMiddle_3
                    }
                },
                { PlattformTextureTypes.Plattform_Left, new List<Rectangle>() { plattformLeft } },
                { PlattformTextureTypes.Plattform_Right, new List<Rectangle>() { plattformRight } },
                { PlattformTextureTypes.Plattform_Portal, new List<Rectangle>() { plattformPortal } },
                { PlattformTextureTypes.Plattform_Breakable, new List<Rectangle>() { plattformBreakable } },
                { PlattformTextureTypes.Plattform_MysteryBox, new List<Rectangle>() { plattformMysteryBox } },
                { PlattformTextureTypes.PlattformLeft_Spikes, new List<Rectangle>() { plattformLeftSpikes } },
                { PlattformTextureTypes.PlattformRight_Spikes, new List<Rectangle>() { plattformRightSpikes } },
                { PlattformTextureTypes.PlattformTop_Spikes, new List<Rectangle>() { plattformTopSpikes } },
                { PlattformTextureTypes.PlattformBottom_Spikes, new List<Rectangle>() { plattformBottomSpikes } },
            };
        }

        public Texture2D LoadAndGetTexture(string path, ContentManager Content)
        {
            Texture2D loadedTexture = Content.Load<Texture2D>(path);
            return loadedTexture;
        }

        //public Texture2D GetSpriteSheet(SpriteSheetTextureTypes spriteSheetTextureType)
        //{
        //    switch (spriteSheetTextureType)
        //    {
        //        case SpriteSheetTextureTypes.None:
        //            break;
        //        case SpriteSheetTextureTypes.Player:
        //            break;
        //        default:
        //            break;
        //    }
        //}
    }

    public enum PlattformTextureTypes
    {
        Plattform_Middle, Plattform_Left, Plattform_Right,
        Plattform_Portal, Plattform_Breakable,
        Plattform_MysteryBox,
        PlattformLeft_Spikes, PlattformRight_Spikes, PlattformTop_Spikes, PlattformBottom_Spikes
    }

    //public enum SpriteSheetTextureTypes
    //{
    //    None,
    //    Player,
    //}
}
