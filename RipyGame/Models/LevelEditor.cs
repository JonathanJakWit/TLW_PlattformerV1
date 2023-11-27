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
    public class LevelEditor
    {
        private TextureManager textureManager;
        private Texture2D levelTileset;
        private Texture2D plattformTileset;

        public FreePlayer cameraTarget;
        private GameObjectPencil objectPencil;
        private List<EditorTile> editorTiles;

        public LevelEditor(TextureManager textureManager, AnimationManager animationManager)
        {
            this.textureManager = textureManager;
            this.levelTileset = textureManager.LevelOneTilesetTex;
            this.plattformTileset = textureManager.StonePlattformTilesetTex;
            this.cameraTarget = new FreePlayer();
            this.objectPencil = new GameObjectPencil(textureManager);
            this.editorTiles = new List<EditorTile>();
            InitializeTiles();
        }
        private void InitializeTiles()
        {
            int edgeAlignment = 5;
            Vector2 firstTilePos = new Vector2(GameValues.WindowBounds.Width - edgeAlignment - GameValues.ColumnWidth * 1, edgeAlignment);
            Rectangle plattformRect = textureManager.PlattformSourceRectangles.GetValueOrDefault(PlattformTextureTypes.Plattform_Middle)[0];
            editorTiles.Add(new EditorTile(plattformRect, GameObjectTypes.Plattform, firstTilePos));
        }

        public void Update()
        {
            cameraTarget.Update();
            objectPencil.Update();

            foreach (EditorTile editorTile in editorTiles)
            {
                editorTile.Update();
                if (editorTile.IsClicked)
                {
                    objectPencil.SelectedType = editorTile.objectType;
                    editorTile.IsClicked = false;
                }
            }

            if (GameValues.IsKeyPressed(Keys.Q))
            {
                foreach (GameObject gameObject in objectPencil.DrawnGameObjects)
                {
                    if (gameObject is Plattform)
                    {
                        LoadedGameLevel.AddPlattform(gameObject);
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (EditorTile editorTile in editorTiles)
            {
                if (editorTile.objectType == GameObjectTypes.Plattform)
                {
                    editorTile.Draw(spriteBatch, plattformTileset);
                }
                else
                {
                    editorTile.Draw(spriteBatch, levelTileset);
                }
            }

            //foreach (GameObject gameObject in objectPencil.DrawnGameObjects)
            //{
            //    gameObject
            //}
        }
    }
}
