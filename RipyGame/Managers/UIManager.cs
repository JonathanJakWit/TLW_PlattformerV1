using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Components;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Managers
{
    public class UIManager
    {
        public GameStates GameState { get; set; }
        public bool IsNewGameState { get; set; }
        public bool IsResetGame { get; set; }

        private Texture2D buttonTexture;
        private Vector2 firstButtonPos;
        private Vector2 secondButtonPos;
        private Vector2 thirdButtonPos;
        private Vector2 fourthButtonPos;

        private int mainMenuPlayButtonIndex;
        private int gameOverMenuReplayButtonIndex;
        private int mainMenuHighscoreButtonIndex;
        private int mainMenuQuitButtonIndex;
        private int highscoreMenuMainMenuButtonIndex;

        public Dictionary<ButtonType, Button> buttons;

        public UIManager(TextureManager textureManager, GameStates gameState)
        {
            GameState = gameState;
            IsNewGameState = false;
            IsResetGame = true;

            mainMenuPlayButtonIndex = 0;
            gameOverMenuReplayButtonIndex = 1;
            mainMenuHighscoreButtonIndex = 2;
            mainMenuQuitButtonIndex = 3;
            highscoreMenuMainMenuButtonIndex = 4;

            string playText = "Play";
            string replayText = "Replay";
            string quitText = "Quit";
            string highscoreText = "Scores";
            string menuText = "Menu";

            Color buttonIdleColor = Color.LightGray;
            Color buttonInFocusColor = Color.Gray;
            Vector2 buttonScale = new Vector2(4, 4);

            buttonTexture = textureManager.MenuButton;
            //float yDisplacement = GameValues.TileHeight * GameValues.TileScale.Y * 8;
            float yDisplacement = GameValues.WindowBounds.Height / 4;
            firstButtonPos = new Vector2(GameValues.WindowCenter.X - buttonTexture.Width * 8, GameValues.WindowCenter.Y + buttonTexture.Height / 2 + yDisplacement);
            secondButtonPos = new Vector2(GameValues.WindowCenter.X - buttonTexture.Width / 2, GameValues.WindowCenter.Y + buttonTexture.Height / 2 + yDisplacement);
            thirdButtonPos = new Vector2(GameValues.WindowCenter.X + buttonTexture.Width * 8, GameValues.WindowCenter.Y + buttonTexture.Height / 2 + yDisplacement);
            fourthButtonPos = new Vector2(GameValues.WindowCenter.X - buttonTexture.Width / 2, GameValues.WindowBounds.Height - buttonTexture.Height * 10);

            buttons = new Dictionary<ButtonType, Button>()
            {
                {ButtonType.Play, new Button(playText, GameValues.ArcadeFont, buttonTexture, buttonInFocusColor, firstButtonPos, buttonIdleColor, buttonScale)},
                {ButtonType.Replay, new Button(replayText, GameValues.ArcadeFont, buttonTexture, buttonInFocusColor, firstButtonPos, buttonIdleColor, buttonScale)},
                {ButtonType.HighscoreMenu, new Button(highscoreText, GameValues.ArcadeFont, buttonTexture, buttonInFocusColor, secondButtonPos, buttonIdleColor, buttonScale)},
                {ButtonType.Quit, new Button(quitText, GameValues.ArcadeFont, buttonTexture, buttonInFocusColor, thirdButtonPos, buttonIdleColor, buttonScale)},
                {ButtonType.MainMenu, new Button(menuText, GameValues.ArcadeFont, buttonTexture, buttonInFocusColor, fourthButtonPos, buttonIdleColor, buttonScale)},
            };
        }

        public void Reset(GameStates gameState)
        {
            GameState = gameState;
            IsNewGameState = true;
        }

        public void Update(MouseState oldMouseState, MouseState newMouseState, GameStates gameState)
        {
            if (GameState != gameState)
            {
                IsNewGameState = true;
            }

            switch (GameState)
            {
                case GameStates.MainMenu:
                    buttons.GetValueOrDefault(ButtonType.Play).Update(oldMouseState, newMouseState);
                    buttons.GetValueOrDefault(ButtonType.HighscoreMenu).Update(oldMouseState, newMouseState);
                    buttons.GetValueOrDefault(ButtonType.Quit).Update(oldMouseState, newMouseState);

                    if (buttons.GetValueOrDefault(ButtonType.Play).IsClicked)
                    {
                        GameState = GameStates.Playing;
                        IsNewGameState = true;
                        buttons.GetValueOrDefault(ButtonType.Play).IsClicked = false;
                    }
                    else if (buttons.GetValueOrDefault(ButtonType.HighscoreMenu).IsClicked)
                    {
                        GameState = GameStates.HighscoreMenu;
                        IsNewGameState = true;
                        buttons.GetValueOrDefault(ButtonType.HighscoreMenu).IsClicked = false;
                    }
                    else if (buttons.GetValueOrDefault(ButtonType.Quit).IsClicked)
                    {
                        GameState = GameStates.Quit;
                        IsNewGameState = true;
                        buttons.GetValueOrDefault(ButtonType.Quit).IsClicked = false;
                    }
                    break;

                case GameStates.HighscoreMenu:
                    buttons.GetValueOrDefault(ButtonType.MainMenu).Update(oldMouseState, newMouseState);

                    if (buttons.GetValueOrDefault(ButtonType.MainMenu).IsClicked)
                    {
                        GameState = GameStates.MainMenu;
                        IsNewGameState = true;
                        buttons.GetValueOrDefault(ButtonType.MainMenu).IsClicked = false;
                    }
                    break;

                case GameStates.GameOverWinMenu:
                    buttons.GetValueOrDefault(ButtonType.Replay).Update(oldMouseState, newMouseState);
                    buttons.GetValueOrDefault(ButtonType.HighscoreMenu).Update(oldMouseState, newMouseState);
                    buttons.GetValueOrDefault(ButtonType.Quit).Update(oldMouseState, newMouseState);

                    if (buttons.GetValueOrDefault(ButtonType.Replay).IsClicked)
                    {
                        GameState = GameStates.Replaying;
                        IsResetGame = true;
                        IsNewGameState = true;
                        Reset(GameStates.Replaying);
                        buttons.GetValueOrDefault(ButtonType.Replay).IsClicked = false;
                    }
                    else if (buttons.GetValueOrDefault(ButtonType.HighscoreMenu).IsClicked)
                    {
                        GameState = GameStates.HighscoreMenu;
                        IsNewGameState = true;
                        buttons.GetValueOrDefault(ButtonType.HighscoreMenu).IsClicked = false;
                    }
                    else if (buttons.GetValueOrDefault(ButtonType.Quit).IsClicked)
                    {
                        GameState = GameStates.Quit;
                        IsNewGameState = true;
                        buttons.GetValueOrDefault(ButtonType.Quit).IsClicked = false;
                    }
                    break;

                case GameStates.GameOverLossMenu:
                    buttons.GetValueOrDefault(ButtonType.Replay).Update(oldMouseState, newMouseState);
                    buttons.GetValueOrDefault(ButtonType.HighscoreMenu).Update(oldMouseState, newMouseState);
                    buttons.GetValueOrDefault(ButtonType.Quit).Update(oldMouseState, newMouseState);

                    if (buttons.GetValueOrDefault(ButtonType.Replay).IsClicked)
                    {
                        GameState = GameStates.Replaying;
                        IsResetGame = true;
                        IsNewGameState = true;
                        Reset(GameStates.Replaying);
                        buttons.GetValueOrDefault(ButtonType.Replay).IsClicked = false;
                    }
                    else if (buttons.GetValueOrDefault(ButtonType.HighscoreMenu).IsClicked)
                    {
                        GameState = GameStates.HighscoreMenu;
                        IsNewGameState = true;
                        buttons.GetValueOrDefault(ButtonType.HighscoreMenu).IsClicked = false;
                    }
                    else if (buttons.GetValueOrDefault(ButtonType.Quit).IsClicked)
                    {
                        GameState = GameStates.Quit;
                        IsNewGameState = true;
                        buttons.GetValueOrDefault(ButtonType.Quit).IsClicked = false;
                    }
                    break;

                case GameStates.Playing:
                    break;
                case GameStates.Quit:
                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            switch (GameState)
            {
                case GameStates.MainMenu:
                    buttons.GetValueOrDefault(ButtonType.Play).Draw(spriteBatch);
                    buttons.GetValueOrDefault(ButtonType.HighscoreMenu).Draw(spriteBatch);
                    buttons.GetValueOrDefault(ButtonType.Quit).Draw(spriteBatch);
                    break;
                case GameStates.HighscoreMenu:
                    buttons.GetValueOrDefault(ButtonType.MainMenu).Draw(spriteBatch);
                    break;
                case GameStates.GameOverWinMenu:
                    buttons.GetValueOrDefault(ButtonType.Replay).Draw(spriteBatch);
                    buttons.GetValueOrDefault(ButtonType.HighscoreMenu).Draw(spriteBatch);
                    buttons.GetValueOrDefault(ButtonType.Quit).Draw(spriteBatch);
                    break;
                case GameStates.GameOverLossMenu:
                    buttons.GetValueOrDefault(ButtonType.Replay).Draw(spriteBatch);
                    buttons.GetValueOrDefault(ButtonType.HighscoreMenu).Draw(spriteBatch);
                    buttons.GetValueOrDefault(ButtonType.Quit).Draw(spriteBatch);
                    break;



                case GameStates.Playing:
                    break;
                case GameStates.Replaying:
                    break;
                case GameStates.Quit:
                    break;
                default:
                    break;
            }
        }
    }

    public enum ButtonType
    {
        Play, Replay,
        Quit,
        MainMenu,
        HighscoreMenu,
    }
}
