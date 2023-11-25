using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Models
{
    public class ParallaxBG
    {
        private Texture2D farBG;
        private Texture2D middleBG;
        private Texture2D nearBG;

        private Vector2 farPos1;
        private Vector2 middlePos1;
        private Vector2 nearPos1;
        private Vector2 farPos2;
        private Vector2 middlePos2;
        private Vector2 nearPos2;

        private Vector2 farSpeed;
        private Vector2 middleSpeed;
        private Vector2 nearSpeed;

        private float farDrawLayer;
        private float middleDrawLayer;
        private float nearDrawLayer;

        Vector2 farDistanceTraveled;
        Vector2 middleDistanceTraveled;
        Vector2 nearDistanceTraveled;

        private bool isFarPushed;
        private bool isMiddlePushed;
        private bool isNearPushed;

        public ParallaxBG(Texture2D farTex, Texture2D middleTex, Texture2D nearTex, float drawLayer, float farSpeed, float middleSpeed, float nearSpeed)
        {
            farBG = farTex;
            middleBG = middleTex;
            nearBG = nearTex;

            farPos1 = new Vector2(0, 0);
            middlePos1 = new Vector2(0, 0);
            nearPos1 = new Vector2(0, 0);
            farPos2 = new Vector2(farBG.Width, 0);
            middlePos2 = new Vector2(middleBG.Width, 0);
            nearPos2 = new Vector2(nearBG.Width, 0);

            farDrawLayer = drawLayer;
            middleDrawLayer = farDrawLayer + 0.01F;
            nearDrawLayer = middleDrawLayer + 0.01F;

            this.farSpeed = new Vector2(farSpeed, 0);
            this.middleSpeed = new Vector2(middleSpeed, 0);
            this.nearSpeed = new Vector2(nearSpeed, 0);

            this.farDistanceTraveled = new Vector2(0, 0);
            this.middleDistanceTraveled = new Vector2(0, 0);
            this.nearDistanceTraveled = new Vector2(0, 0);

            this.isFarPushed = false;
            this.isMiddlePushed = false;
            this.isNearPushed = false;
        }

        private void MoveLeft()
        {
            farPos1 -= farSpeed;
            farPos2 -= farSpeed;
            middlePos1 -= middleSpeed;
            middlePos2 -= middleSpeed;
            nearPos1 -= nearSpeed;
            nearPos2 -= nearSpeed;

            farDistanceTraveled -= farSpeed;
            middleDistanceTraveled -= middleSpeed;
            nearDistanceTraveled -= nearSpeed;
        }
        private void MoveRight()
        {
            farPos1 += farSpeed;
            farPos2 += farSpeed;
            middlePos1 += middleSpeed;
            middlePos2 += middleSpeed;
            nearPos1 += nearSpeed;
            nearPos2 += nearSpeed;

            farDistanceTraveled += farSpeed;
            middleDistanceTraveled += middleSpeed;
            nearDistanceTraveled += nearSpeed;
        }

        #region Potential
        //private MoveableDirections IsOutOfBounds(Vector2 pos, int width, Camera cam)
        //{
        //    if (pos.X < cam.Transform.Translation.X + width)
        //    {
        //        return MoveableDirections.Left;
        //    }
        //    else if (pos.X > cam.Transform.Translation.X + GameValues.WindowCenter.X)
        //    {
        //        return MoveableDirections.Right;
        //    }
        //    return MoveableDirections.Idle;
        //}
        //private void UpdatePositions(Camera camera)
        //{
        //    MoveableDirections fp1Dir = IsOutOfBounds(farPos1, farBG.Width, camera);
        //    MoveableDirections fp2Dir = IsOutOfBounds(farPos2, farBG.Width, camera);
        //    MoveableDirections mp1Dir = IsOutOfBounds(middlePos1, middleBG.Width, camera);
        //    //MoveableDirections mp2Dir = IsOutOfBounds(middlePos2, camera);
        //    //MoveableDirections np1Dir = IsOutOfBounds(nearPos1, camera);
        //    //MoveableDirections np2Dir = IsOutOfBounds(nearPos2, camera);

        //    if (fp1Dir != MoveableDirections.Idle)
        //    {
        //        if (fp1Dir == MoveableDirections.Left)
        //        {
        //            farPos1 = new Vector2(farPos1.X + farBG.Width, farPos1.Y);
        //        }
        //        else if (fp1Dir == MoveableDirections.Right)
        //        {
        //            farPos1 = new Vector2(farPos1.X - farBG.Width, farPos1.Y);
        //        }
        //    }
        //}
        #endregion Potential

        private void UpdatePositions()
        {
            //if (isPushed)
            //{
            //    return;
            //}

            if (farDistanceTraveled.X > farBG.Width / (GameValues.PlayerBounds.Width / 4))
            {
                if (!isFarPushed)
                {
                    farPos1 = new Vector2(farPos2.X + farBG.Width, 0);
                    isFarPushed = true;
                }
                else
                {
                    farPos2 = new Vector2(farPos1.X + farBG.Width, 0);
                    isFarPushed = false;
                }
                farDistanceTraveled = new Vector2(0, 0);
                //isPushed = true;
            }
            if (middleDistanceTraveled.X > middleBG.Width / (GameValues.PlayerBounds.Width / 8))
            {
                if (!isMiddlePushed)
                {
                    middlePos1 = new Vector2(middlePos2.X + middleBG.Width, 0);
                    isMiddlePushed = true;
                }
                else
                {
                    middlePos2 = new Vector2(middlePos1.X + middleBG.Width, 0);
                    isMiddlePushed = false;
                }
                middleDistanceTraveled = new Vector2(0, 0);
            }
            if (nearDistanceTraveled.X > nearBG.Width / (GameValues.PlayerBounds.Width / 16))
            {
                if (!isNearPushed)
                {
                    nearPos1 = new Vector2(nearPos2.X + nearBG.Width, 0);
                    isNearPushed = true;
                }
                else
                {
                    nearPos2 = new Vector2(nearPos1.X + nearBG.Width, 0);
                    isNearPushed = false;
                }
                nearDistanceTraveled = new Vector2(0, 0);
            }


            Debug.WriteLine("fP1: " + farPos1.ToString());
            //Debug.WriteLine("farDistanceT: " + farDistanceTraveled.ToString());
            //Debug.WriteLine("farBGW: " + (farBG.Width / (GameValues.PlayerBounds.Width / 2)).ToString());
        }

        public void Update(Camera camera)
        {
            if (camera.IsUpdated)
            {
                if (camera.CurrentDirection == MoveableDirections.Left)
                {
                    MoveLeft();
                }
                else if (camera.CurrentDirection == MoveableDirections.Right)
                {
                    MoveRight();
                }
                camera.IsUpdated = false;
                UpdatePositions();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(farBG, farPos1, Color.White);
            spriteBatch.Draw(farBG, farPos2, Color.White);
            spriteBatch.Draw(middleBG, middlePos1, Color.White);
            spriteBatch.Draw(middleBG, middlePos2, Color.White);
            spriteBatch.Draw(nearBG, nearPos1, Color.White);
            spriteBatch.Draw(nearBG, nearPos2, Color.White);
        }
    }
}
