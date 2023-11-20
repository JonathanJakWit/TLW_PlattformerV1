using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Level
    {
        public List<Plattform> Plattforms { get; set; }
        public List<Item> Items { get; set; }
        public List<Player> Players { get; set; }

        public Level(int levelIndex)
        {
            if (levelIndex < 1)
            {
                this.Plattforms = GetPlattforms(GamePaths.LevelOneDataPath);
                this.Items = GetItems(GamePaths.LevelOneDataPath);
                this.Players = GetPlayers(GamePaths.LevelOneDataPath);
            }
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
