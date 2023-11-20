using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Managers;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Plattform
    {
        public PlattformTypes PlattformType { get; set; }
        public Vector2 Position { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle Bounds { get; set; }

        private int tileRowCount;
        private int tileColumnCount;

        private List<Rectangle> _sourceRects;

        public Plattform(PlattformTypes plattformType, Vector2 position, int width, int height)
        {
            this.PlattformType = plattformType;
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.Bounds = new Rectangle((int)position.X, (int)position.Y, width, height);

            this.tileRowCount = height / GameValues.TileHeight;
            this.tileColumnCount = width / GameValues.TileWidth;

            this._sourceRects = new List<Rectangle>();
        }

        public void InitializeSourceRectangles(Dictionary<PlattformTextureTypes, Rectangle> plattformSourceRects)
        {
            if (PlattformType == PlattformTypes.Solid)
            {
                _sourceRects.Add(plattformSourceRects.GetValueOrDefault(PlattformTextureTypes.Plattform_Left));
                for (int i = 0; i < tileColumnCount - 2; i++)
                {
                    _sourceRects.Add(plattformSourceRects.GetValueOrDefault(PlattformTextureTypes.Plattform_Middle));
                }
                _sourceRects.Add(plattformSourceRects.GetValueOrDefault(PlattformTextureTypes.Plattform_Right));
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tileSet)
        {
            int tileIndex = 0;
            foreach (Rectangle srcRect in _sourceRects)
            {
                Vector2 currentRectPos = new Vector2(Position.X * tileIndex * GameValues.TileWidth * (int)GameValues.TileScale.X, Position.Y);
                spriteBatch.Draw(tileSet, currentRectPos, srcRect, Color.White);
                tileIndex++;
            }
        }
    }

    public enum PlattformTypes
    {
        Solid,
        Interactable
    }

    public enum PlattformAttributes
    {
        None,
        Passage,
        Portal,
        Breakable,
        Dangerous,
        ItemSpawn
    }
}
