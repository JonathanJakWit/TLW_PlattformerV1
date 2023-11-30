using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        public PlattformTypes SelectedPlattformType { get; set; }
        public PlattformAttributes SelectedPlattformAttribute { get; set; }

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
        private Plattform GetPlattform(TextureManager textureManager, PlattformTypes plattformType, Vector2 position, int width=0, int height=0)
        {
            if (width != 0 && height != 0)
            {
                Plattform combinedPlattform = new Plattform(textureManager, plattformType, position, width, GameValues.RowHeight);
                return combinedPlattform;
            }

            Plattform plattform = new Plattform(textureManager, plattformType, position, GameValues.ColumnWidth, GameValues.RowHeight);
            return plattform;
        }

        private void UpdateSelectedObjects()
        {
            //SelectedPlattform.MoveTo(SelectedPosition);
            //SelectedPlattform = new Plattform(textureManager, PlattformTypes.Solid, GameValues.)
        }

        private bool IsInPlattform(Vector2 newPLattformPos)
        {
            for (int i = 0; i < DrawnGameObjects.Count; i++)
            {
                if (DrawnGameObjects[i] is Plattform)
                {
                    if (DrawnGameObjects[i].Bounds.Contains(newPLattformPos))
                    {
                        DrawnGameObjects.Add(GetPlattform(textureManager, PlattformTypes.Solid, DrawnGameObjects[i].Position, DrawnGameObjects[i].Bounds.Width + GameValues.ColumnWidth, DrawnGameObjects[i].Bounds.Height));
                        DrawnGameObjects.RemoveAt(i);
                        return true;
                    }
                }
            }
            return false;
        }

        public void Update(Vector2 displacedPosition)
        {
            if (GameValues.IsLeftMouseClicked())
            {
                switch (SelectedType)
                {
                    case GameObjectTypes.Plattform:
                        Vector2 cursorPos = new Vector2(GameValues.NewMouseState.X + displacedPosition.X - GameValues.WindowCenter.X, GameValues.NewMouseState.Y);
                        if (!IsInPlattform(cursorPos))
                        {
                            DrawnGameObjects.Add(GetPlattform(textureManager, PlattformTypes.Solid, cursorPos));
                        }
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

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (GameObject gameObject in DrawnGameObjects)
            {
                LoadedGameLevel.DrawHitBox(spriteBatch, gameObject);
            }
        }
    }
}
