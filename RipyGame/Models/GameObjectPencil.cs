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
        private TextureManager textureManager;

        public GameObjectTypes SelectedType { get; set; }
        //public Vector2 SelectedPosition { get; set; }

        //public Plattform SelectedPlattform { get; set; }
        //public Item SelectedItem { get; set; }
        //public Player SelectedPlayer { get; set; }

        public List<GameObject> DrawnGameObjects { get; set; }

        public GameObjectPencil(TextureManager textureManager)
        {
            this.textureManager = textureManager;
            SelectedType = GameObjectTypes.Plattform;
            //SelectedPlattform = GetDefaultPlattform();
            DrawnGameObjects = new List<GameObject>();
        }

        private Plattform GetDefaultPlattform()
        {
            Plattform plattform = new Plattform(textureManager, PlattformTypes.Solid, new(0, 0), GameValues.ColumnWidth, GameValues.TileHeight);
            return plattform;
        }
        private Plattform GetPlattform(TextureManager textureManager, PlattformTypes plattformType, Vector2 position)
        {
            Plattform plattform = new Plattform(textureManager, plattformType, position, GameValues.ColumnWidth, GameValues.RowHeight);
            return plattform;
        }

        private void UpdateSelectedObjects()
        {
            //SelectedPlattform.MoveTo(SelectedPosition);
            //SelectedPlattform = new Plattform(textureManager, PlattformTypes.Solid, GameValues.)
        }

        public void Update(Vector2 displacedPosition)
        {
            if (GameValues.IsLeftMouseClicked())
            {
                //SelectedPosition = new Vector2(GameValues.NewMouseState.X / GameValues.ColumnWidth, GameValues.NewMouseState.Y / GameValues.RowHeight);
                //SelectedPosition = new Vector2(GameValues.NewMouseState.X, GameValues.NewMouseState.Y);
                //SelectedPosition = new Vector2(GameValues.WindowCenter.X, GameValues.WindowCenter.Y);
                //UpdateSelectedObjects();
                switch (SelectedType)
                {
                    case GameObjectTypes.Plattform:
                        Vector2 cursorPos = new Vector2(GameValues.NewMouseState.X + displacedPosition.X, GameValues.NewMouseState.Y);
                        DrawnGameObjects.Add(GetPlattform(textureManager, PlattformTypes.Solid, GameValues.NewMouseState.Position.ToVector2()));
                        //DrawnGameObjects.Add(new Plattform(textureManager, PlattformTypes.Solid, GameValues.WindowCenter, new(GameValues.WindowCenter.X + 32, GameValues.WindowCenter.Y)));
                        //DrawnGameObjects.Add(SelectedPlattform);
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
