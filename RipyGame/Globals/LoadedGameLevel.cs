using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Managers;
using TLW_Plattformer.RipyGame.Models;

namespace TLW_Plattformer.RipyGame.Globals
{
    public static class LoadedGameLevel
    {
        //public static List<Plattform> Plattforms { get; set; }
        //public static List<Item> Items { get; set; }
        //public static List<Player> Players { get; set; }

        //public static Dictionary<GameObjectTypes, List<GameObject>> GameObjects { get; set; }
        public static Dictionary<GameObjectTypes, List<GameObject>> GameObjects { get; set; }

        public static void AddPlattform(GameObject plattformObj)
        {
            GameObjects.GetValueOrDefault(GameObjectTypes.Plattform).Add(plattformObj);
        }
        public static void AddItem(Item item)
        {

        }
        public static void AddPlayer(Player player)
        {

        }

        public static void Update(GameTime gameTime)
        {
            //foreach (var item in collection)
            //{

            //}

            foreach (Player player in GameObjects.GetValueOrDefault(GameObjectTypes.PLayer))
            {
                foreach (Plattform plattform in GameObjects.GetValueOrDefault(GameObjectTypes.Plattform))
                {
                    if (player.Bounds.Intersects(plattform.Bounds))
                    {
                        //MoveableDirections mD = GameValues.GetCollisionDirection(player.Bounds, plattform.Bounds);
                        player.HandleCollision(plattform);
                        //plattform.HandleCollision(player);
                    }

                    //if (player.Bounds.Intersects(plattform.Bounds))
                    //{
                    //    player.HandleCollision(plattform);
                    //    //plattform.HandleCollision(player);
                    //}
                }
                player.UpdatePlayer(gameTime);
            }

            //foreach (GameObject current in GameObjects)
            //{
            //    //current.Update(gameTime);
            //    foreach (GameObject other in GameObjects)
            //    {
            //        if (current == other)
            //        {
            //            continue;
            //        }

            //        if (current.Bounds.Intersects(other.Bounds))
            //        {
            //            current.HandleCollision(other);
            //        }
            //    }
            //}
        }

        public static void Draw(SpriteBatch spriteBatch, Texture2D levelTileset, Texture2D plattformTileset)
        {
            foreach (Plattform plattform in GameObjects.GetValueOrDefault(GameObjectTypes.Plattform))
            {
                plattform.Draw(spriteBatch, plattformTileset);
            }

            foreach (Item item in GameObjects.GetValueOrDefault(GameObjectTypes.Item))
            {
                item.Draw(spriteBatch);
            }

            foreach (Player player in GameObjects.GetValueOrDefault(GameObjectTypes.PLayer))
            {
                player.Draw(spriteBatch);
            }
        }

        public static void Load(string levelDataPath, AnimationManager animationManager, TextureManager textureManager)
        {
            //GameObjects = new List<GameObject>();
            GameObjects = new Dictionary<GameObjectTypes, List<GameObject>>();

            // Change below to actually load from the json file

            int levelOneWidth = 2688 * 4;
            GameValues.LevelEndPos = new Vector2(GameValues.LevelStartPos.X + levelOneWidth, GameValues.LevelEndPos.Y);
            GameValues.LevelBounds = new Rectangle(GameValues.LevelStartPos.ToPoint(), GameValues.LevelEndPos.ToPoint());

            #region PLattforms
            Vector2 plattform1Pos = new Vector2(
                GameValues.LevelStartPos.X + GameValues.ColumnWidth,
                GameValues.LevelEndPos.Y - GameValues.RowHeight);
            int plattform1Width = GameValues.ColumnWidth * 128;
            int plattform1Height = GameValues.RowHeight;

            Vector2 plattform2Pos = new Vector2(
                GameValues.LevelStartPos.X + GameValues.ColumnWidth * 6,
                GameValues.LevelEndPos.Y - GameValues.RowHeight * 4);
            int plattform2Width = GameValues.ColumnWidth * 8;
            int plattform2Height = GameValues.RowHeight;

            List<GameObject> plattforms = new List<GameObject>();
            plattforms.Add(new Plattform(textureManager, PlattformTypes.Solid, plattform1Pos, plattform1Width, plattform1Height));
            plattforms.Add(new Plattform(textureManager, PlattformTypes.Solid, plattform2Pos, plattform2Width, plattform2Height));

            GameObjects.Add(GameObjectTypes.Plattform, plattforms);
            #endregion PLattforms

            #region Items
            List<GameObject> items = new List<GameObject>();
            GameObjects.Add(GameObjectTypes.Item, items);
            //Items = new List<Item>();
            #endregion Items

            #region Players
            Vector2 player1Pos = new Vector2(
                GameValues.LevelStartPos.X + GameValues.ColumnWidth * 2,
                GameValues.LevelEndPos.Y - GameValues.RowHeight * 4);
            float p1Scale = 4F;
            float p1MoveSpeed = 12F;
            float p1JumpSpeed = 16F;
            float p1FallSpeed = 8F;

            List<GameObject> players = new List<GameObject>();
            players.Add(new Player(PlayerIndex.One, animationManager, player1Pos, Color.White, p1Scale, p1MoveSpeed, p1JumpSpeed, p1FallSpeed));

            GameObjects.Add(GameObjectTypes.PLayer, players);
            //GameObjects.Add(new Player(PlayerIndex.One, animationManager, player1Pos, Color.White, p1Scale, p1MoveSpeed, p1JumpSpeed, p1FallSpeed));

            //Players = new List<Player>();
            //Players.Add(new Player(PlayerIndex.One, animationManager, player1Pos, Color.White, p1Scale, p1MoveSpeed, p1JumpSpeed, p1FallSpeed));
            #endregion Players
        }
    }
}
