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
    public class Level
    {
        public Level() { }

        //private void UpdateCollisions()
        //{
        //    foreach (Player player in LoadedGameLevel.Players)
        //    {
        //        //player.UpdateAllowedDirections(Plattforms);
        //        //player.TestFreePlayer();

        //        //if (player.IsGrounded)
        //        //{
        //        //continue;
        //        //}
        //        //foreach (Plattform plattform in Plattforms)
        //        //{
        //        //    if (player.Bounds.Intersects(plattform.Bounds))
        //        //    {
        //        //        player.IsGrounded = true;
        //        //    }
        //        //}
        //    }
        //}

        public void Update(GameTime gameTime)
        {
            //UpdateCollisions();

            //foreach (Item item in LoadedGameLevel.Items)
            //{
            //    foreach (Player player in LoadedGameLevel.Players)
            //    {
            //        if (item.IsPlayerCollision(player))
            //        {
            //            player.HandleItemInteraction(item);
            //        }
            //    }
            //    item.Update(gameTime);
            //}

            //foreach (Player player in LoadedGameLevel.Players)
            //{
            //    player.UpdatePlayer(gameTime);
            //}
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D levelTileset)
        {

            //foreach (Plattform plattform in LoadedGameLevel.Plattforms)
            //{
            //    plattform.Draw(spriteBatch, levelTileset);
            //}

            //foreach (Item item in LoadedGameLevel.Items)
            //{
            //    item.Draw(spriteBatch);
            //}

            //foreach (Player player in LoadedGameLevel.Players)
            //{
            //    player.Draw(spriteBatch);
            //}
        }
    }
}
