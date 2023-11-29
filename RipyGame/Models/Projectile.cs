using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Projectile : MoveableGameObject
    {
        public ProjectileTypes ProjectileType { get; set; }
        public int DamageValue { get; private set; }

        public Projectile(ProjectileTypes projectileType, Vector2 startPos, Vector2 initialVelocity, Rectangle bounds)
            : base(startPos, initialVelocity, bounds)
        {
            this.ProjectileType = projectileType;
            switch (projectileType)
            {
                case ProjectileTypes.FireBall:
                    DamageValue = 3;
                    break;
                case ProjectileTypes.Icicle:
                    DamageValue = 2;
                    break;
                case ProjectileTypes.CrystalShard:
                    DamageValue = 1;
                    break;
                default:
                    break;
            }
        }
    }

    public enum ProjectileTypes
    {
        FireBall,
        Icicle,
        CrystalShard
    }
}
