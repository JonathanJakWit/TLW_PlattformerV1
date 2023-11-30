using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
        public Camera LevelCamera {  get; set; }
        private ParallaxBG parallaxBG;
        private Texture2D levelTilesetTex;
        private Texture2D plattformTilesetTex;

        public int CurrentLevelIndex {  get; private set; }
        public GameStates LevelGameState { get; set; }

        private LevelEditor levelEditor;
        private bool isEditing;

        public LevelHandler(TextureManager textureManager, AnimationManager animationManager, int levelIndex)
        {
            this.parallaxBG = GetParallaxBG(textureManager);
            this.levelTilesetTex = textureManager.LevelOneTilesetTex;
            this.plattformTilesetTex = textureManager.StonePlattformTilesetTex;

            this.CurrentLevelIndex = levelIndex;
            this.LevelGameState = GameStates.Playing;

            if (CurrentLevelIndex == 1)
            {
                //LoadedGameLevel.Load(animationManager, textureManager); // From Code Value Levels
                //LoadedGameLevel.LoadLevel(GamePaths.JsonLevelDataFilesPath + "test_level_data.json", animationManager, textureManager);
                LoadedGameLevel.LoadLevel(GamePaths.JsonLevelOneDataPath, animationManager, textureManager);
            }
            else
            {
                throw new Exception("Didnt start at lvl 1, wtf");
            }

            this.LevelCamera = new Camera();
            this.levelEditor = new LevelEditor(textureManager, animationManager);
            this.isEditing = false;
        }

        private ParallaxBG GetParallaxBG(TextureManager textureManager)
        {
            float farSpeed = 1F;
            float middleSpeed = 2F;
            float nearSpeed = 3F;

            ParallaxBG newPxBG = new ParallaxBG(textureManager.LevelOneFarBackgroundTex, textureManager.LevelOneMiddleBackgroundTex, textureManager.LevelOneNearBackgroundTex,
                GameValues.MapDrawLayer, farSpeed, middleSpeed, nearSpeed);
            return newPxBG;
        }

        public void ResetLevel(AnimationManager animationManager, TextureManager textureManager)
        {
            ChangeLevelTo(1, animationManager, textureManager);
        }

        public void ChangeLevelTo(int levelIndex, AnimationManager animationManager, TextureManager textureManager)
        {
            LoadedGameLevel.UnloadLevel();

            if (levelIndex == 1)
            {
                LoadedGameLevel.LoadLevel(GamePaths.JsonLevelOneDataPath, animationManager, textureManager);
            }
            else if (levelIndex == 2)
            {
                LoadedGameLevel.LoadLevel(GamePaths.JsonLevelTwoDataPath, animationManager, textureManager);
            }
            else
            {
                LoadedGameLevel.LoadLevel(GamePaths.JsonLevelOneDataPath, animationManager, textureManager);
            }
            CurrentLevelIndex = levelIndex;
        }
        public void ChangeLevelBy(int increment, AnimationManager animationManager, TextureManager textureManager)
        {
            ChangeLevelTo(CurrentLevelIndex + increment, animationManager, textureManager);
        }

        public int GetHighestPlayerScore()
        {
            int hPS = 0;
            foreach (Player gameObject in LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer))
            {
                if (gameObject is Player)
                {
                    if (gameObject.Score > hPS)
                    {
                        hPS = gameObject.Score;
                    }
                }
            }
            return hPS;
        }

        public int GetLowestPlayerHealth()
        {
            int lPH = 100;
            foreach (Player player in LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer))
            {
                if (player.Health < lPH)
                {
                    lPH = player.Health;
                }
            }
            return lPH;
        }

        public bool AllPlayersDead()
        {
            bool allAreDead = true;
            foreach (Player player in LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer))
            {
                if (player.IsAlive)
                {
                    allAreDead = false;
                }
            }

            return allAreDead;
        }

        public void Update(GameTime gameTime)
        {
            if (GameValues.IsKeyPressed(Keys.Enter))
            {
                isEditing = !isEditing;
            }

            if (isEditing)
            {
                LevelCamera.Update(levelEditor.cameraTarget);
                parallaxBG.Update(LevelCamera);
                levelEditor.Update();
            }
            else
            {
                LevelCamera.Update(LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer)[0]);
                parallaxBG.Update(LevelCamera);
                LoadedGameLevel.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            parallaxBG.Draw(spriteBatch);
            LoadedGameLevel.Draw(spriteBatch, levelTilesetTex, plattformTilesetTex);
            if (isEditing)
            {
                levelEditor.Draw(spriteBatch);
            }
        }

    }
}
