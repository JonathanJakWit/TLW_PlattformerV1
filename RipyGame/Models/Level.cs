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
        //public Plattform[] Plattforms { get; set; }
        //public Item[] Items { get; set; }
        //public Player[] Players { get; set; }

        public List<Plattform> Plattforms { get; set; }
        public List<Item> Items { get; set; }
        public List<Player> Players { get; set; }

        //public Level(int levelIndex, LoadLevelManager loadLevelManager)
        public Level(int levelIndex, AnimationManager animationManager)
        {
            if (levelIndex == 1)
            {
                LoadLevel(animationManager);

                //Plattforms = loadLevelManager.FillEmptyPlattformArray();
                //loadLevelManager.FillEmptyItemArray(Items);
                //loadLevelManager.FillEmptyPlayerArray(Players);

                //this.Plattforms = LoadedLevel.Plattforms;
                //this.Items = LoadedLevel.Items;
                //this.Players = LoadedLevel.Players;

                //this.Plattforms = GetPlattforms(GamePaths.LevelOneDataPath);
                //this.Items = GetItems(GamePaths.LevelOneDataPath);
                //this.Players = GetPlayers(GamePaths.LevelOneDataPath);
            }
        }

        private void LoadLevel(AnimationManager animationManager)
        {
            // Change below code to actually load from a json file

            #region PLattforms
            Vector2 plattform1Pos = new Vector2(
                GameValues.LevelStartPos.X + GameValues.ColumnWidth,
                GameValues.LevelEndPos.Y - GameValues.RowHeight);
            int plattform1Width = GameValues.ColumnWidth * 4;
            int plattform1Height = GameValues.RowHeight;

            Vector2 plattform2Pos = new Vector2(
                plattform1Pos.X + GameValues.ColumnWidth * 2,
                plattform1Pos.Y);
            int plattform2Width = GameValues.ColumnWidth * 6;
            int plattform2Height = GameValues.RowHeight;

            Plattforms = new List<Plattform>();
            Plattforms.Add(new Plattform(PlattformTypes.Solid, plattform1Pos, plattform1Width, plattform1Height));
            Plattforms.Add(new Plattform(PlattformTypes.Solid, plattform2Pos, plattform2Width, plattform2Height));
            #endregion PLattforms


            #region Items
            Items = new List<Item>();
            #endregion Items

            #region Players
            Vector2 player1Pos = new Vector2(
                GameValues.LevelStartPos.X + GameValues.ColumnWidth * 2,
                GameValues.LevelEndPos.Y - GameValues.RowHeight * 2);
            float p1Speed = 5F;
            float p1JumpSpeed = 10F;

            Players = new List<Player>();
            Players.Add(new Player(PlayerIndex.One, animationManager, player1Pos, Color.White, p1Speed, p1JumpSpeed));
            #endregion Players
        }

        private List<Plattform> GetPlattforms(string jsonPath)
        {
            List<Plattform> loadedPlattforms = new List<Plattform>();

            // TODO : Load the plattforms from the json file

            return loadedPlattforms;
        }
        private List<Item> GetItems(string jsonPath)
        {
            List<Item> loadedItems = new List<Item>();

            // TODO : Load the Items from the json file

            return loadedItems;
        }
        private List<Player> GetPlayers(string jsonPath)
        {
            List<Player> loadedPlayers = new List<Player>();

            // TODO : Load the Players from the json file

            return loadedPlayers;
        }

        private void UpdateCollisions()
        {
            foreach (Player player in Players)
            {
                if (player.IsGrounded)
                {
                    continue;
                }
                foreach (Plattform plattform in Plattforms)
                {
                    if (player.Bounds.Intersects(plattform.Bounds))
                    {
                        player.IsGrounded = true;
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateCollisions();

            foreach (Item item in Items)
            {
                foreach (Player player in Players)
                {
                    if (item.IsPlayerCollision(player))
                    {
                        player.HandleItemInteraction(item);
                    }
                }
                item.Update(gameTime);
            }

            foreach (Player player in Players)
            {
                player.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D levelTileset)
        {
            foreach (Plattform plattform in this.Plattforms)
            {
                plattform.Draw(spriteBatch, levelTileset);
            }

            foreach (Item item in Items)
            {
                item.Draw(spriteBatch);
            }

            foreach (Player player in Players)
            {
                player.Draw(spriteBatch);
            }
        }
    }
}
