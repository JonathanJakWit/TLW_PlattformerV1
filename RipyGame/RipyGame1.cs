using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Managers;
using TLW_Plattformer.RipyGame.Handlers;
using TLW_Plattformer.RipyGame.Models;

namespace TLW_Plattformer.RipyGame
{
    public class RipyGame1
    {
        public bool FinishedPlaying { get; set; }
        private ContentManager Content { get; set; }
        private TextureManager _TextureManager { get; set; }
        private AnimationManager _AnimationManager { get; set; }
        private HighscoreManager _HighscoreManager { get; set; }
        private HudManager _HudManager { get; set; }
        private UIManager _UIManager { get; set; }

        private Texture2D _BackgroundTexture { get; set; }

        private LevelHandler _LevelHandler { get; set; }
        private GameStates CurrentGameState { get; set; }
        private int CurrentLevelIndex { get; set; }

        private MouseState oldMouseState { get; set; }
        private MouseState newMouseState { get; set; }

        private KeyboardState oldKeyboardState { get; set; }
        private KeyboardState newKeyboardState { get; set; }

        private bool isNewGameState { get; set; }
        private bool gameIsDone;
        private bool isPaused { get; set; }
        //private bool isDataSaved { get; set; }

        private int currentHighscore;
        //private string currentTilemapDataPath;

        public RipyGame1(ContentManager contentManager)
        {
            Content = contentManager;
            Initialize();

            this.FinishedPlaying = false;
            this.CurrentGameState = GameStates.MainMenu;
            this.CurrentLevelIndex = 1;

            this._TextureManager = new TextureManager(Content, CurrentLevelIndex);
            this._AnimationManager = new AnimationManager(Content, _TextureManager);
            this._HighscoreManager = new HighscoreManager(GamePaths.HighscoreDataPath);
            this.currentHighscore = _HighscoreManager.GetHighscore();
            //this._HudManager = new HudManager(_TextureManager, GameValues.HudStartPos, GameValues.HudEndPos, _HighscoreManager.GetHighscore());
            this._UIManager = new UIManager(_TextureManager, CurrentGameState);

            this._LevelHandler = new LevelHandler(_TextureManager, _AnimationManager, CurrentLevelIndex);

            this.oldMouseState = Mouse.GetState();
            this.newMouseState = oldMouseState;

            this.oldKeyboardState = Keyboard.GetState();
            this.newKeyboardState = oldKeyboardState;

            this.isNewGameState = true;
            this.isPaused = false;

            this._BackgroundTexture = _TextureManager.MainMenuBackgroundTex;
        }

        public void Initialize()
        {
            GamePaths.InitializePaths();
            GameValues.InitializeValues(Content);
        }

        private void SaveHighscoreData()
        {
            foreach (Player player in LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer))
            {
                _HighscoreManager.UpdateAndSetScores(player.Name, player.Score);
            }
        }
        private void SaveData(bool saveScore = true)
        {
            //if (saveScore)
            //if (false)
            //{
            //    // Save Highscores
            //    string pName = GetPlayerName();
            //    int pScore = _LevelHandler.CurrentPlayerScore;

            //    if (isDataSaved)
            //    {
            //        _HighscoreManager.UpdateAndSetScores(pName, pScore);
            //    }
            //}
            //else
            //{
            //    _HighscoreManager.UpdateAndSetScores(_LevelHandler.CurrentPlayerName, _LevelHandler.CurrentPlayerScore);
            //}

            
            SaveHighscoreData();
        }

        private void ResetLevel()
        {
            this.FinishedPlaying = false;
            this.CurrentGameState = GameStates.Playing;
            this.CurrentLevelIndex = 1;
            //this._UIManager.Reset(CurrentGameState);
            //this.currentTilemapDataPath = Paths.GetPath(Path.Level1TilemapDataFile);

            this._LevelHandler.ResetLevel(_AnimationManager, _TextureManager);

            this.oldMouseState = Mouse.GetState();
            this.newMouseState = oldMouseState;
            this.oldKeyboardState = Keyboard.GetState();
            this.newKeyboardState = oldKeyboardState;

            this.isNewGameState = true;
            this.gameIsDone = false;
        }

        private void TogglePause()
        {
            isPaused = !isPaused;
        }

        private void UpdateMenu(GameTime gameTime)
        {
            if (!isNewGameState)
            {
                _UIManager.GameState = CurrentGameState;
            }

            newMouseState = Mouse.GetState();
            _UIManager.Update(oldMouseState, newMouseState, CurrentGameState);
            if (_UIManager.GameState != CurrentGameState)
            {
                CurrentGameState = _UIManager.GameState;
                isNewGameState = true;
            }
            oldMouseState = newMouseState;

            if (_UIManager.IsNewGameState && isNewGameState)
            {
                CurrentGameState = _UIManager.GameState;
                if (CurrentGameState == GameStates.Playing)
                {
                    //BackgroundTexture = TextureManager.GetTexture(TextureTypes.LevelOneBackground);
                    // Paralax here or in the levelhandler?
                }
                else if (CurrentGameState == GameStates.Replaying)
                {
                    ResetLevel();
                }
                else if (CurrentGameState == GameStates.MainMenu)
                {
                    _BackgroundTexture = _TextureManager.MainMenuBackgroundTex;
                }
                else if (CurrentGameState == GameStates.HighscoreMenu)
                {
                    _BackgroundTexture = _TextureManager.HighscoreMenuBackgroundTex;
                    SaveHighscoreData();
                }
                else if (CurrentGameState == GameStates.GameOverWinMenu)
                {
                    _BackgroundTexture = _TextureManager.GameOverWinMenuBackgroundTex;
                }
                else if (CurrentGameState == GameStates.GameOverLossMenu)
                {
                    _BackgroundTexture = _TextureManager.GameOverLoseMenuBackgroundTex;
                }

                _UIManager.IsNewGameState = false;
                isNewGameState = false;
            }
        }
        private void UpdatePlaying(GameTime gameTime)
        {
            #region TestingPlaying
            if (oldKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.T) && newKeyboardState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.T))
            {
                // Code for testing by pressing T while playing
            }
            #endregion TestingPlaying

            if (GameValues.IsKeyPressed(Keys.L))
            {
                LoadedGameLevel.WriteLevel(GamePaths.JsonLevelDataFilesPath + "test_level_data.json");
            }

            _LevelHandler.Update(gameTime);
            if (_LevelHandler.GetHighestPlayerScore() > currentHighscore)
            {
                currentHighscore = _LevelHandler.GetHighestPlayerScore();
            }
            //_HudManager.Update(currentHighscore, _LevelHandler.CurrentPlayerScore);

            if (LoadedGameLevel.CurrentEnemyAmount <= 0) // Change false to [Win/Increase level condition]
            {
                if (_LevelHandler.CurrentLevelIndex == 2)
                {
                    gameIsDone = true;
                    _LevelHandler.LevelGameState = GameStates.GameOverWinMenu;
                }

                if (_LevelHandler.CurrentLevelIndex == 1)
                {
                    _LevelHandler.ChangeLevelBy(1, _AnimationManager, _TextureManager);
                }
            }
            else if (_LevelHandler.AllPlayersDead())
            {
                gameIsDone = true;
                _LevelHandler.LevelGameState = GameStates.GameOverLossMenu;
            }
            //else if (_LevelHandler.GetLowestPlayerHealth() <= 0)
            //{
            //    gameIsDone = true;
            //    _LevelHandler.LevelGameState = GameStates.GameOverLossMenu;
            //}
        }

        public void Update(GameTime gameTime)
        {
            newKeyboardState = Keyboard.GetState();
            if (oldKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q) && newKeyboardState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                //_LevelHandler.level.LevelMap.SaveMapData(Paths.GetPath(Path.EditedLevel1TilemapDataFile));
            }

            if (oldKeyboardState.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.P) && newKeyboardState.IsKeyUp(Microsoft.Xna.Framework.Input.Keys.P))
            {
                TogglePause();
            }

            if (gameIsDone)
            {
                SaveData();

                if (_LevelHandler.LevelGameState == GameStates.GameOverWinMenu)
                {
                    CurrentGameState = GameStates.GameOverWinMenu;
                    _BackgroundTexture = _TextureManager.GameOverWinMenuBackgroundTex;
                    gameIsDone = false;
                }
                else if (_LevelHandler.LevelGameState == GameStates.GameOverLossMenu)
                {
                    CurrentGameState = GameStates.GameOverLossMenu;
                    _BackgroundTexture = _TextureManager.GameOverLoseMenuBackgroundTex;
                    gameIsDone = false;
                }
            }

            if (!isPaused && CurrentGameState == GameStates.Playing)
            {
                UpdatePlaying(gameTime);
            }
            else if (CurrentGameState == GameStates.Quit)
            {
                SaveData(false);
                FinishedPlaying = true;
            }
            else
            {
                UpdateMenu(gameTime);
            }

            oldKeyboardState = newKeyboardState;
        }

        private void DrawMenu(SpriteBatch spriteBatch)
        {
            _UIManager.Draw(spriteBatch);
            if (CurrentGameState == GameStates.HighscoreMenu)
            {
                _HighscoreManager.Draw(spriteBatch, GamePaths.HighscoreDataPath);
            }
        }
        private void DrawPlaying(SpriteBatch spriteBatch)
        {
            if (isPaused)
            {
                spriteBatch.Draw(_TextureManager.PauseIcon, _TextureManager.PauseIconDestRect, Color.White);
            }
            _LevelHandler.Draw(spriteBatch);
            //_LevelHandler.Draw(spriteBatch, _HudManager.EditorTiles);
            //_HudManager.Draw(spriteBatch);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentGameState == GameStates.Playing)
            {
                spriteBatch.Begin(transformMatrix: _LevelHandler.LevelCamera.Transform);
                DrawPlaying(spriteBatch);
                spriteBatch.End();
            }
            else
            {
                SamplerState gameSamplerState = SamplerState.PointClamp;
                spriteBatch.Begin(samplerState: gameSamplerState);
                spriteBatch.Draw(_BackgroundTexture, GameValues.WindowBounds, Color.White);
                DrawMenu(spriteBatch);
                spriteBatch.End();
            }

        }
    }

    public enum GameStates
    {
        MainMenu,
        HighscoreMenu,
        GameOverWinMenu,
        GameOverLossMenu,
        Playing, Replaying,
        Quit
    }
}
