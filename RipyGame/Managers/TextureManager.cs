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
        public Texture2D LevelOneBackgroundTex { get; private set; }
        public Texture2D LevelOneTilesetTex { get; private set; }

        private Rectangle plattformMiddle_1;
        private Rectangle plattformMiddle_2;
        private Rectangle plattformMiddle_3;
        private Rectangle plattformLeft;
        private Rectangle plattformRight;
        private Rectangle plattformPortal;
        private Rectangle plattformBreakable;
        private Rectangle plattformSpikes;
        private Rectangle plattformMysteryBox;

        public Dictionary<PlattformTextureTypes, List<Rectangle>> PlattformSourceRectangles { get; private set; }

        public TextureManager(ContentManager Content)
        {
            #region Initialize Textures
            #region Backgrounds
            LevelOneBackgroundTex = Content.Load<Texture2D>(GamePaths.LevelOneBackgroundPath);
            #endregion Backgrounds

            #region Tilesets
            LevelOneTilesetTex = Content.Load<Texture2D>(GamePaths.LevelOneTilesetPath);
            #endregion Tilesets
            #endregion Initialize Textures

            #region Initialize Rectangles
            int tileWidth = 32;
            int tileHeight = 32;

            plattformMiddle_1 = new Rectangle(tileWidth * 0, tileHeight * 0, tileWidth, tileHeight);
            plattformMiddle_2 = new Rectangle(tileWidth * 1, tileHeight * 0, tileWidth, tileHeight);
            plattformMiddle_3 = new Rectangle(tileWidth * 2, tileHeight * 0, tileWidth, tileHeight);

            plattformLeft = new Rectangle(tileWidth * 0, tileHeight * 1, tileWidth, tileHeight);
            plattformRight = new Rectangle(tileWidth * 1, tileHeight * 1, tileWidth, tileHeight);

            plattformPortal = new Rectangle(tileWidth * 0, tileHeight * 2, tileWidth, tileHeight);
            plattformBreakable = new Rectangle(tileWidth * 1, tileHeight * 2, tileWidth, tileHeight);
            plattformSpikes = new Rectangle(tileWidth * 2, tileHeight * 2, tileWidth, tileHeight);
            plattformMysteryBox = new Rectangle(tileWidth * 3, tileHeight * 2, tileWidth, tileHeight);
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
                { PlattformTextureTypes.Plattform_Spikes, new List<Rectangle>() { plattformSpikes } },
                { PlattformTextureTypes.Plattform_MysteryBox, new List<Rectangle>() { plattformMysteryBox } },
            };
        }

        public Texture2D LoadAndGetTexture(string path, ContentManager Content)
        {
            Texture2D loadedTexture = Content.Load<Texture2D>(path);
            return loadedTexture;
        }
    }

    public enum PlattformTextureTypes
    {
        Plattform_Middle, Plattform_Left, Plattform_Right,
        Plattform_Portal, Plattform_Breakable,
        Plattform_Spikes, Plattform_MysteryBox
    }
}
