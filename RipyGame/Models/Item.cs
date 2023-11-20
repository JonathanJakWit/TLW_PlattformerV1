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
    public class Item : AnimatedSprite
    {
        public ItemTypes ItemType { get; private set; }
        private bool playerIsInteracting;

        public Item(ItemTypes itemType, Vector2 position, Vector2 velocity, Rectangle bounds, Color color, float scale, float drawLayerIndex, SpriteEffects spriteEffects=SpriteEffects.None)
            : base(position, velocity, bounds, color, scale, drawLayerIndex, spriteEffects)
        {
            this.ItemType = itemType;
            this.playerIsInteracting = false;
        }

        public Item(ItemTypes itemType, Vector2 position)
            : base(position, new(0, 0), new((int)position.X, (int)position.Y, GameValues.TileWidth * (int)GameValues.TileScale.X, GameValues.TileHeight * (int)GameValues.TileScale.Y), Color.White, GameValues.TileScale.X, GameValues.ItemDrawLayer)
        {
            ItemType = itemType;
        }

        public bool IsPlayerCollision(Player player)
        {
            if (player.Bounds.Intersects(Bounds))
            {
                playerIsInteracting = true;
            }
            return playerIsInteracting;
        }

        private void HandlePlayerInteraction()
        {
            // TODO : Add code to make things happen on player interaction with item depending on the itemtype
        }

        public override void Update(GameTime gameTime)
        {
            if (playerIsInteracting)
            {
                HandlePlayerInteraction();
            }

            base.Update(gameTime);
        }
    }

    public enum ItemTypes
    {
        HealthPot
    }
}
