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
using TLW_Plattformer.RipyGame.Handlers;
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
        private bool isFalling;

        //private float directionCooldown;
        //private Timer directionTimer;
        private int jumpCooldown;
        private Timer jumpTimer;

        //private float speed;
        //private float jumpSpeed;
        //private float fallSpeed;

        private Vector2 moveLeftSpeed;
        private Vector2 moveRightSpeed;
        private Vector2 jumpSpeed;
        private Vector2 fallSpeed;

        public bool IsGrounded { get; set; }
        private Dictionary<PlayerActions, Animation> animations { get; set; }

        public string Name { get; set; }
        public int Score { get; private set; }
        public int Health { get; set; }

        public Player(PlayerIndex playerIndex, AnimationManager animationManager, Vector2 position, Color color, float scale, float moveSpeed, float jumpSpeed, float fallSpeed)
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
            this.isFalling = false;

            this.currentDirectionX = MoveableDirections.Idle;
            this.currentDirectionY = MoveableDirections.Idle;

            //this.directionCooldown = 0.5F;
            //this.directionTimer = new Timer(1, 1, directionCooldown, GameValues.Time);
            //this.jumpTimer = new Timer((int)jumpCooldown, GameValues.Time);
            this.jumpCooldown = 2;
            this.jumpTimer = new Timer(jumpCooldown, GameValues.Time);

            IsGrounded = true;
            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
            animations = animationManager.GetPlayerAnimations();
            //activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);

            // Temp
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);
            Name = "Jonathan";
            Health = 10;
            // Temp

            Score = 0;

            this.moveLeftSpeed = new Vector2(-moveSpeed, 0);
            this.moveRightSpeed = new Vector2(+moveSpeed, 0);
            this.jumpSpeed = new Vector2(0, -jumpSpeed);
            this.fallSpeed = new Vector2(0, +fallSpeed);
        }

        public void HandleItemInteraction(Item item)
        {

        }

        private bool IsPlayerCentered()
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
        private void MovePlayerBy(Vector2 distance)
        {
            if (IsPlayerCentered()) // Player is in the centre of the window
            {
                BackgroundHandler.MoveBy(distance);
            }
            else
            {
                MoveBy(distance);
            }
        }

        private void StartGoIdle()
        {
            //isMoving = false;
            currentDirectionX = MoveableDirections.Idle;
            currentDirectionY = MoveableDirections.Idle;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);
            //Velocity = new Vector2(0, 0);
        }
        private void StartMoveLeft()
        {
            //isMoving = true;
            currentDirectionX = MoveableDirections.Left;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
            ChangeSpriteEffect(SpriteEffects.FlipHorizontally);
            //Velocity = new Vector2(-speed, Velocity.Y);
            //canMoveLeft = false; canMoveRight = false;
            //directionTimer.Reset(GameValues.Time);
        }
        private void StartMoveRight()
        {
            //isMoving = true;
            currentDirectionX = MoveableDirections.Right;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
            ChangeSpriteEffect(SpriteEffects.None);
            //Velocity = new Vector2(speed, Velocity.Y);
            //canMoveLeft = false; canMoveRight = false;
            //directionTimer.Reset(GameValues.Time);
        }
        private void StartJump()
        {
            IsGrounded = false;
            //isMoving = true;
            isJumping = true;
            canJump = false;
            currentDirectionY = MoveableDirections.Up;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Jump);
            jumpTimer.Reset(GameValues.Time);
            //Velocity = new Vector2(Velocity.X, + jumpSpeed);
        }
        private void EndJump()
        {
            isJumping = false;
            //currentDirectionY = MoveableDirections.Down;
            //Velocity = new Vector2(Velocity.X, Velocity.Y - jumpSpeed);
        }
        private void StartCrouch()
        {
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Crouch);
        }
        private void StartFall()
        {
            isFalling = true;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Fall);
        }

        private void UpdateMoveLeft()
        {
            MovePlayerBy(moveLeftSpeed);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
        }
        private void UpdateMoveRight()
        {
            MovePlayerBy(moveRightSpeed);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
        }
        private void UpdateJump()
        {
            MoveBy(jumpSpeed);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Jump);
        }
        private void UpdateFall()
        {
            MoveBy(fallSpeed);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Fall);
        }

        #region VIKTIGT SENARE
        //private void CheckCollisionAndUpdateDirections(Rectangle hitBoxToCheck)
        //{
        //    if (Position.X > hitBoxToCheck.X && Position.X < hitBoxToCheck.X + hitBoxToCheck.Width)
        //    {
        //        if (canMoveLeft)
        //        {
        //            if (hitBoxToCheck.X + hitBoxToCheck.Width > Position.X - speed)
        //            {
        //                canMoveLeft = false;
        //            }
        //        }
        //        if (canMoveRight)
        //        {
        //            if (Position.X + Bounds.Width + speed > hitBoxToCheck.X)
        //            {
        //                canMoveRight = false;
        //            }
        //        }
        //        if (canMoveUp)
        //        {
        //            if (hitBoxToCheck.Y + hitBoxToCheck.Height > Position.Y - Velocity.Y)
        //            {
        //                canMoveUp = false;
        //            }
        //        }
        //        if (canJump)
        //        {
        //            if (hitBoxToCheck.Y + hitBoxToCheck.Height > Position.Y - jumpSpeed)
        //            {
        //                canJump = false;
        //            }
        //        }
        //        if (!IsGrounded && canMoveDown)
        //        {
        //            if (hitBoxToCheck.Y < Position.Y + Velocity.Y)
        //            {
        //                canMoveDown = false;
        //                IsGrounded = true;
        //                Position = new Vector2(Position.X, hitBoxToCheck.Y);
        //            }
        //        }
        //    }
        //}
        #endregion VIKTIGT SENARE

        public void UpdateAllowedDirections()
        {
            #region TEMP TESTING
            canMoveLeft = true;
            canMoveRight = true;
            canMoveUp = true;
            if (Position.Y + Bounds.Height < GameValues.WindowBounds.Height - Bounds.Height - 5)
            {
                canMoveDown = true;
                IsGrounded = false;
            }
            else
            {
                canMoveDown = false;
                IsGrounded = true;
            }

            if (IsGrounded)
            {
                canJump = true;
                canMoveDown = false;
            }

            //if (isJumping && !canMoveDown)
            //{
            //    //canJump = true;
            //    //EndJump();
            //}
            #endregion TEMP TESTING

            #region VIKTIGT SENARE
            //foreach (Plattform plattform in plattforms)
            //{
            //    if (plattform.PlattformType == PlattformTypes.Solid)
            //    {
            //        CheckCollisionAndUpdateDirections(plattform.Bounds);
            //    }
            //}
            #endregion VIKTIGT SENARE
        }

        private bool IsAnyInput()
        {
            if (GameValues.NewKeyboardState.IsKeyDown(moveLeftKey) || GameValues.NewKeyboardState.IsKeyDown(moveRightKey) || GameValues.NewKeyboardState.IsKeyDown(jumpKey) || GameValues.NewKeyboardState.IsKeyDown(crouchKey))
            {
                return true;
            }
            return false;
        }
        private bool IsStationary()
        {
            if (currentDirectionX == MoveableDirections.Idle && currentDirectionY == MoveableDirections.Idle)
            {
                return true;
            }
            return false;
        }

        private void UpdateMovementInputs()
        {
            if (!IsAnyInput())
            {
                if (IsStationary())
                {
                    return;
                }
                else if (currentDirectionX != MoveableDirections.Idle && currentDirectionY != MoveableDirections.Idle)
                {
                    if (!isJumping && !isFalling)
                    {
                        StartGoIdle(); // Change to check for outside forces affecting movement velocity before going idle
                    }
                }
            }

            if (canMoveLeft && GameValues.NewKeyboardState.IsKeyDown(moveLeftKey))
            {
                if (currentDirectionX == MoveableDirections.Left)
                {
                    UpdateMoveLeft();
                }
                else
                {
                    StartMoveLeft();
                }
            }
            if (canMoveRight && GameValues.NewKeyboardState.IsKeyDown(moveRightKey))
            {
                if (currentDirectionX == MoveableDirections.Right)
                {
                    UpdateMoveRight();
                }
                else
                {
                    StartMoveRight();
                }
            }
            if (canJump || isJumping)
            {
                if (isJumping)
                {
                    UpdateJump();
                }
                else if (GameValues.IsKeyPressed(jumpKey))
                {
                    StartJump();
                }
            }
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
            if (IsGrounded)
            {
                isFalling = false;
                canJump = true;
            }

            if (IsGrounded && !IsAnyInput())
            {
                activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);
            }

            if (Velocity.X == 0 && Velocity.Y == 0)
            {
                isMoving = false;
            }
            else
            {
                isMoving = true;
            }

            UpdateMovementInputs();

            if (isJumping)
            {
                jumpTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
                if (jumpTimer.TimerFinished)
                {
                    EndJump();
                }
            }

            if (!IsStationary())
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
            }

            base.Update(gameTime);
        }

        public void UpdatePlayer(GameTime gameTime)
        {
            UpdateAllowedDirections();

            Update(gameTime);
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
        Jump, Fall, Crouch, Dash, Slide,
        BasicAttack, HeavyAttack, RangedAttack
    }
}
