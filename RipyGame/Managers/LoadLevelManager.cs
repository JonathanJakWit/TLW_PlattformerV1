using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Models;

namespace TLW_Plattformer.RipyGame.Managers
{
    public class LoadLevelManager
    {
        private static List<Plattform> _plattforms;
        private static List<Item> _items;
        private static List<Player> _players;

        public LoadLevelManager(string levelDataPath, AnimationManager animationManager)
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

            _plattforms = new List<Plattform>();
            _plattforms.Add(new Plattform(PlattformTypes.Solid, plattform1Pos, plattform1Width, plattform1Height));
            _plattforms.Add(new Plattform(PlattformTypes.Solid, plattform2Pos, plattform2Width, plattform2Height));
            #endregion PLattforms

            #region Items
            _items = new List<Item>();
            #endregion Items

            #region Players
            Vector2 player1Pos = new Vector2(
                GameValues.LevelStartPos.X + GameValues.ColumnWidth * 2,
                GameValues.LevelEndPos.Y - GameValues.RowHeight * 2);
            float p1Speed = 5F;
            float p1JumpSpeed = 10F;

            _players = new List<Player>();
            _players.Add(new Player(PlayerIndex.One, animationManager, player1Pos, Color.White, p1Speed, p1JumpSpeed));
            #endregion Players
        }

        public List<Plattform> GetPlattforms() { return _plattforms; }
        public List<Item> GetItems() { return _items; }
        public List<Player> GetPlayers() { return _players; }

        //public Plattform[] FillEmptyPlattformArray()
        //{
        //    Plattform[] emptyPlattformArray = new Plattform[_plattforms.Count];
        //    _plattforms.CopyTo(emptyPlattformArray);
        //    return emptyPlattformArray;
        //}

        //public void FillEmptyItemArray(Item[] emptyItemArray)
        //{
        //    _items.CopyTo(emptyItemArray);
        //}

        //public void FillEmptyPlayerArray(Player[] emptyPlayerArray)
        //{
        //    _players.CopyTo(emptyPlayerArray);
        //}

        //public List<Item> GetItems() { return _items; }
        //public List<Player> GetPlayers() { return _players; }
    }
}
