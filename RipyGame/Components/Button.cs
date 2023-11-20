using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Models;

namespace TLW_Plattformer.RipyGame.Components
{
    internal class Button : Clickable
    {
        private string buttonText;
        private SpriteFont textFont;
        private Vector2 textMidPoint;
        Vector2 textPos;
        Vector2 textScale;

        private Texture2D idleTex;
        private Texture2D inFocusTex;
        private Color idleColor;
        private Color inFocusColor;

        private bool isFocused;

        private Vector2 scale;

        private Vector2 og_Position;
        private Rectangle og_Bounds;

        // Button with different Textures
        public Button(string buttonText, SpriteFont textFont, Texture2D idleTexture, Texture2D inFocusTexture, Vector2 position, Color idleColour, Vector2 scale)
            : base(new((int)position.X, (int)position.Y, idleTexture.Width * (int)scale.X, idleTexture.Height * (int)scale.Y), position)
        {
            this.buttonText = buttonText;
            this.textFont = textFont;
            this.idleTex = idleTexture;
            this.inFocusTex = inFocusTexture;
            this.idleColor = idleColour;
            this.inFocusColor = Color.White;

            this.textMidPoint = textFont.MeasureString(buttonText) / 2;
            this.textPos = new Vector2(position.X + idleTex.Width * 2, position.Y + idleTex.Height * 2);
            this.textScale = new Vector2(1, 1);

            this.og_Position = position;
            this.og_Bounds = new((int)position.X, (int)position.Y, idleTexture.Width, idleTexture.Height);
        }

        // Button with one Texture and different Colors
        public Button(string buttonText, SpriteFont textFont, Texture2D idleTexture, Color inFocusColour, Vector2 position, Color idleColour, Vector2 scale)
            : base(new((int)position.X, (int)position.Y, idleTexture.Width * (int)scale.X, idleTexture.Height * (int)scale.Y), position)
        {
            this.buttonText = buttonText;
            this.textFont = textFont;
            this.idleTex = idleTexture;
            this.inFocusTex = null;
            this.idleColor = idleColour;
            this.inFocusColor = inFocusColour;

            this.textMidPoint = textFont.MeasureString(buttonText) / 2;
            this.textPos = new Vector2(position.X + idleTex.Width * 2, position.Y + idleTex.Height * 2);
            this.textScale = new Vector2(1, 1);

            this.scale = scale;

            this.og_Position = position;
            this.og_Bounds = _bounds;
        }

        public void MoveButton(Vector2 newPos)
        {
            position = newPos;
            _bounds = new Rectangle((int)newPos.X, (int)newPos.Y, _bounds.Width, _bounds.Height);
            Reset();
        }
        public void Reset()
        {
            isFocused = false;
            IsClicked = false;
        }

        public void Update(MouseState oldMouseState, MouseState newMouseState)
        {
            if (CheckIfInBounds(newMouseState.Position))
            {
                isFocused = true;
            }
            else
            {
                isFocused = false;
            }

            if (isFocused && newMouseState.LeftButton == ButtonState.Pressed && oldMouseState.LeftButton == ButtonState.Released)
            {
                OnClick();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (inFocusColor != idleColor)
            {
                if (isFocused)
                {
                    spriteBatch.Draw(idleTex, _bounds, this.inFocusColor);
                }
                else
                {
                    spriteBatch.Draw(idleTex, _bounds, this.idleColor);
                }
                // Draw the Text
                spriteBatch.DrawString(textFont, buttonText, textPos, Color.Black, 0, textMidPoint, textScale, SpriteEffects.None, 1F);
            }
            else if (inFocusTex != null)
            {
                if (isFocused)
                {
                    spriteBatch.Draw(inFocusTex, position, this.idleColor);
                }
                else
                {
                    spriteBatch.Draw(idleTex, position, this.idleColor);
                }
                // Draw the Text
                spriteBatch.DrawString(textFont, buttonText, position, Color.White, 0, textMidPoint, textScale, SpriteEffects.None, 1F);
            }

        }
    }
}
