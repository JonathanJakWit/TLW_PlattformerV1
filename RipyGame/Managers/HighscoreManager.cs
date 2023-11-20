using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Managers
{
    public class HighscoreManager
    {
        private Vector2 highscoreTextPosition;

        private Dictionary<string, int> highScores;

        public HighscoreManager(string PATH)
        {
            this.highscoreTextPosition = new Vector2(GameValues.WindowCenter.X - GameValues.TileWidth * GameValues.TileScale.X, GameValues.WindowCenter.Y - GameValues.TileHeight * GameValues.TileScale.Y);

            this.highScores = new Dictionary<string, int>();

            XElement dataFromFile = XElement.Load(PATH);

            XElement player1 = dataFromFile.Element("Player1");
            XElement player2 = dataFromFile.Element("Player2");
            XElement player3 = dataFromFile.Element("Player3");
            XElement player4 = dataFromFile.Element("Player4");
            XElement player5 = dataFromFile.Element("Player5");

            LoadScoreToObject(player1);
            LoadScoreToObject(player2);
            LoadScoreToObject(player3);
            LoadScoreToObject(player4);
            LoadScoreToObject(player5);
        }

        private void LoadScoreToObject(XElement playerData)
        {
            string pName = playerData.Element("Name").Value;
            int pScore = int.Parse(playerData.Element("Score").Value);

            if (highScores.ContainsKey(pName))
            {

                highScores[pName] = pScore;
            }
            else
            {
                highScores.Add(pName, pScore);
            }
        }

        public string GetScoresString(string PATH)
        {
            Dictionary<string, int> newHighScores = new Dictionary<string, int>();

            XElement dataFromFile = XElement.Load(PATH);

            XElement player1 = dataFromFile.Element("Player1");
            XElement player2 = dataFromFile.Element("Player2");
            XElement player3 = dataFromFile.Element("Player3");
            XElement player4 = dataFromFile.Element("Player4");
            XElement player5 = dataFromFile.Element("Player5");

            string p1Name = player1.Element("Name").Value;
            int p1Score = int.Parse(player1.Element("Score").Value);
            if (newHighScores.ContainsKey(p1Name)) { newHighScores[p1Name] = p1Score; }
            else { newHighScores.Add(p1Name, p1Score); }

            string p2Name = player2.Element("Name").Value;
            int p2Score = int.Parse(player2.Element("Score").Value);
            if (newHighScores.ContainsKey(p2Name)) { newHighScores[p2Name] = p2Score; }
            else { newHighScores.Add(p2Name, p2Score); }

            string p3Name = player3.Element("Name").Value;
            int p3Score = int.Parse(player3.Element("Score").Value);
            if (newHighScores.ContainsKey(p3Name)) { newHighScores[p3Name] = p3Score; }
            else { newHighScores.Add(p3Name, p3Score); }

            string p4Name = player4.Element("Name").Value;
            int p4Score = int.Parse(player4.Element("Score").Value);
            if (newHighScores.ContainsKey(p4Name)) { newHighScores[p4Name] = p4Score; }
            else { newHighScores.Add(p4Name, p4Score); }

            string p5Name = player5.Element("Name").Value;
            int p5Score = int.Parse(player5.Element("Score").Value);
            if (newHighScores.ContainsKey(p5Name)) { newHighScores[p5Name] = p5Score; }
            else { newHighScores.Add(p5Name, p5Score); }

            string finishedString = "";
            foreach (var keyAndValue in newHighScores)
            {
                finishedString += "Name - [" + keyAndValue.Key + "] | Score - [" + keyAndValue.Value + "]\n";
            }

            return finishedString;
        }

        public int GetHighscore()
        {
            return highScores.Values.ElementAt(0);
        }

        private void SetNewScore(string PATH, int scoreIndex, string newName, int newScore)
        {
            var hsDoc = XDocument.Load(PATH);
            string scoreIndexElementStr = "Player" + scoreIndex;
            var scoreIndexName = hsDoc.Root.Element(scoreIndexElementStr).Element("Name");
            var scoreIndexScore = hsDoc.Root.Element(scoreIndexElementStr).Element("Score");
            if (scoreIndexName != null)
            {
                scoreIndexName.Value = newName;
            }
            if (scoreIndexScore != null)
            {
                scoreIndexScore.Value = newScore.ToString();
            }
            hsDoc.Save(PATH);
        }

        public void UpdateAndSetScores(string currentPlayerName, int currentPlayerScore)
        {
            if (currentPlayerScore > highScores.Values.ElementAt(0))
            {
                SetNewScore(GamePaths.HighscoreDataPath, 1, currentPlayerName, currentPlayerScore);
                SetNewScore(GamePaths.HighscoreDataPath, 2, highScores.Keys.ElementAt(0), highScores.Values.ElementAt(0));
                SetNewScore(GamePaths.HighscoreDataPath, 3, highScores.Keys.ElementAt(1), highScores.Values.ElementAt(1));
                SetNewScore(GamePaths.HighscoreDataPath, 4, highScores.Keys.ElementAt(2), highScores.Values.ElementAt(2));
                SetNewScore(GamePaths.HighscoreDataPath, 5, highScores.Keys.ElementAt(3), highScores.Values.ElementAt(3));
            }
            else if (currentPlayerScore > highScores.Values.ElementAt(1))
            {
                SetNewScore(GamePaths.HighscoreDataPath, 2, currentPlayerName, currentPlayerScore);
                SetNewScore(GamePaths.HighscoreDataPath, 3, highScores.Keys.ElementAt(1), highScores.Values.ElementAt(1));
                SetNewScore(GamePaths.HighscoreDataPath, 4, highScores.Keys.ElementAt(2), highScores.Values.ElementAt(2));
                SetNewScore(GamePaths.HighscoreDataPath, 5, highScores.Keys.ElementAt(3), highScores.Values.ElementAt(3));
            }
            else if (currentPlayerScore > highScores.Values.ElementAt(2))
            {
                SetNewScore(GamePaths.HighscoreDataPath, 3, currentPlayerName, currentPlayerScore);
                SetNewScore(GamePaths.HighscoreDataPath, 4, highScores.Keys.ElementAt(2), highScores.Values.ElementAt(2));
                SetNewScore(GamePaths.HighscoreDataPath, 5, highScores.Keys.ElementAt(3), highScores.Values.ElementAt(3));
            }
            else if (currentPlayerScore > highScores.Values.ElementAt(3))
            {
                SetNewScore(GamePaths.HighscoreDataPath, 4, currentPlayerName, currentPlayerScore);
                SetNewScore(GamePaths.HighscoreDataPath, 5, highScores.Keys.ElementAt(3), highScores.Values.ElementAt(3));
            }
            else if (currentPlayerScore > highScores.Values.ElementAt(4))
            {
                SetNewScore(GamePaths.HighscoreDataPath, 5, currentPlayerName, currentPlayerScore);
            }
        }

        public void Draw(SpriteBatch spriteBatch, string scoreDataPath)
        {
            spriteBatch.DrawString(GameValues.ArcadeFont, GetScoresString(scoreDataPath), highscoreTextPosition, Color.White);
        }
    }
}
