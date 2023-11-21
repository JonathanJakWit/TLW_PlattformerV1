using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLW_Plattformer.RipyGame.Models
{
    internal class Sprite : GameObject
    {
        private readonly Texture2D _texture;
        private readonly Color _color;
        protected float rotation;
        protected float scale;
        protected float drawLayerIndex;
        protected SpriteEffects spriteEffects;

        public Sprite(Texture2D texture, Vector2 position, Color color, float scale = 1f, float drawLayerIndex = 0.2f, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0f)
        {
            _texture = texture;
            _color = color;
            this.spriteEffects = spriteEffects;
            this.rotation = rotation;
            this.scale = scale;
            this.drawLayerIndex = drawLayerIndex;
            Position = position;
            Bounds = new Rectangle((int)position.X, (int)position.Y, _texture.Width, _texture.Height);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, Bounds, _color, rotation, Center, scale, spriteEffects, drawLayerIndex);
        }
    }

    public class AnimatedSprite : MoveableGameObject
    {
        private Color color;
        protected Animation baseAnim;
        protected Animation activeAnimation;
        protected List<Animation> animationQueue;
        protected float rotation;
        protected float scale;
        protected float drawLayerIndex;
        protected SpriteEffects spriteEffects;

        private Vector2 original_position;
        private Vector2 original_velocity;
        private Rectangle original_bounds;

        public AnimatedSprite(Vector2 position, Vector2 velocity, Rectangle bounds, Color color, float scale, float drawLayerIndex = 0.1F, SpriteEffects spriteEffects = SpriteEffects.None, float rotation = 0F)
            : base(position, velocity, bounds)
        {
            this.baseAnim = null;
            this.activeAnimation = null;
            this.animationQueue = new List<Animation>();
            this.color = color;
            this.rotation = rotation;
            this.scale = scale;
            this.drawLayerIndex = drawLayerIndex;
            this.spriteEffects = spriteEffects;

            this.original_position = position;
            this.original_velocity = velocity;
            this.original_bounds = bounds;
        }
        public AnimatedSprite(Vector2 position, Rectangle bounds)
            : base(position, new(0, 0), bounds)
        {
            this.animationQueue = new List<Animation>();
        }

        public virtual void Reset()
        {
            Position = original_position;
            Velocity = original_velocity;
            Bounds = original_bounds;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (this.activeAnimation == null) { return; }

            activeAnimation.Update(gameTime);
            if (!activeAnimation.IsRepeating && activeAnimation.DurationTimer.TimerFinished)
            {
                if (animationQueue.Count > 1)
                {
                    activeAnimation = animationQueue[0];
                    animationQueue.RemoveAt(0);
                }
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            activeAnimation.Draw(Position, spriteBatch, scale, drawLayerIndex);
        }
    }
}
