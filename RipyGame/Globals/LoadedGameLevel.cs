using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TLW_Plattformer.RipyGame.Managers;
using TLW_Plattformer.RipyGame.Models;
using System.IO;
using TLW_Plattformer.RipyGame.Components;

namespace TLW_Plattformer.RipyGame.Globals
{
    public static class LoadedGameLevel
    {
        private static Texture2D hitBoxTex {  get; set; }
        public static bool DrawHitboxes { get; set; }

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
            if (GameValues.IsKeyPressed(Keys.H))
            {
                DrawHitboxes = !DrawHitboxes;
            }

            foreach (Player player in GameObjects.GetValueOrDefault(GameObjectTypes.PLayer))
            {
                foreach (Plattform plattform in GameObjects.GetValueOrDefault(GameObjectTypes.Plattform))
                {
                    if (player.Bounds.Intersects(plattform.Bounds))
                    {
                        MoveableDirections colDir = GameValues.GetCollisionDirection(player, plattform);
                        player.HandleCollision(plattform, colDir);
                    }

                    //if (player.Bounds.Intersects(plattform.Bounds))
                    //{
                    //    player.HandleCollision(plattform);
                    //    //plattform.HandleCollision(player);
                    //}
                }
                player.Update(gameTime);
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

        public static void DrawHitBox(SpriteBatch spriteBatch, GameObject gameObject)
        {
            spriteBatch.Draw(hitBoxTex, gameObject.Bounds, Color.Red);
        }

        public static void Draw(SpriteBatch spriteBatch, Texture2D levelTileset, Texture2D plattformTileset)
        {
            foreach (Plattform plattform in GameObjects.GetValueOrDefault(GameObjectTypes.Plattform))
            {
                if (DrawHitboxes)
                {
                    DrawHitBox(spriteBatch, plattform);
                }
                plattform.Draw(spriteBatch, plattformTileset);
            }

            foreach (Item item in GameObjects.GetValueOrDefault(GameObjectTypes.Item))
            {
                if (DrawHitboxes)
                {
                    DrawHitBox(spriteBatch, item);
                }
                item.Draw(spriteBatch);
            }

            foreach (Player player in GameObjects.GetValueOrDefault(GameObjectTypes.PLayer))
            {
                if (DrawHitboxes)
                {
                    DrawHitBox(spriteBatch, player);
                }
                player.Draw(spriteBatch);
            }
        }

        private static Player GetPlayer(int playerIndex, Rectangle playerBounds, AnimationManager animationManager)
        {
            PlayerIndex curPlayerIndex = PlayerIndex.One;
            if (playerIndex == 1) { curPlayerIndex = PlayerIndex.One; }
            else if (playerIndex == 2) { curPlayerIndex = PlayerIndex.Two; }
            else if (playerIndex == 3) { curPlayerIndex = PlayerIndex.Three; }
            else if (playerIndex == 4) { curPlayerIndex = PlayerIndex.Four; }

            Player madePlayer = new Player(curPlayerIndex, animationManager,
                new(playerBounds.X, playerBounds.Y), Color.White, GameValues.PlayerScale.X,
                GameValues.PlayerMoveSpeed, GameValues.PlayerJumpSpeed, GameValues.PlayerFallSpeed);
            return madePlayer;
        }

        private static Plattform GetPlattform(PlattformTypes plattformType, Rectangle plattformBounds, TextureManager textureManager)
        {
            Plattform madePlattform = new Plattform(textureManager, plattformType,
                new(plattformBounds.X, plattformBounds.Y),
                plattformBounds.Width / (int)GameValues.TileScale.X / (int)GameValues.ColumnWidth,
                plattformBounds.Height / (int)GameValues.TileScale.Y / (GameValues.RowHeight));
            return madePlattform;
        }

        private static Enemy GetEnemy(EnemyTypes enemyType, Rectangle enemyBounds, AnimationManager animationManager)
        {
            Enemy madeEnemy = new Enemy(enemyType, animationManager, enemyBounds, new(0, 0));
            return madeEnemy;
        }

        public static void LoadLevel(string levelDataPath, AnimationManager animationManager, TextureManager textureManager)
        {
            hitBoxTex = textureManager.FullTex;
            DrawHitboxes = true;

            GameObjects = new Dictionary<GameObjectTypes, List<GameObject>>();

            List<Rectangle> playerBoundsList = new List<Rectangle>();
            List<Rectangle> plattformBoundsList = new List<Rectangle>();
            List<Rectangle> enemyBoundsList = new List<Rectangle>();
            List<PlattformTypes> plattformTypeList = new List<PlattformTypes>();
            List<EnemyTypes> enemyTypeList = new List<EnemyTypes>();

            if (File.Exists(levelDataPath))
            {
                playerBoundsList = JsonParser.GetRectangleList(levelDataPath, "players");
                plattformBoundsList = JsonParser.GetRectangleList(levelDataPath, "platforms");
                enemyBoundsList = JsonParser.GetRectangleList(levelDataPath, "enemies");

                plattformTypeList = JsonParser.GetPlattformTypeList(levelDataPath);
                enemyTypeList = JsonParser.GetEnemyTypeList(levelDataPath);
            }
            else
            {
                throw new Exception("Level Does Not Exist");
            }

            List<GameObject> players = new List<GameObject>();
            List<GameObject> plattforms = new List<GameObject>();
            List<GameObject> enemies = new List<GameObject>();

            int curPlayerIndex = 1;
            foreach (Rectangle playerBounds in playerBoundsList)
            {
                players.Add(GetPlayer(curPlayerIndex, playerBounds, animationManager));
                curPlayerIndex++;
            }

            int curPlattformIndex = 0;
            foreach (Rectangle plattformBounds in plattformBoundsList)
            {
                plattforms.Add(GetPlattform(plattformTypeList[curPlattformIndex], plattformBounds, textureManager));
                curPlattformIndex++;
            }

            int curEnemyIndex = 0;
            foreach (Rectangle enemyBounds in enemyBoundsList)
            {
                enemies.Add(GetEnemy(enemyTypeList[curEnemyIndex], enemyBounds, animationManager));
                curEnemyIndex++;
            }

            GameObjects.Add(GameObjectTypes.PLayer, players);
            GameObjects.Add(GameObjectTypes.Plattform, plattforms);
            GameObjects.Add(GameObjectTypes.Enemy, enemies);
        }

        public static void WriteLevel(string path)
        {
            if (File.Exists(path))
            {
                JsonParser.WriteJsonToFile(path);
            }
            else
            {
                throw new Exception("Level File Does Not Exist");
            }
        }

        public static void Load(AnimationManager animationManager, TextureManager textureManager)
        {
            hitBoxTex = textureManager.FullTex;
            DrawHitboxes = true;

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
                GameValues.LevelEndPos.Y - GameValues.RowHeight * 6);
            float p1Scale = 0.35F;
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
