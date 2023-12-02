using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Components;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Handlers;
using TLW_Plattformer.RipyGame.Managers;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Player : AnimatedSprite
    {
        private PlayerIndex _playerIndex;
        public PlayerIndex PlayerIndex { get { return _playerIndex; } }

        private Keys moveLeftKey;
        private Keys moveRightKey;
        private Keys jumpKey;
        private Keys crouchKey;
        private Keys shootProjectileKey;

        private MoveableDirections currentDirectionX;
        private MoveableDirections currentDirectionY;

        private bool canMoveLeft;
        private bool canMoveRight;
        private bool canMoveUp;
        private bool canMoveDown;
        private bool canJump;
        private bool canCrouch;

        private bool isMoving;
        private bool isJumping;
        private bool isFalling;
        private bool isIdle;

        private int goIdleCooldown;
        private Timer goIdleTimer;
        private int jumpCooldown;
        private Timer jumpTimer;

        private Vector2 moveLeftSpeed;
        private Vector2 moveRightSpeed;
        private Vector2 jumpSpeed;
        private Vector2 fallSpeed;

        public bool IsGrounded { get; set; }
        private Dictionary<PlayerActions, Animation> animations { get; set; }

        private Texture2D fireBallTex;
        private int fireBallWidth;
        private int fireBallHeight;
        private int fireBallSpeed;
        public List<Projectile> ShotProjectiles { get; private set; }

        private Vector2 hudStartPos;

        private Texture2D healthIconTex;
        private int healthIconWidth;
        private int healthIconHeight;
        public string Name { get; set; }
        public int Score { get; set; }
        public int Health { get; set; }

        private float og_scale;
        private float moveScale;
        private float jumpScale;
        private float fallScale;

        public Player(PlayerIndex playerIndex, TextureManager textureManager, AnimationManager animationManager, Vector2 position, Color color, float scale, float moveSpeed, float jumpSpeed, float fallSpeed)
            : base(position, new(0, 0), new((int)position.X, (int)position.Y, GameValues.PlayerBounds.Width, GameValues.PlayerBounds.Height), color, scale, GameValues.PlayerDrawLayer)
        {
            this._playerIndex = playerIndex;
            switch (playerIndex)
            {
                case PlayerIndex.One:
                    moveLeftKey = GameValues.P1_MoveLeftKey;
                    moveRightKey = GameValues.P1_MoveRightKey;
                    jumpKey = GameValues.P1_JumpKey;
                    crouchKey = GameValues.P1_CrouchKey;
                    shootProjectileKey = GameValues.P1_ShootProjectileKey;
                    this.hudStartPos = new Vector2(0, 0);
                    break;
                case PlayerIndex.Two:
                    moveLeftKey = GameValues.P2_MoveLeftKey;
                    moveRightKey = GameValues.P2_MoveRightKey;
                    jumpKey = GameValues.P2_JumpKey;
                    crouchKey = GameValues.P2_CrouchKey;
                    shootProjectileKey = GameValues.P2_ShootProjectileKey;
                    this.hudStartPos = new Vector2(GameValues.WindowSize.X - GameValues.ColumnWidth * 3, 0);
                    break;
                case PlayerIndex.Three:
                    break;
                case PlayerIndex.Four:
                    break;
                default:
                    break;
            }

            this.canMoveLeft = true;
            this.canMoveRight = true;
            this.canMoveUp = true;
            this.canMoveDown = true;

            this.canJump = true;
            this.canCrouch = true;

            this.isMoving = false;
            this.isJumping = false;
            this.isFalling = false;
            this.isIdle = false;
            //this.isMovementStarted = false;
            this.IsGrounded = false;

            this.currentDirectionX = MoveableDirections.Idle;
            this.currentDirectionY = MoveableDirections.Idle;

            this.goIdleCooldown = 1;
            this.goIdleTimer = new Timer(goIdleCooldown, GameValues.Time);
            goIdleTimer.IsActive = false;
            this.jumpCooldown = 2;
            this.jumpTimer = new Timer(1, 3, 0.2F, GameValues.Time);
            //this.jumpTimer = new Timer(jumpCooldown, GameValues.Time);

            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
            HasHitBox = true;

            animations = animationManager.GetPlayerAnimations();

            // Temp
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);
            Name = "Jonathan";
            Health = 3;
            this.healthIconTex = textureManager.HealthIcon;
            this.healthIconWidth = 128;
            this.healthIconHeight = 128;
            // Temp

            Score = 0;

            this.moveLeftSpeed = new Vector2(-moveSpeed, 0);
            this.moveRightSpeed = new Vector2(+moveSpeed, 0);
            this.jumpSpeed = new Vector2(0, -jumpSpeed);
            this.fallSpeed = new Vector2(0, +fallSpeed);

            //this.fireBallTex = textureManager.FullTex;
            this.fireBallTex = textureManager.FireBallTex;
            this.fireBallWidth = 15;
            this.fireBallHeight = 15;
            this.fireBallSpeed = 20;
            this.ShotProjectiles = new List<Projectile>();

            this.og_scale = scale;
            this.moveScale = scale * 1.5F;
            this.jumpScale = scale * 1.5F;
            this.fallScale = scale * 1.5F;
        }

        public void AddScore(int scoreAmount)
        {
            Score += scoreAmount;
        }

        public override void HandleCollision(GameObject other, MoveableDirections collsionDirection)
        {
            if (other is Plattform)
            {
                //if (other.IsDangerous)
                //{
                //    Vector2 knockback = Vector2.Zero;
                //    if (collsionDirection == MoveableDirections.Right)
                //    {
                //        knockback = new Vector2(-Bounds.Width / 2, 0);
                //    }
                //    else if (collsionDirection == MoveableDirections.Left)
                //    {
                //        knockback = new Vector2(Bounds.Width / 2, 0);
                //    }
                //    else // If the player is above or below the dangerous plattform when colliding
                //    {
                //        knockback = new Vector2(Bounds.Height / 2, 0);
                //    }
                //    MovePlayerBy(knockback);
                //    Health--;
                //}
                return;
            }

            if (other is Enemy)
            {
                Vector2 knockback = Vector2.Zero;
                if (collsionDirection == MoveableDirections.Right)
                {
                    knockback = new Vector2(-Bounds.Width / 2, 0);
                }
                else if (collsionDirection == MoveableDirections.Left)
                {
                    knockback = new Vector2(Bounds.Width / 2, 0);
                }
                else // If the player is above or below the enemy when colliding
                {
                    Score += GameValues.EnemyPointValue;
                    other.IsAlive = false;
                    return;
                }
                MovePlayerBy(knockback);
                Health--;
                return;
            }

            if (other is Projectile)
            {
                return;
            }
        }
        public void HandleProjectile(Projectile projectile)
        {
            switch (projectile.ProjectileType)
            {
                case ProjectileTypes.FireBall:
                    return;
                case ProjectileTypes.Icicle:
                    Health -= projectile.DamageValue;
                    projectile.IsAlive = false;
                    return;
                case ProjectileTypes.CrystalShard:
                    Health -= projectile.DamageValue;
                    projectile.IsAlive = false;
                    return;
                default:
                    break;
            }
        }
        public void HandleSpikes(Plattform plattform, MoveableDirections collsionDirection)
        {
            Vector2 knockback = Vector2.Zero;
            if (collsionDirection == MoveableDirections.Right)
            {
                knockback = new Vector2(-Bounds.Width, 0);
            }
            else if (collsionDirection == MoveableDirections.Left)
            {
                knockback = new Vector2(Bounds.Width, 0);
            }
            else // If the player is above or below the dangerous plattform when colliding
            {
                knockback = new Vector2(Bounds.Height / 2, 0);
            }
            MovePlayerBy(knockback);
            Health--;
        }


        private void MovePlayerBy(Vector2 distance)
        {
            MoveBy(distance);
        }

        private void StartGoIdle()
        {
            scale = og_scale;
            isIdle = true;
            velocity.X = 0;
            currentDirectionX = MoveableDirections.Idle;
            currentDirectionY = MoveableDirections.Idle;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);
        }
        private void StartMoveLeft()
        {
            scale = moveScale;
            //isMovementStarted = true;
            isIdle = false;
            if (currentDirectionX == MoveableDirections.Right)
            {
                velocity.X += moveLeftSpeed.X; // Reset it to 0 since it was moving right
            }
            velocity.X += moveLeftSpeed.X;
            currentDirectionX = MoveableDirections.Left;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
            ChangeSpriteEffect(SpriteEffects.FlipHorizontally);
        }
        private void EndMoveLeft()
        {
            //isMovementStarted = false;
            velocity.X -= moveLeftSpeed.X;
        }
        private void StartMoveRight()
        {
            scale = moveScale;
            //isMovementStarted = true;
            isIdle = false;
            if (currentDirectionX == MoveableDirections.Left)
            {
                velocity.X += moveRightSpeed.X; // Reset it to 0 since it was moving left
            }
            velocity.X += moveRightSpeed.X;
            currentDirectionX = MoveableDirections.Right;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
            ChangeSpriteEffect(SpriteEffects.None);
        }
        private void EndMoveRight()
        {
            //isMovementStarted = false;
            velocity.X -= moveRightSpeed.X;
        }
        private void StartJump()
        {
            scale = jumpScale;
            isIdle = false;
            velocity.Y += jumpSpeed.Y;
            IsGrounded = false;
            isJumping = true;
            canJump = false;
            currentDirectionY = MoveableDirections.Up;
            //activeAnimation = animations.GetValueOrDefault(PlayerActions.Jump);
            if (velocity.X < 0)
            {
                activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
            }
            else
            {
                activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
            }
            jumpTimer.Reset(GameValues.Time);
        }
        private void EndJump()
        {
            velocity.Y -= jumpSpeed.Y;
            isJumping = false;
        }
        private void StartCrouch()
        {
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Crouch);
        }
        private void StartFall()
        {
            scale = fallScale;
            isIdle = false;
            velocity.Y += fallSpeed.Y;
            isFalling = true;
            //activeAnimation = animations.GetValueOrDefault(PlayerActions.Fall);
            if (velocity.X < 0)
            {
                activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
            }
            else
            {
                activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
            }
        }
        private void EndFall()
        {
            velocity.Y -= fallSpeed.Y;
            isFalling = false;
        }

        private void UpdateMoveLeft()
        {
            //MovePlayerBy(moveLeftSpeed);
            scale = moveScale;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
        }
        private void UpdateMoveRight()
        {
            //MovePlayerBy(moveRightSpeed);
            scale = moveScale;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
        }
        private void UpdateJump()
        {
            scale = jumpScale;
            //MoveBy(jumpSpeed);
            //activeAnimation = animations.GetValueOrDefault(PlayerActions.Jump);
            if (velocity.X < 0)
            {
                activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
            }
            else
            {
                activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
            }
        }
        private void UpdateFall()
        {
            scale = fallScale;
            //MoveBy(fallSpeed);
            //activeAnimation = animations.GetValueOrDefault(PlayerActions.Fall);
            if (velocity.X < 0)
            {
                activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
            }
            else
            {
                activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
            }
        }

        private void ShootFireball(bool shootUp=false)
        {
            Rectangle fbBounds = new Rectangle((int)Center.X, (int)Center.Y, fireBallWidth, fireBallHeight);
            if (shootUp)
            {
                Vector2 fbSpeed = new Vector2(0, -fireBallSpeed - velocity.Y / 2);
                ShotProjectiles.Add(new Projectile(ProjectileTypes.FireBall, Center, fbSpeed, fbBounds));
            }
            else if (spriteEffects == SpriteEffects.FlipHorizontally) // Looking Left
            {
                Vector2 fbSpeed = new Vector2(-fireBallSpeed - velocity.X / 2, 0);
                ShotProjectiles.Add(new Projectile(ProjectileTypes.FireBall, Center, fbSpeed, fbBounds));
            }
            else if (spriteEffects == SpriteEffects.None) // Looking Right
            {
                Vector2 fbSpeed = new Vector2(fireBallSpeed + velocity.X / 2, 0);
                ShotProjectiles.Add(new Projectile(ProjectileTypes.FireBall, Center, fbSpeed, fbBounds));
            }
        }

        //private void ShootFireball()
        //{
        //    Rectangle fbBounds = new Rectangle((int)Center.X, (int)Center.Y, fireBallWidth, fireBallHeight);
        //    if (currentDirectionX == MoveableDirections.Left)
        //    {
        //        Vector2 fbSpeed = new Vector2(-fireBallSpeed, 0);
        //        ShotProjectiles.Add(new Projectile(ProjectileTypes.FireBall, Center, fbSpeed, fbBounds));
        //    }
        //    else if (currentDirectionX == MoveableDirections.Left)
        //    {
        //        Vector2 fbSpeed = new Vector2(fireBallSpeed, 0);
        //        ShotProjectiles.Add(new Projectile(ProjectileTypes.FireBall, Center, fbSpeed, fbBounds));
        //    }
        //    else // Shot it up
        //    {
        //        Vector2 fbSpeed = new Vector2(0, -fireBallSpeed);
        //        ShotProjectiles.Add(new Projectile(ProjectileTypes.FireBall, Center, fbSpeed, fbBounds));
        //    }
        //}
        

        public void UpdateAllowedDirections()
        {
            #region TEMP TESTING
            if (Position.X < GameValues.LevelBounds.X)
            {
                canMoveLeft = false;
            }
            if (Position.X + Bounds.Width > GameValues.LevelBounds.X + GameValues.LevelBounds.Width)
            {
                canMoveRight = false;
            }

            IsGrounded = false;
            foreach (Plattform plattform in LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.Plattform))
            {
                if (plattform.IsAlive && Bounds.Intersects(plattform.Bounds))
                {
                    MoveableDirections colDir = GameValues.GetCollisionDirection(this, plattform);
                    //Debug.WriteLine(colDir.ToString());
                    if (colDir == MoveableDirections.Left)
                    {
                        canMoveLeft = false;
                    }
                    else if (colDir == MoveableDirections.Right)
                    {
                        canMoveRight = false;
                    }
                    else if (colDir == MoveableDirections.Up)
                    {
                        canMoveUp = false;
                    }
                    else if (colDir == MoveableDirections.Down)
                    {
                        IsGrounded = true;
                    }
                }
            }

            if (!canMoveLeft && velocity.X < 0)
            {
                velocity.X = 0;
            }
            if (!canMoveRight && velocity.X > 0)
            {
                velocity.X = 0;
            }

            if (isJumping && !canMoveUp)
            {
                EndJump();
            }

            if (IsGrounded)
            {
                canMoveDown = false;
            }
            else
            {
                canMoveDown = true;
            }
            #endregion TEMP TESTING
        }

        private bool IsAnyInput()
        {
            if (GameValues.NewKeyboardState.IsKeyDown(moveLeftKey) || GameValues.NewKeyboardState.IsKeyDown(moveRightKey) || GameValues.NewKeyboardState.IsKeyDown(jumpKey) || GameValues.NewKeyboardState.IsKeyDown(crouchKey))
            {
                return true;
            }
            if (IsLeftPressed() || IsRightPressed() || IsJumpPressed() || IsShootPressed())
            {
                return true;
            }

            return false;
        }

        private bool IsLeftPressed()
        {
            if (GameValues.NewKeyboardState.IsKeyDown(moveLeftKey))
            {
                return true;
            }
            if (GamePad.GetState(_playerIndex).DPad.Left == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }
        private bool IsRightPressed()
        {
            if (GameValues.NewKeyboardState.IsKeyDown(moveRightKey))
            {
                return true;
            }
            if (GamePad.GetState(_playerIndex).DPad.Right == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }
        private bool IsJumpPressed()
        {
            if (GameValues.IsKeyPressed(jumpKey))
            {
                return true;
            }
            if (GamePad.GetState(_playerIndex).Buttons.A == ButtonState.Pressed) // Add check if was released
            {
                return true;
            }

            return false;
        }
        private bool IsShootPressed()
        {
            if (GameValues.IsKeyPressed(shootProjectileKey))
            {
                return true;
            }
            if (GamePad.GetState(_playerIndex).Buttons.X == ButtonState.Pressed)
            {
                return true;
            }

            return false;
        }

        private void UpdateInputs()
        {
            if (!IsAnyInput())
            {
                if (!isIdle)
                {
                    goIdleTimer.Reset(GameValues.Time);
                    goIdleTimer.IsActive = true;
                }
                if (goIdleTimer.IsActive)
                {
                    goIdleTimer.Update(GameValues.Time);
                    if (goIdleTimer.TimerFinished)
                    {
                        StartGoIdle();
                    }
                }
            }

            if (canMoveLeft && IsLeftPressed())
            {
                if (currentDirectionX == MoveableDirections.Left) { UpdateMoveLeft(); }
                else { StartMoveLeft(); }
            }
            if (canMoveRight && IsRightPressed())
            {
                if (currentDirectionX == MoveableDirections.Right) { UpdateMoveRight(); }
                else { StartMoveRight(); }
            }
            if (canMoveUp && canJump && IsJumpPressed())
            {
                if (isJumping) { UpdateJump(); }
                else { StartJump(); }
            }

            if (IsShootPressed())
            {
                if (GameValues.NewKeyboardState.IsKeyDown(Keys.W))
                {
                    ShootFireball(true);
                }
                else
                {
                    ShootFireball();
                }
            }
        }

        #region V2
        public override void Update(GameTime gameTime)
        {
            if (!IsAlive) { return; }
            if (Health <= 0)
            {
                IsAlive = false;
                return;
            }

            UpdateAllowedDirections();
            UpdateInputs();

            foreach (Projectile projectile in ShotProjectiles)
            {
                if (projectile.IsAlive)
                {
                    projectile.Update(gameTime);
                }
            }

            if (!IsGrounded)
            {
                if (canMoveDown)
                {
                    if (isFalling)
                    {
                        UpdateFall();
                    }
                    else
                    {
                        StartFall();
                    }
                }
            }
            else
            {
                if (isFalling)
                {
                    EndFall();
                }

                canJump = true;
            }

            if (isJumping)
            {
                jumpTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (jumpTimer.TimerFinished)
                {
                    EndJump();
                }
            }

            canMoveLeft = true;
            canMoveRight = true;
            canMoveUp = true;
            canMoveDown = true;

            base.Update(gameTime);
        }
        #endregion V2

        public override void Draw(SpriteBatch spriteBatch)
        {
            // Draw health icons and points
            for (int i = 0; i < Health; i++)
            {
                Vector2 curHpIconPos = Vector2.Zero;
                if (_playerIndex == PlayerIndex.One)
                {
                    //curHpIconPos = new Vector2(hudStartPos.X + LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer)[0].Position.X - GameValues.WindowCenter.X + healthIconWidth * i, hudStartPos.Y + GameValues.TileHeight / 4);
                    curHpIconPos = new Vector2(hudStartPos.X + LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer)[0].Position.X - GameValues.WindowCenter.X + healthIconWidth * i, hudStartPos.Y + healthIconHeight / 2);
                }
                else
                {
                    //curHpIconPos = new Vector2(LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer)[0].Position.X + GameValues.WindowCenter.X - healthIconWidth - healthIconWidth * i, hudStartPos.Y + GameValues.TileHeight / 4);
                    curHpIconPos = new Vector2(LoadedGameLevel.GameObjects.GetValueOrDefault(GameObjectTypes.PLayer)[0].Position.X + GameValues.WindowCenter.X - healthIconWidth - healthIconWidth * i, hudStartPos.Y + healthIconHeight / 2);
                }
                Rectangle curHpIconDestRect = new Rectangle((int)curHpIconPos.X, (int)curHpIconPos.Y, healthIconWidth, healthIconHeight);
                spriteBatch.Draw(healthIconTex, curHpIconDestRect, Color.White);
            }

            foreach (Projectile projectile in ShotProjectiles)
            {
                if (projectile.IsAlive)
                {
                    spriteBatch.Draw(fireBallTex, projectile.Bounds, Color.OrangeRed);
                }
            }

            base.Draw(spriteBatch);
        }
    }

    public enum PlayerActions
    {
        HealthIdle, Idle,
        MoveLeft, MoveRight,
        Jump, Fall, Crouch, Dash, Slide,
        BasicAttack, HeavyAttack, RangedAttack
    }
}
