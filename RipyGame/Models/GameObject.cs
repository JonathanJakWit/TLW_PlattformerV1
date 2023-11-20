using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLW_Plattformer.RipyGame.Models
{
    public class GameObject
    {
        public Vector2 Position { get; protected set; }
        public Vector2 Center { get; protected set; }
        public Rectangle Bounds { get; protected set; }

        public bool IsAlive { get; protected set; }

        public GameObject Parent { get; protected set; }
    }

    public class MoveableGameObject : GameObject
    {
        public Vector2 Velocity { get; protected set; }

        public MoveableGameObject(Vector2 position, Vector2 velocity, Rectangle bounds)
        {
            Position = position;
            Center = new Vector2(position.X + bounds.Width / 2, position.Y + bounds.Height / 2);
            Bounds = bounds;
            IsAlive = true;

            Velocity = velocity;
        }

        public virtual void Update(GameTime gameTime)
        {
            Position += Velocity;
            Center += Velocity;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
        }
    }
}
