using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;

namespace TLW_Plattformer.RipyGame.Models
{
    public abstract class GameObject
    {
        public Vector2 Position { get; protected set; }
        public Vector2 Center { get; protected set; }
        public Rectangle Bounds { get; protected set; }
        public Vector2 HitBoxPosition { get; protected set; }
        public Rectangle HitBox { get; protected set; }
        public bool HasHitBox { get; protected set; }
        public bool IsAlive { get; set; }

        public bool IsDangerous { get; protected set; }

        //public GameObject Parent { get; protected set; }

        public bool IsCentered()
        {
            if (Position.X > GameValues.WindowCenter.X - Bounds.Width)
            {
                if (Position.X < GameValues.WindowCenter.X + Bounds.Width)
                {
                    return true;
                }
            }
            return false;
        }

        public virtual void HandleCollision(GameObject other, MoveableDirections collisionDirection)
        {
            return;

            //if (other == null)
            //{
            //    return;
            //}
        }
    }

    public abstract class MoveableGameObject : GameObject
    {
        public Vector2 velocity = Vector2.Zero;
        public Vector2 Velocity 
        {
            get { return velocity; }
            protected set { velocity = value; }
        }

        public MoveableGameObject(Vector2 position, Vector2 velocity, Rectangle bounds)
        {
            Position = position;
            Center = new Vector2(position.X + bounds.Width / 2, position.Y + bounds.Height / 2);
            Bounds = bounds;
            IsAlive = true;

            Velocity = velocity;
            this.velocity = velocity;
        }

        public virtual void MoveBy(Vector2 distance)
        {
            Position += distance;
            HitBoxPosition += distance;
            Center += distance;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
            HitBox = new Rectangle((int)HitBoxPosition.X, (int)HitBoxPosition.Y, HitBox.Width, HitBox.Height);
        }

        public virtual void Update(GameTime gameTime)
        {
            Position += Velocity;
            Center += Velocity;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
        }
    }

    public enum GameObjectTypes
    {
        Plattform,
        Item,
        PLayer,
        Enemy
    }
}
