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
        Rectangle sourceRect;
        public GameObjectTypes objectType {  get; private set; }
        Color color;
        bool isFocused;

        public EditorTile(Rectangle sourceRect, GameObjectTypes objectType, Vector2 position)
            : base(new((int)position.X, (int)position.Y, GameValues.ColumnWidth, GameValues.RowHeight), position)
        {
            this.sourceRect = sourceRect;
            this.objectType = objectType;
            this.color = Color.White;
            this.isFocused = false;
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

        public void Draw(SpriteBatch spriteBatch, Texture2D tileSheet)
        {
            spriteBatch.Draw(tileSheet, _bounds, sourceRect, color);
        }
    }
}
