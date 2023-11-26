using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Managers;

namespace TLW_Plattformer.RipyGame.Models
{
    public class GameObjectPencil
    {
        public GameObjectTypes SelectedType { get; set; }
        public Vector2 SelectedPosition { get; set; }

        public Plattform SelectedPlattform { get; set; }
        public Item SelectedItem { get; set; }
        public Player SelectedPlayer { get; set; }

        private List<GameObject> drawnGameObjects;

        public GameObjectPencil(TextureManager textureManager)
        {
            SelectedType = GameObjectTypes.Plattform;
            SelectedPlattform = GetDefaultPlattform(textureManager);
            drawnGameObjects = new List<GameObject>();
        }

        private Plattform GetDefaultPlattform(TextureManager textureManager)
        {
            Plattform plattform = new Plattform(textureManager, PlattformTypes.Solid, new(0, 0), GameValues.ColumnWidth, GameValues.TileHeight);
            return plattform;
        }
        //private void GetPlattform(TextureManager textureManager, Platt)
        //{
        //    Plattform plattform = new Plattform(textureManager, )
        //}

        private void UpdateSelectedObjects(TextureManager textureManager)
        {
            SelectedPlattform.MoveTo(SelectedPosition);
        }

        public void Update()
        {
            if (GameValues.IsLeftMouseClicked())
            {
                SelectedPosition = new Vector2(GameValues.NewMouseState.X / GameValues.ColumnWidth, GameValues.NewMouseState.Y / GameValues.RowHeight);
                switch (SelectedType)
                {
                    case GameObjectTypes.Plattform:
                        drawnGameObjects.Add(SelectedPlattform);
                        break;
                    case GameObjectTypes.Item:
                        break;
                    case GameObjectTypes.PLayer:
                        break;
                    case GameObjectTypes.Enemy:
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
