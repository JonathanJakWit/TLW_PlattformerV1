using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
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
    public class Player : AnimatedSprite
    {
        private PlayerIndex _playerIndex;
        private Keys moveLeftKey;
        private Keys moveRightKey;
        private Keys jumpKey;
        private Keys crouchKey;

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

        private float directionCooldown;
        private Timer directionTimer;
        private float jumpCooldown;
        private Timer jumpTimer;

        private float speed;
        private float jumpSpeed;
        private float fallSpeed;

        public bool IsGrounded { get; set; }
        private Dictionary<PlayerActions, Animation> animations { get; set; }

        public string Name { get; set; }
        public int Score { get; private set; }
        public int Health { get; set; }

        public Player(PlayerIndex playerIndex, AnimationManager animationManager, Vector2 position, Color color, float scale, float speed, float jumpSpeed)
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
                    break;
                case PlayerIndex.Two:
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
            this.canJump = true;
            this.canCrouch = true;

            this.isMoving = false;
            this.isJumping = false;

            this.currentDirectionX = MoveableDirections.Idle;
            this.currentDirectionY = MoveableDirections.Idle;

            this.directionCooldown = 0.5F;
            this.directionTimer = new Timer(1, 1, directionCooldown, GameValues.Time);
            this.jumpCooldown = 1F;
            //this.jumpTimer = new Timer((int)jumpCooldown, GameValues.Time);
            this.jumpTimer = new Timer(1, GameValues.Time);

            IsGrounded = true;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
            animations = animationManager.GetPlayerAnimations();
            //activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);

            // Temp
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
            Name = "Jonathan";
            Health = 10;
            // Temp

            Score = 0;

            this.speed = speed;
            this.jumpSpeed = jumpSpeed;
            this.fallSpeed = speed;
        }

        public void HandleItemInteraction(Item item)
        {

        }

        private void GoIdle()
        {
            isMoving = false;
            Velocity = new Vector2(0, 0);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);
        }
        private void MoveLeft()
        {
            isMoving = true;
            currentDirectionX = MoveableDirections.Left;
            Velocity = new Vector2(-speed, Velocity.Y);
            //canMoveLeft = false; canMoveRight = false;
            directionTimer.Reset(GameValues.Time);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
        }
        private void MoveRight()
        {
            isMoving = true;
            currentDirectionX = MoveableDirections.Right;
            Velocity = new Vector2(speed, Velocity.Y);
            //canMoveLeft = false; canMoveRight = false;
            directionTimer.Reset(GameValues.Time);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
        }
        private void StartJump()
        {
            IsGrounded = false;
            isMoving = true;
            isJumping = true;
            currentDirectionY = MoveableDirections.Up;
            Velocity = new Vector2(Velocity.X, + jumpSpeed);
            canJump = false;
            jumpTimer.Reset(GameValues.Time);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Jump);
        }
        private void EndJump()
        {
            isJumping = false;
            currentDirectionY = MoveableDirections.Down;
            Velocity = new Vector2(Velocity.X, Velocity.Y - jumpSpeed);
        }
        private void Crouch()
        {
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Crouch);
        }

        private void CheckCollisionAndUpdateDirections(Rectangle hitBoxToCheck)
        {
            if (Position.X > hitBoxToCheck.X && Position.X < hitBoxToCheck.X + hitBoxToCheck.Width)
            {
                if (canMoveLeft)
                {
                    if (hitBoxToCheck.X + hitBoxToCheck.Width > Position.X - speed)
                    {
                        canMoveLeft = false;
                    }
                }
                if (canMoveRight)
                {
                    if (Position.X + Bounds.Width + speed > hitBoxToCheck.X)
                    {
                        canMoveRight = false;
                    }
                }
                if (canMoveUp)
                {
                    if (hitBoxToCheck.Y + hitBoxToCheck.Height > Position.Y - Velocity.Y)
                    {
                        canMoveUp = false;
                    }
                }
                if (canJump)
                {
                    if (hitBoxToCheck.Y + hitBoxToCheck.Height > Position.Y - jumpSpeed)
                    {
                        canJump = false;
                    }
                }
                if (!IsGrounded && canMoveDown)
                {
                    if (hitBoxToCheck.Y < Position.Y + Velocity.Y)
                    {
                        canMoveDown = false;
                        IsGrounded = true;
                        Position = new Vector2(Position.X, hitBoxToCheck.Y);
                    }
                }
            }
        }

        public void UpdateAllowedDirections(List<Plattform> plattforms)
        {
            foreach (Plattform plattform in plattforms)
            {
                if (plattform.PlattformType == PlattformTypes.Solid)
                {
                    CheckCollisionAndUpdateDirections(plattform.Bounds);
                }
            }
        }

        private bool IsAnyInput()
        {
            if (GameValues.NewKeyboardState.IsKeyDown(moveLeftKey) || GameValues.NewKeyboardState.IsKeyDown(moveRightKey) || GameValues.NewKeyboardState.IsKeyDown(jumpKey) || GameValues.NewKeyboardState.IsKeyDown(crouchKey))
            {
                return true;
            }
            return false;
        }

        private void UpdateMovementInputs()
        {
            if (!IsAnyInput())
            {
                if (!isMoving)
                {
                    GoIdle(); // Change to check for outside forces affecting movement velocity before going idle
                }
                return;
            }

            if (canMoveLeft)
            {
                if (GameValues.NewKeyboardState.IsKeyDown(moveLeftKey))
                {
                    MoveLeft();
                }
            }
            if (canMoveRight)
            {
                if (GameValues.NewKeyboardState.IsKeyDown(moveRightKey))
                {
                    MoveRight();
                }
            }
            if (canJump)
            {
                if (GameValues.NewKeyboardState.IsKeyDown(jumpKey))
                {
                    StartJump();
                }
            }

            //if (canMoveLeft)
            //{
            //    if (GameValues.IsKeyPressed(moveLeftKey))
            //    {
            //        MoveLeft();
            //        return;
            //    }
            //}
            //if (canMoveRight)
            //{
            //    if (GameValues.IsKeyPressed(moveRightKey))
            //    {
            //        MoveRight();
            //        return;
            //    }
            //}
            //if (canJump)
            //{
            //    if (GameValues.IsKeyPressed(jumpKey))
            //    {
            //        StartJump();
            //        return;
            //    }
            //}
        }

        //public void TestFreePlayer()
        //{
        //    canMoveLeft = true;
        //    canMoveRight = true;
        //    canMoveUp = true;
        //    canMoveDown = true;
        //    canJump = true;
        //}

        public override void Update(GameTime gameTime)
        {
            if (!IsAlive) { return; }

            //if (currentDirectionX == MoveableDirections.Idle && currentDirectionY == MoveableDirections.Idle)
            //{
            //    GoIdle();
            //}

            if (!IsGrounded)
            {
                if (canMoveDown)
                {
                    Position = new Vector2(Position.X, Position.Y + fallSpeed);
                }
                else
                {
                    IsGrounded = true;
                }
            }

            if (Velocity.X == 0 && Velocity.Y == 0)
            {
                //GoIdle();
                isMoving = false;
            }

            //if (!isMoving)
            //{
            //    UpdateMovementInputs();
            //}
            UpdateMovementInputs();

            if (isMoving)
            {
                // Stop movement if player can't move in the direction anymore
                switch (currentDirectionX)
                {
                    case MoveableDirections.Left:
                        if (!canMoveLeft)
                        {
                            Velocity = new Vector2(0, Velocity.Y);
                            currentDirectionX = MoveableDirections.Idle;
                        }
                        break;
                    case MoveableDirections.Right:
                        if (!canMoveRight)
                        {
                            Velocity = new Vector2(0, Velocity.Y);
                            currentDirectionX = MoveableDirections.Idle;
                        }
                        break;

                    default:
                        break;
                }
                switch (currentDirectionY)
                {
                    case MoveableDirections.Up:
                        if (!canMoveUp)
                        {
                            Velocity = new Vector2(Velocity.X, 0);
                            currentDirectionY = MoveableDirections.Idle;
                        }
                        if (!canJump)
                        {
                            Velocity = new Vector2(Velocity.X, 0);
                            currentDirectionY = MoveableDirections.Idle;
                        }
                        break;
                    case MoveableDirections.Down:
                        if (!canMoveDown)
                        {
                            Velocity = new Vector2(Velocity.X, 0);
                            currentDirectionY = MoveableDirections.Idle;
                        }
                        break;

                    default:
                        break;
                }

                //directionTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                //if (directionTimer.TimerFinished)
                //{
                //    isMoving = false;
                //    Velocity = new Vector2(0, Velocity.Y);
                //}

            }
            if (isJumping)
            {
                jumpTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (jumpTimer.TimerFinished)
                {
                    EndJump();
                    //isJumping = false;
                    //Velocity = new Vector2(Velocity.X, Velocity.Y + jumpSpeed);
                }
            }

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
        HealthIdle, Idle,
        MoveLeft, MoveRight,
        Jump, Crouch, Dash, Slide,
        BasicAttack, HeavyAttack, RangedAttack
    }
}
