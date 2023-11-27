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
    #region V2
    public class ParallaxBG
    {
        private Texture2D farBG;
        private Texture2D middleBG;
        private Texture2D nearBG;

        private List<Rectangle> farDestRects;
        private List<Rectangle> farSourceRects;
        private List<Rectangle> middleDestRects;
        private List<Rectangle> middleSourceRects;
        private List<Rectangle> nearDestRects;
        private List<Rectangle> nearSourceRects;

        private Vector2 farSpeed;
        private Vector2 middleSpeed;
        private Vector2 nearSpeed;

        private float farDrawLayer;
        private float middleDrawLayer;
        private float nearDrawLayer;

        Vector2 farDistanceTraveled;
        Vector2 middleDistanceTraveled;
        Vector2 nearDistanceTraveled;

        private int splitRectAmount;
        private int rectDivisionSize;

        public ParallaxBG(Texture2D farTex, Texture2D middleTex, Texture2D nearTex, float drawLayer, float farSpeed, float middleSpeed, float nearSpeed)
        {
            farBG = farTex;
            middleBG = middleTex;
            nearBG = nearTex;

            this.splitRectAmount = 5;
            this.rectDivisionSize = farBG.Width * (int)GameValues.TileScale.X / splitRectAmount;

            this.farDestRects = new List<Rectangle>();
            this.farSourceRects = new List<Rectangle>();
            this.middleDestRects = new List<Rectangle>();
            this.middleSourceRects = new List<Rectangle>();
            this.nearDestRects = new List<Rectangle>();
            this.nearSourceRects = new List<Rectangle>();

            for (int i = 0; i < splitRectAmount; i++)
            {
                farSourceRects.Add(new Rectangle(farBG.Width / splitRectAmount * i, 0, farBG.Width / splitRectAmount, farBG.Height));
                middleSourceRects.Add(new Rectangle(middleBG.Width / splitRectAmount * i, 0, middleBG.Width / splitRectAmount, middleBG.Height));
                nearSourceRects.Add(new Rectangle(nearBG.Width / splitRectAmount * i, 0, nearBG.Width / splitRectAmount, nearBG.Height));

                farDestRects.Add(new Rectangle(farBG.Width / splitRectAmount * i, 0, farBG.Width / splitRectAmount, farBG.Height));
                middleDestRects.Add(new Rectangle(middleBG.Width / splitRectAmount * i, 0, middleBG.Width / splitRectAmount, middleBG.Height));
                nearDestRects.Add(new Rectangle(nearBG.Width / splitRectAmount * i, 0, nearBG.Width / splitRectAmount, nearBG.Height));
            }
            for (int i = 0; i < splitRectAmount; i++)
            {
                farSourceRects.Add(new Rectangle(farBG.Width / splitRectAmount * i, 0, farBG.Width / splitRectAmount, farBG.Height));
                middleSourceRects.Add(new Rectangle(middleBG.Width / splitRectAmount * i, 0, middleBG.Width / splitRectAmount, middleBG.Height));
                nearSourceRects.Add(new Rectangle(nearBG.Width / splitRectAmount * i, 0, nearBG.Width / splitRectAmount, nearBG.Height));

                farDestRects.Add(new Rectangle(farBG.Width + farBG.Width / splitRectAmount * i, 0, farBG.Width / splitRectAmount, farBG.Height));
                middleDestRects.Add(new Rectangle(middleBG.Width + middleBG.Width / splitRectAmount * i, 0, middleBG.Width / splitRectAmount, middleBG.Height));
                nearDestRects.Add(new Rectangle(nearBG.Width + nearBG.Width / splitRectAmount * i, 0, nearBG.Width / splitRectAmount, nearBG.Height));
            }

            farDrawLayer = drawLayer;
            middleDrawLayer = farDrawLayer + 0.01F;
            nearDrawLayer = middleDrawLayer + 0.01F;

            this.farSpeed = new Vector2(farSpeed, 0);
            this.middleSpeed = new Vector2(middleSpeed, 0);
            this.nearSpeed = new Vector2(nearSpeed, 0);

            this.farDistanceTraveled = new Vector2(0, 0);
            this.middleDistanceTraveled = new Vector2(0, 0);
            this.nearDistanceTraveled = new Vector2(0, 0);
        }

        private void MoveLeft()
        {
            farDistanceTraveled -= farSpeed;
            middleDistanceTraveled -= middleSpeed;
            nearDistanceTraveled -= nearSpeed;

            for (int i = 0; i < farDestRects.Count; i++)
            {
                farDestRects[i] = new Rectangle(farDestRects[i].X - (int)farSpeed.X, farDestRects[i].Y, farDestRects[i].Width, farDestRects[i].Height);
            }
            for (int i = 0; i < middleDestRects.Count; i++)
            {
                middleDestRects[i] = new Rectangle(middleDestRects[i].X - (int)middleSpeed.X, middleDestRects[i].Y, middleDestRects[i].Width, middleDestRects[i].Height);
            }
            for (int i = 0; i < nearDestRects.Count; i++)
            {
                nearDestRects[i] = new Rectangle(nearDestRects[i].X - (int)nearSpeed.X, nearDestRects[i].Y, nearDestRects[i].Width, nearDestRects[i].Height);
            }

            // Add move left checks
        }
        private void MoveRight()
        {
            farDistanceTraveled += farSpeed;
            middleDistanceTraveled += middleSpeed;
            nearDistanceTraveled += nearSpeed;

            for (int i = 0; i < farDestRects.Count; i++)
            {
                farDestRects[i] = new Rectangle(farDestRects[i].X + (int)farSpeed.X, farDestRects[i].Y, farDestRects[i].Width, farDestRects[i].Height);
            }
            for (int i = 0; i < middleDestRects.Count; i++)
            {
                middleDestRects[i] = new Rectangle(middleDestRects[i].X + (int)middleSpeed.X, middleDestRects[i].Y, middleDestRects[i].Width, middleDestRects[i].Height);
            }
            for (int i = 0; i < nearDestRects.Count; i++)
            {
                nearDestRects[i] = new Rectangle(nearDestRects[i].X + (int)nearSpeed.X, nearDestRects[i].Y, nearDestRects[i].Width, nearDestRects[i].Height);
            }

            if (farDistanceTraveled.X >= farDestRects[0].Width / rectDivisionSize)
            {
                farDestRects.Add(new Rectangle(farDestRects[farDestRects.Count - 1].X + farDestRects[0].Width,
                    farDestRects[farDestRects.Count - 1].Y, farDestRects[0].Width, farDestRects[0].Height));
                //farDestRects.RemoveAt(0);
                farDistanceTraveled = new Vector2(0, 0);
            }
            if (middleDistanceTraveled.X >= middleDestRects[0].Width / rectDivisionSize)
            {
                middleDestRects.Add(new Rectangle(middleDestRects[middleDestRects.Count - 1].X + middleDestRects[0].Width,
                    middleDestRects[middleDestRects.Count - 1].Y, middleDestRects[0].Width, middleDestRects[0].Height));
                //middleDestRects.RemoveAt(0);
                middleDistanceTraveled = new Vector2(0, 0);
            }
            if (nearDistanceTraveled.X >= nearDestRects[0].Width / rectDivisionSize)
            {
                nearDestRects.Add(new Rectangle(nearDestRects[nearDestRects.Count - 1].X + nearDestRects[0].Width,
                    nearDestRects[nearDestRects.Count - 1].Y, nearDestRects[0].Width, nearDestRects[0].Height));
                //nearDestRects.RemoveAt(0);
                nearDistanceTraveled = new Vector2(0, 0);
            }

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
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int fsrI = 0;
            for (int i = 0; i < farDestRects.Count; i++)
            {
                if (fsrI >= farSourceRects.Count)
                {
                    fsrI = 0;
                }
                spriteBatch.Draw(farBG, farDestRects[i], farSourceRects[fsrI], Color.White);
                fsrI++;

            }

            int msrI = 0;
            for (int i = 0; i < middleDestRects.Count; i++)
            {
                if (msrI >= middleSourceRects.Count)
                {
                    msrI = 0;
                }
                spriteBatch.Draw(middleBG, middleDestRects[i], middleSourceRects[msrI], Color.White);
                msrI++;
            }

            int nsrI = 0;
            for (int i = 0; i < nearDestRects.Count; i++)
            {
                if (nsrI >= nearSourceRects.Count)
                {
                    nsrI = 0;
                }
                spriteBatch.Draw(nearBG, nearDestRects[i], nearSourceRects[nsrI], Color.White);
                nsrI++;
            }
        }
    }
    #endregion V2

    #region V1
    //public class ParallaxBG
    //{
    //    private Texture2D farBG;
    //    private Texture2D middleBG;
    //    private Texture2D nearBG;

    //    private Vector2 farPos1;
    //    private Vector2 middlePos1;
    //    private Vector2 nearPos1;
    //    private Vector2 farPos2;
    //    private Vector2 middlePos2;
    //    private Vector2 nearPos2;

    //    private Vector2 farSpeed;
    //    private Vector2 middleSpeed;
    //    private Vector2 nearSpeed;

    //    private float farDrawLayer;
    //    private float middleDrawLayer;
    //    private float nearDrawLayer;

    //    Vector2 farDistanceTraveled;
    //    Vector2 middleDistanceTraveled;
    //    Vector2 nearDistanceTraveled;

    //    private bool isFarPushed;
    //    private bool isMiddlePushed;
    //    private bool isNearPushed;

    //    public ParallaxBG(Texture2D farTex, Texture2D middleTex, Texture2D nearTex, float drawLayer, float farSpeed, float middleSpeed, float nearSpeed)
    //    {
    //        farBG = farTex;
    //        middleBG = middleTex;
    //        nearBG = nearTex;

    //        farPos1 = new Vector2(0, 0);
    //        middlePos1 = new Vector2(0, 0);
    //        nearPos1 = new Vector2(0, 0);
    //        farPos2 = new Vector2(farBG.Width, 0);
    //        middlePos2 = new Vector2(middleBG.Width, 0);
    //        nearPos2 = new Vector2(nearBG.Width, 0);

    //        farDrawLayer = drawLayer;
    //        middleDrawLayer = farDrawLayer + 0.01F;
    //        nearDrawLayer = middleDrawLayer + 0.01F;

    //        this.farSpeed = new Vector2(farSpeed, 0);
    //        this.middleSpeed = new Vector2(middleSpeed, 0);
    //        this.nearSpeed = new Vector2(nearSpeed, 0);

    //        this.farDistanceTraveled = new Vector2(0, 0);
    //        this.middleDistanceTraveled = new Vector2(0, 0);
    //        this.nearDistanceTraveled = new Vector2(0, 0);

    //        this.isFarPushed = false;
    //        this.isMiddlePushed = false;
    //        this.isNearPushed = false;
    //    }

    //    private void MoveLeft()
    //    {
    //        farPos1 -= farSpeed;
    //        farPos2 -= farSpeed;
    //        middlePos1 -= middleSpeed;
    //        middlePos2 -= middleSpeed;
    //        nearPos1 -= nearSpeed;
    //        nearPos2 -= nearSpeed;

    //        farDistanceTraveled -= farSpeed;
    //        middleDistanceTraveled -= middleSpeed;
    //        nearDistanceTraveled -= nearSpeed;
    //    }
    //    private void MoveRight()
    //    {
    //        farPos1 += farSpeed;
    //        farPos2 += farSpeed;
    //        middlePos1 += middleSpeed;
    //        middlePos2 += middleSpeed;
    //        nearPos1 += nearSpeed;
    //        nearPos2 += nearSpeed;

    //        farDistanceTraveled += farSpeed;
    //        middleDistanceTraveled += middleSpeed;
    //        nearDistanceTraveled += nearSpeed;
    //    }

    //    #region Potential
    //    //private MoveableDirections IsOutOfBounds(Vector2 pos, int width, Camera cam)
    //    //{
    //    //    if (pos.X < cam.Transform.Translation.X + width)
    //    //    {
    //    //        return MoveableDirections.Left;
    //    //    }
    //    //    else if (pos.X > cam.Transform.Translation.X + GameValues.WindowCenter.X)
    //    //    {
    //    //        return MoveableDirections.Right;
    //    //    }
    //    //    return MoveableDirections.Idle;
    //    //}
    //    //private void UpdatePositions(Camera camera)
    //    //{
    //    //    MoveableDirections fp1Dir = IsOutOfBounds(farPos1, farBG.Width, camera);
    //    //    MoveableDirections fp2Dir = IsOutOfBounds(farPos2, farBG.Width, camera);
    //    //    MoveableDirections mp1Dir = IsOutOfBounds(middlePos1, middleBG.Width, camera);
    //    //    //MoveableDirections mp2Dir = IsOutOfBounds(middlePos2, camera);
    //    //    //MoveableDirections np1Dir = IsOutOfBounds(nearPos1, camera);
    //    //    //MoveableDirections np2Dir = IsOutOfBounds(nearPos2, camera);

    //    //    if (fp1Dir != MoveableDirections.Idle)
    //    //    {
    //    //        if (fp1Dir == MoveableDirections.Left)
    //    //        {
    //    //            farPos1 = new Vector2(farPos1.X + farBG.Width, farPos1.Y);
    //    //        }
    //    //        else if (fp1Dir == MoveableDirections.Right)
    //    //        {
    //    //            farPos1 = new Vector2(farPos1.X - farBG.Width, farPos1.Y);
    //    //        }
    //    //    }
    //    //}
    //    #endregion Potential

    //    private void UpdatePositions()
    //    {
    //        //if (isPushed)
    //        //{
    //        //    return;
    //        //}

    //        if (farDistanceTraveled.X > farBG.Width / (GameValues.PlayerBounds.Width / 4))
    //        {
    //            if (!isFarPushed)
    //            {
    //                farPos1 = new Vector2(farPos2.X + farBG.Width, 0);
    //                isFarPushed = true;
    //            }
    //            else
    //            {
    //                farPos2 = new Vector2(farPos1.X + farBG.Width, 0);
    //                isFarPushed = false;
    //            }
    //            farDistanceTraveled = new Vector2(0, 0);
    //            //isPushed = true;
    //        }
    //        if (middleDistanceTraveled.X > middleBG.Width / (GameValues.PlayerBounds.Width / 8))
    //        {
    //            if (!isMiddlePushed)
    //            {
    //                middlePos1 = new Vector2(middlePos2.X + middleBG.Width, 0);
    //                isMiddlePushed = true;
    //            }
    //            else
    //            {
    //                middlePos2 = new Vector2(middlePos1.X + middleBG.Width, 0);
    //                isMiddlePushed = false;
    //            }
    //            middleDistanceTraveled = new Vector2(0, 0);
    //        }
    //        if (nearDistanceTraveled.X > nearBG.Width / (GameValues.PlayerBounds.Width / 16))
    //        {
    //            if (!isNearPushed)
    //            {
    //                nearPos1 = new Vector2(nearPos2.X + nearBG.Width, 0);
    //                isNearPushed = true;
    //            }
    //            else
    //            {
    //                nearPos2 = new Vector2(nearPos1.X + nearBG.Width, 0);
    //                isNearPushed = false;
    //            }
    //            nearDistanceTraveled = new Vector2(0, 0);
    //        }
    //    }

    //    public void Update(Camera camera)
    //    {
    //        if (camera.IsUpdated)
    //        {
    //            if (camera.CurrentDirection == MoveableDirections.Left)
    //            {
    //                MoveLeft();
    //            }
    //            else if (camera.CurrentDirection == MoveableDirections.Right)
    //            {
    //                MoveRight();
    //            }
    //            camera.IsUpdated = false;
    //            UpdatePositions();
    //        }
    //    }

    //    public void Draw(SpriteBatch spriteBatch)
    //    {
    //        spriteBatch.Draw(farBG, farPos1, Color.White);
    //        spriteBatch.Draw(farBG, farPos2, Color.White);
    //        spriteBatch.Draw(middleBG, middlePos1, Color.White);
    //        spriteBatch.Draw(middleBG, middlePos2, Color.White);
    //        spriteBatch.Draw(nearBG, nearPos1, Color.White);
    //        spriteBatch.Draw(nearBG, nearPos2, Color.White);
    //    }
    //}
    #endregion V1
}
