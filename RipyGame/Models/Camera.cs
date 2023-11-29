using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Camera
    {
        public Matrix Transform { get; private set; }
        private float dx;
        private float dy;
        private float old_dx;
        private float old_dy;
        
        //private float followSpeed = 2F;
        public bool IsUpdated { get; set; } 
        public MoveableDirections CurrentDirection { get; private set; }

        public Camera()
        {
            dx = 1;
            dy = 1;
            old_dx = 0;
            old_dy = 0;
            IsUpdated = false;
            CurrentDirection = MoveableDirections.Idle;
        }


        public void Update(GameObject target)
        {
            dx = GameValues.WindowCenter.X - target.Position.X;
            //dx = MathHelper.Clamp(dx, -GameValues.LevelBounds.Width + GameValues.WindowSize.X, GameValues.ColumnWidth / 2);
            dx = MathHelper.Clamp(dx, -GameValues.LevelBounds.Width + GameValues.WindowSize.X - (GameValues.ColumnWidth), GameValues.ColumnWidth / 8);

            dy = GameValues.WindowCenter.Y - target.Position.Y;
            //dy = MathHelper.Clamp(dy, -GameValues.LevelBounds.Height + GameValues.WindowSize.Y, GameValues.RowHeight / 2);
            dy = MathHelper.Clamp(dy, -GameValues.LevelBounds.Height + GameValues.WindowSize.Y - (GameValues.RowHeight), GameValues.RowHeight / 8);
            Transform = Matrix.CreateTranslation(dx, dy, 0F);

            if (!IsUpdated && old_dx == dx && old_dy == dy)
            {
                IsUpdated = false;
                CurrentDirection = MoveableDirections.Idle;
            }
            else
            {
                IsUpdated = true;
                if (old_dx < dx)
                {
                    CurrentDirection = MoveableDirections.Left;
                }
                else if (old_dx > dx)
                {
                    CurrentDirection = MoveableDirections.Right;
                }
                old_dx = dx;
                old_dy = dy;
            }
        }
    }
}
