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
        private Texture2D plattformInteractablesTileset;

        public FreePlayer cameraTarget;
        private GameObjectPencil objectPencil;
        private List<EditorTile> editorTiles;

        public LevelEditor(TextureManager textureManager, AnimationManager animationManager)
        {
            this.textureManager = textureManager;
            this.levelTileset = textureManager.LevelOneTilesetTex;
            this.plattformTileset = textureManager.StonePlattformTilesetTex;
            this.plattformInteractablesTileset = textureManager.NoneTex; // FIX
            this.cameraTarget = new FreePlayer();
            this.objectPencil = new GameObjectPencil(textureManager);
            this.editorTiles = new List<EditorTile>();
            InitializeTiles();
        }
        private void InitializeTiles()
        {
            int edgeAlignment = 5;
            Vector2 firstTilePos = new Vector2(GameValues.WindowBounds.Width - edgeAlignment - GameValues.ColumnWidth * 1, edgeAlignment);
            Vector2 secondTilePos = new Vector2(GameValues.WindowBounds.Width - edgeAlignment - GameValues.ColumnWidth * 2, edgeAlignment);
            
            Rectangle plattformSourceRect = textureManager.PlattformSourceRectangles.GetValueOrDefault(PlattformTextureTypes.Plattform_Middle)[0];
            Rectangle spikePlattformSourceRect = textureManager.PlattformSourceRectangles.GetValueOrDefault(PlattformTextureTypes.PlattformBottom_Spikes)[0];
            editorTiles.Add(new EditorTile(plattformTileset, plattformSourceRect, GameObjectTypes.Plattform, firstTilePos));
            editorTiles.Add(new EditorTile(plattformInteractablesTileset, spikePlattformSourceRect, GameObjectTypes.Plattform, secondTilePos));
        }

        public void Update()
        {
            cameraTarget.Update();
            objectPencil.Update(cameraTarget.Position);

            foreach (EditorTile editorTile in editorTiles)
            {
                editorTile.Update();
                if (editorTile.IsClicked)
                {
                    objectPencil.SelectedType = editorTile.objectType;
                    objectPencil.SelectedPlattformType = editorTile.plattformType;
                    objectPencil.SelectedPlattformAttribute = editorTile.plattformAttribute;
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
                    editorTile.Draw(spriteBatch);
                }
                else
                {
                    editorTile.Draw(spriteBatch);
                }
            }
            objectPencil.Draw(spriteBatch);
        }
    }
}
