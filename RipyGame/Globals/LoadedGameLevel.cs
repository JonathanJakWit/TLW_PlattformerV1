using Microsoft.Xna.Framework;
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
        public static List<Plattform> Plattforms { get; set; }
        public static List<Item> Items { get; set; }
        public static List<Player> Players { get; set; }

        public static void Load(string levelDataPath, AnimationManager animationManager)
        {
            // Change below to actually load from the json file

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
            float p1Scale = 4F;
            float p1MoveSpeed = 5F;
            float p1JumpSpeed = 2F;
            float p1FallSpeed = 1F;

            Players = new List<Player>();
            Players.Add(new Player(PlayerIndex.One, animationManager, player1Pos, Color.White, p1Scale, p1MoveSpeed, p1JumpSpeed, p1FallSpeed));
            #endregion Players
        }
    }
}
