using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Models
{
    public class EditorTile : Clickable
    {
        private Texture2D tileSet;
        private Rectangle sourceRect;
        public GameObjectTypes objectType {  get; private set; }
        public PlattformTypes plattformType { get; private set; }
        public PlattformAttributes plattformAttribute { get; private set; }
        Color color;
        bool isFocused;

        public EditorTile(Texture2D tileSet, Rectangle sourceRect, GameObjectTypes objectType, Vector2 position, PlattformTypes plattformType=PlattformTypes.Solid, PlattformAttributes plattformAttribute=PlattformAttributes.None)
            : base(new((int)position.X, (int)position.Y, GameValues.ColumnWidth, GameValues.RowHeight), position)
        {
            this.tileSet = tileSet;
            this.sourceRect = sourceRect;
            this.objectType = objectType;
            this.color = Color.White;
            this.isFocused = false;
            this.plattformType = plattformType;
            this.plattformAttribute = plattformAttribute;
        }

        public void Update()
        {
            if (CheckIfInBounds(GameValues.NewMouseState.Position))
            {
                isFocused = true;
                color = Color.Gray;
            }
            else
            {
                isFocused = false;
                color = Color.White;
            }

            if (isFocused)
            {
                if (GameValues.IsRightMouseClicked())
                {
                    OnClick();
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(tileSet, _bounds, sourceRect, color);
        }
    }
}
