using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Components;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Managers;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Enemy : AnimatedSprite
    {
        public EnemyTypes EnemyType { get; protected set; }
        private MoveableDirections currentDirection;
        private float speed;
        private Vector2 moveSpeed;
        private Timer switchDirectionTimer;
        private bool switchDirectionEnabled;
        private Timer attackTimer;
        private bool hasProjectiles;
        public List<Projectile> ShotProjectiles { get; protected set; }
        private Vector2 projectileSpeed;
        private int projectileWidth;
        private int projectileHeight;
        private Color projectileColor;
        private Dictionary<EnemyActions, Animation> animations;
        private Texture2D projectileTexture;

        private float og_scale;
        private float moveScale;

        public int Health { get; protected set; }

        public Enemy(EnemyTypes enemyType, TextureManager textureManager, AnimationManager animationManager, Rectangle bounds, Vector2 velocity)
            : base(new(bounds.X, bounds.Y), velocity, bounds, Color.White, GameValues.EnemyScale.X, GameValues.EnemyDrawLayer)
        {
            EnemyType = enemyType;
            currentDirection = MoveableDirections.None;
            switchDirectionEnabled = false;
            hasProjectiles = false;
            animations = animationManager.GetEnemyAnimations(enemyType);
            //activeAnimation = animations.GetValueOrDefault(EnemyActions.Idle);
            activeAnimation = animations.GetValueOrDefault(EnemyActions.Walk);
            ShotProjectiles = new List<Projectile>();
            float thisEnemyScale = 1F;
            int thisEnemyAttackCooldown = 1;
            switch (enemyType)
            {
                case EnemyTypes.CrystalGuardian:
                    Health = 3;
                    currentDirection = MoveableDirections.Left;
                    ChangeSpriteEffect(SpriteEffects.FlipHorizontally);
                    speed = 1;
                    moveSpeed = new Vector2(-speed, 0);
                    switchDirectionTimer = new Timer(5, GameValues.Time);
                    switchDirectionEnabled = true;
                    thisEnemyScale = 0.25F;
                    thisEnemyAttackCooldown = 6;
                    hasProjectiles = true;
                    projectileSpeed = new Vector2(-10, 0);
                    projectileWidth = 10; projectileHeight = 10;
                    projectileColor = Color.Pink;
                    //projectileTexture = textureManager.CrystalShardTex;
                    projectileTexture = textureManager.FullTex;
                    break;
                case EnemyTypes.FrostWraith:
                    break;
                case EnemyTypes.ShadowPhantom:
                    break;
                default:
                    break;
            }
            og_scale = scale * thisEnemyScale;
            moveScale = og_scale * 1.5F;
            scale = moveScale;
            //scale = scale * thisEnemyScale;
            attackTimer = new Timer(thisEnemyAttackCooldown, GameValues.Time);
        }

        public void HandleProjectile(Projectile projectile)
        {
            switch (projectile.ProjectileType)
            {
                case ProjectileTypes.FireBall:
                    Health -= projectile.DamageValue;
                    projectile.IsAlive = false;
                    return;
                case ProjectileTypes.Icicle:
                    return;
                case ProjectileTypes.CrystalShard:
                    return;
                default:
                    break;
            }
        }

        private Projectile GetProjectile(ProjectileTypes projectileType)
        {
            Rectangle projRect = new Rectangle((int)Center.X, (int)Center.Y, projectileWidth, projectileHeight);
            Projectile madeProjectile = new Projectile(projectileType, Center, projectileSpeed, projRect);
            return madeProjectile;
        }

        private void SwitchDirection()
        {
            moveSpeed *= -1;

            if (currentDirection == MoveableDirections.Left)
            {
                currentDirection = MoveableDirections.Right;
            }
            else if (currentDirection == MoveableDirections.Right)
            {
                currentDirection = MoveableDirections.Left;
            }

            //activeAnimation = animations.GetValueOrDefault(EnemyActions.Idle);
            activeAnimation = animations.GetValueOrDefault(EnemyActions.Walk);
            if (spriteEffects == SpriteEffects.None) { 
                ChangeSpriteEffect(SpriteEffects.FlipHorizontally);
            }
            else if (spriteEffects == SpriteEffects.FlipHorizontally) {
                ChangeSpriteEffect(SpriteEffects.None);
            }
        }

        private void Attack()
        {
            switch (EnemyType)
            {
                case EnemyTypes.CrystalGuardian:
                    ShotProjectiles.Add(GetProjectile(ProjectileTypes.CrystalShard));
                    break;
                case EnemyTypes.FrostWraith:
                    break;
                case EnemyTypes.ShadowPhantom:
                    break;
                default:
                    break;
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!IsAlive) { return; }
            if (Health <= 0)
            {
                IsAlive = false;
                LoadedGameLevel.CurrentEnemyAmount--;
                return;
            }

            attackTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            if (attackTimer.TimerFinished)
            {
                Attack();
                attackTimer.Reset((float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            if (switchDirectionEnabled)
            {
                switchDirectionTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (switchDirectionTimer.TimerFinished)
                {
                    SwitchDirection();
                    switchDirectionTimer.Reset((float)gameTime.ElapsedGameTime.TotalSeconds);
                }
                MoveBy(moveSpeed);
            }

            if (hasProjectiles)
            {
                foreach (Projectile projectile in ShotProjectiles)
                {
                    projectile.Update(gameTime);
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsAlive) { return; }

            if (hasProjectiles)
            {
                foreach (Projectile projectile in ShotProjectiles)
                {
                    if (projectile.IsAlive)
                    {
                        spriteBatch.Draw(projectileTexture, projectile.Bounds, projectileColor);
                    }
                }
            }

            base.Draw(spriteBatch);
        }
    }

    public enum EnemyTypes
    {
        CrystalGuardian,
        FrostWraith,
        ShadowPhantom
    }

    public enum EnemyActions
    {
        Idle,
        Walk,
        Attack,
        TakeDamage,
        Die
    }
}
