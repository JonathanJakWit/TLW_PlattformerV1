using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Models
{
    public class FreePlayer : GameObject
    {
        private int speed;

        public FreePlayer()
        {
            Position = GameValues.WindowCenter;
            this.speed = 10;
        }

        private void MoveLeft()
        {
            Position = new Vector2(Position.X - speed, Position.Y);
        }
        private void MoveRight()
        {
            Position = new Vector2(Position.X + speed, Position.Y);
        }

        public void Update()
        {
            if (GameValues.NewKeyboardState.IsKeyDown(Keys.Left))
            {
                MoveLeft();
            }
            else if (GameValues.NewKeyboardState.IsKeyDown(Keys.Right))
            {
                MoveRight();
            }
        }
    }
}
