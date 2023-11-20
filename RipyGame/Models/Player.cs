using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Managers;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Player : AnimatedSprite
    {
        public bool IsGrounded { get; set; }
        private Dictionary<PlayerActions, Animation> animations { get; set; }

        public Player(AnimationManager animationManager, Vector2 position, Color color)
            : base(position, new(0, 0), new((int)position.X, (int)position.Y, GameValues.PlayerBounds.Width, GameValues.PlayerBounds.Height), color, 1F, GameValues.PlayerDrawLayer)
        {
            IsGrounded = false;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
            animations = animationManager.GetPlayerAnimations();

        }

        public void HandleItemInteraction(Item item)
        {

        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw health icons and points

            base.Draw(spriteBatch);
        }
    }

    public enum PlayerActions
    {
        Idle, HealthIdle,
        MoveLeft, MoveRight,
        Jump, Dash, Slide,
        BasicAttack, HeavyAttack, RangedAttack
    }
}
