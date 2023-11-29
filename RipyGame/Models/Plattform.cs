using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Managers;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Plattform : GameObject
    {
        public PlattformTypes PlattformType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private int tileRowCount;
        private int tileColumnCount;

        private List<Rectangle> _destinationRectangles;
        private List<Rectangle> _sourceRects;

        public Plattform(TextureManager textureManager, PlattformTypes plattformType, Vector2 position, int width, int height)
        {
            this.PlattformType = plattformType;
            this.Position = position;
            this.Width = width;
            this.Height = height;
            this.Bounds = new Rectangle((int)position.X, (int)position.Y, width * (int)GameValues.TileScale.X, height * (int)GameValues.TileScale.Y / 2);
            this.Center = new Vector2(position.X + Bounds.Width / 2, position.Y + Bounds.Height / 2);

            this.tileRowCount = height / GameValues.TileHeight;
            this.tileColumnCount = width / GameValues.TileWidth;

            this._destinationRectangles = new List<Rectangle>();
            int tileCount = width / GameValues.TileWidth;
            for (int i = 0; i < tileCount; i++)
            {
                _destinationRectangles.Add(new Rectangle((int)position.X + GameValues.ColumnWidth * i, (int)position.Y, GameValues.ColumnWidth, GameValues.RowHeight));
            }

            this._sourceRects = new List<Rectangle>();
            InitializeSourceRectangles(textureManager.PlattformSourceRectangles);
        }

        //public Plattform(TextureManager textureManager, PlattformTypes plattformType, Vector2 startPos, Vector2 endPos)
        //{
        //    this.PlattformType = plattformType;
        //    this.Position = startPos;
        //    this.Width = (int)endPos.X - (int)startPos.X;
        //    Width = Width / GameValues.ColumnWidth;
        //    Height = GameValues.RowHeight;
        //    this.Bounds = new Rectangle((int)Position.X, (int)Position.Y, Width * (int)GameValues.TileScale.X, Height);

        //    this.tileRowCount = Height / GameValues.TileHeight;
        //    this.tileColumnCount = Width / GameValues.TileWidth;

        //    this._destinationRectangles = new List<Rectangle>();
        //    int tileCount = Width / GameValues.TileWidth;
        //    for (int i = 0; i < tileCount; i++)
        //    {
        //        _destinationRectangles.Add(new Rectangle((int)Position.X + GameValues.ColumnWidth * i, (int)Position.Y, GameValues.ColumnWidth, GameValues.RowHeight));
        //    }

        //    this._sourceRects = new List<Rectangle>();
        //    InitializeSourceRectangles(textureManager.PlattformSourceRectangles);
        //}

        public void InitializeSourceRectangles(Dictionary<PlattformTextureTypes, List<Rectangle>> plattformSourceRects)
        {
            if (PlattformType == PlattformTypes.Solid)
            {
                _sourceRects.Add(plattformSourceRects.GetValueOrDefault(PlattformTextureTypes.Plattform_Left)[0]);
                for (int i = 0; i < tileColumnCount - 2; i++)
                {
                    _sourceRects.Add(plattformSourceRects.GetValueOrDefault(PlattformTextureTypes.Plattform_Middle)[0]);
                }
                _sourceRects.Add(plattformSourceRects.GetValueOrDefault(PlattformTextureTypes.Plattform_Right)[0]);
            }
        }

        public void MoveTo(Vector2 newPos)
        {
            Position = newPos;
            //Center = Position;
            Center = new Vector2(Position.X + Bounds.Width / 2, Position.Y + Bounds.Height / 2);
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D tileSet)
        {
            for (int i = 0; i < tileColumnCount; i++)
            {
                spriteBatch.Draw(tileSet, _destinationRectangles[i], _sourceRects[i], Color.White);
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
