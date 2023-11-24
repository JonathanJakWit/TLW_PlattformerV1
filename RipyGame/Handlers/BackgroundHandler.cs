using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Handlers
{
    public static class BackgroundHandler
    {
        private static Texture2D backgroundSource;
        private static int sourceRectWidth;
        private static int sourceRectHeight;
        private static List<Vector2> destPositions;
        private static List<Rectangle> sourceRects;
        private static Vector2 totalDistanceTraveled;

        public static void LoadBackground(Texture2D background)
        {
            backgroundSource = background;

            sourceRectWidth = backgroundSource.Width / 7;
            sourceRectHeight = backgroundSource.Height;
            
            destPositions = new List<Vector2>();
            destPositions.Add(new Vector2(0, 0));
            destPositions.Add(new Vector2(sourceRectWidth * 1, 0));
            destPositions.Add(new Vector2(sourceRectWidth * 2, 0));
            destPositions.Add(new Vector2(sourceRectWidth * 3, 0));
            destPositions.Add(new Vector2(sourceRectWidth * 4, 0));
            destPositions.Add(new Vector2(sourceRectWidth * 5, 0));
            destPositions.Add(new Vector2(sourceRectWidth * 6, 0));

            sourceRects = new List<Rectangle>();
            sourceRects.Add(new Rectangle(0, 0, sourceRectWidth, sourceRectHeight));
            sourceRects.Add(new Rectangle(sourceRectWidth * 1, 0, sourceRectWidth, sourceRectHeight));
            sourceRects.Add(new Rectangle(sourceRectWidth * 2, 0, sourceRectWidth, sourceRectHeight));
            sourceRects.Add(new Rectangle(sourceRectWidth * 3, 0, sourceRectWidth, sourceRectHeight));
            sourceRects.Add(new Rectangle(sourceRectWidth * 4, 0, sourceRectWidth, sourceRectHeight));
            sourceRects.Add(new Rectangle(sourceRectWidth * 5, 0, sourceRectWidth, sourceRectHeight));
            sourceRects.Add(new Rectangle(sourceRectWidth * 6, 0, sourceRectWidth, sourceRectHeight));

            totalDistanceTraveled = new Vector2(0, 0);
        }

        private static void UpdateAndCheckDistance(Vector2 alignment)
        {
            totalDistanceTraveled += alignment;
            if (totalDistanceTraveled.X > sourceRectWidth * 2)
            {
                destPositions.Add(new Vector2(destPositions[destPositions.Count - 1].X + sourceRectWidth, 0));
                destPositions.RemoveAt(0);
                destPositions.Add(new Vector2(destPositions[destPositions.Count - 1].X + sourceRectWidth, 0));
                destPositions.RemoveAt(0);
                totalDistanceTraveled = Vector2.Zero;
            }
        }

        public static void MoveBy(Vector2 distance)
        {
            UpdateAndCheckDistance(distance);

            for (int i = 0; i < destPositions.Count; i++)
            {
                destPositions[i] = destPositions[i] - distance;
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < destPositions.Count; i++)
            {
                spriteBatch.Draw(backgroundSource, destPositions[i], sourceRects[i], Color.White);
            }

            //spriteBatch.Draw(backgroundSource, destPositions[0], sourceRects[0], Color.White);
            //spriteBatch.Draw(backgroundSource, destPositions[1], sourceRects[1], Color.White);
            //spriteBatch.Draw(backgroundSource, destPositions[2], sourceRects[2], Color.White);
            //spriteBatch.Draw(backgroundSource, destPositions[3], sourceRects[3], Color.White);
            //spriteBatch.Draw(backgroundSource, destPositions[4], sourceRects[4], Color.White);
            //spriteBatch.Draw(backgroundSource, destPositions[5], sourceRects[5], Color.White);
            //spriteBatch.Draw(backgroundSource, destPositions[6], sourceRects[6], Color.White);
        }
    }
}
