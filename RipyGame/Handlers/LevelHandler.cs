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

        public int CurrentLevelIndex {  get; private set; }

        //private LoadLevelManager loadLevelManager;
        public Level CurrentLevel {  get; private set; }

        public GameStates LevelGameState { get; set; }

        public LevelHandler(TextureManager textureManager, AnimationManager animationManager, int levelIndex)
        {
            this.backgroundTex = textureManager.LevelOneBackgroundTex;
            this.levelTilesetTex = textureManager.LevelOneTilesetTex;

            this.CurrentLevelIndex = levelIndex;

            //this.loadLevelManager = new LoadLevelManager(GamePaths.LevelOneDataPath, animationManager);
            //this.CurrentLevel = new Level(levelIndex, loadLevelManager);

            //LoadedLevel.Load(GamePaths.LevelOneDataPath, animationManager);
            this.CurrentLevel = new Level(levelIndex, animationManager);

            this.LevelGameState = GameStates.Playing;
        }

        public void ResetLevel()
        {

        }

        public void ChangeLevelTo(int levelIndex)
        {

        }
        public void ChangeLevelBy(int increment)
        {
            ChangeLevelTo(CurrentLevelIndex + increment);
        }

        public int GetHighestPlayerScore()
        {
            int hPS = 0;
            foreach (Player player in CurrentLevel.Players)
            {
                if (player.Score > hPS)
                {
                    hPS = player.Score;
                }
            }
            return hPS;
        }

        public int GetLowestPlayerHealth()
        {
            int lPH = 100;
            foreach (Player player in CurrentLevel.Players)
            {
                if (player.Health < lPH)
                {
                    lPH = player.Health;
                }
            }
            return lPH;
        }

        public void Update(GameTime gameTime)
        {
            CurrentLevel.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(backgroundTex, GameValues.LevelBounds, Color.White);
            CurrentLevel.Draw(spriteBatch, levelTilesetTex);
        }

    }
}
