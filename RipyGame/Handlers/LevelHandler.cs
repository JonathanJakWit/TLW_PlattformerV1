using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Managers;
using TLW_Plattformer.RipyGame.Models;

namespace TLW_Plattformer.RipyGame.Handlers
{
    public class LevelHandler
    {
        private Texture2D backgroundTex;
        private Texture2D levelTilesetTex;
        private Level level;

        public LevelHandler(TextureManager textureManager, AnimationManager animationManager)
        {
            this.backgroundTex = textureManager.LevelOneBackgroundTex;
            this.levelTilesetTex = textureManager.LevelOneTilesetTex;

            this.level = new Level(1);
        }

        public void Update(GameTime gameTime)
        {
            level.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTex, GameValues.LevelBounds, Color.White);
            level.Draw(spriteBatch, levelTilesetTex);
        }

    }
}
