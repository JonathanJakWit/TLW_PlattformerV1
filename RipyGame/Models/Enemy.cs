using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Managers;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Enemy : AnimatedSprite
    {
        public EnemyTypes EnemyType { get; protected set; }
        // List of projectiles

        public Enemy(EnemyTypes enemyType, AnimationManager animationManager, Rectangle bounds, Vector2 velocity)
            : base(new(bounds.X, bounds.Y), velocity, bounds, Color.White, GameValues.EnemyScale.X, GameValues.EnemyDrawLayer)
        {
            EnemyType = enemyType;
        }
    }

    public enum EnemyTypes
    {
        CrystalGuardian,
        FrostWraith,
        ShadowPhantom
    }
}
