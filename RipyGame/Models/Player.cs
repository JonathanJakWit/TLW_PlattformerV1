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
        //private bool isMovementStarted;

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
            this.canMoveUp = true;
            this.canMoveDown = true;

            this.canJump = true;
            this.canCrouch = true;

            this.isMoving = false;
            this.isJumping = false;
            this.isFalling = false;
            //this.isMovementStarted = false;
            this.IsGrounded = false;

            this.currentDirectionX = MoveableDirections.Idle;
            this.currentDirectionY = MoveableDirections.Idle;

            this.goIdleCooldown = 1;
            this.goIdleTimer = new Timer(goIdleCooldown, GameValues.Time);
            goIdleTimer.IsActive = false;
            this.jumpCooldown = 2;
            this.jumpTimer = new Timer(jumpCooldown, GameValues.Time);

            Bounds = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
            HitBox = new Rectangle((int)Position.X, (int)Position.Y, Bounds.Width, Bounds.Height);
            HasHitBox = true;

            animations = animationManager.GetPlayerAnimations();

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

        public override void HandleCollision(GameObject other, MoveableDirections collsionDirection)
        {
            if (other is Plattform)
            {
                return;
                //if (Center.Y < other.Center.Y)
                //{
                //    IsGrounded = true;
                //    canMoveDown = false;
                //}
                //else if (Center.Y > other.Center.Y)
                //{
                //    canMoveUp = false;
                //    canJump = false;
                //}
                //else if (Position.X + Bounds.Width < other.Center.X)
                //{
                //    canMoveRight = false;
                //}
                //else if (Center.X > other.Position.X + other.Bounds.Width)
                //{
                //    canMoveLeft = false;
                //}

                //switch (otherRelativePos)
                //{
                //    case MoveableDirections.Left:
                //        canMoveLeft = false;
                //        break;
                //    case MoveableDirections.Right:
                //        canMoveRight = false;
                //        break;
                //    case MoveableDirections.Up:
                //        canMoveUp = false;
                //        break;
                //    case MoveableDirections.Down:
                //        canMoveDown = false;
                //        IsGrounded = true;
                //        break;
                //    case MoveableDirections.Idle:
                //        break;
                //    default:
                //        break;
                //}
            }

            //base.HandleCollision(other);

            //if (other is Player)
            //{
            //    return;
            //}
            //else if (other is Plattform)
            //{
            //    if (!IsGrounded)
            //    {
            //        canMoveDown = false;
            //        IsGrounded = true;
            //    }
            //}
        }

        private void MovePlayerBy(Vector2 distance)
        {
            MoveBy(distance);
        }

        private void StartGoIdle()
        {
            velocity = Vector2.Zero;
            currentDirectionX = MoveableDirections.Idle;
            currentDirectionY = MoveableDirections.Idle;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);
        }
        private void StartMoveLeft()
        {
            //isMovementStarted = true;
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
            //isMovementStarted = true;
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
            velocity.Y += jumpSpeed.Y;
            IsGrounded = false;
            isJumping = true;
            canJump = false;
            currentDirectionY = MoveableDirections.Up;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Jump);
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
            velocity.Y += fallSpeed.Y;
            isFalling = true;
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Fall);
        }
        private void EndFall()
        {
            velocity.Y -= fallSpeed.Y;
            isFalling = false;
        }

        private void UpdateMoveLeft()
        {
            //MovePlayerBy(moveLeftSpeed);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveLeft);
        }
        private void UpdateMoveRight()
        {
            //MovePlayerBy(moveRightSpeed);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.MoveRight);
        }
        private void UpdateJump()
        {
            //MoveBy(jumpSpeed);
            activeAnimation = animations.GetValueOrDefault(PlayerActions.Jump);
        }
        private void UpdateFall()
        {
            //MoveBy(fallSpeed);
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
                if (Bounds.Intersects(plattform.Bounds))
                {
                    MoveableDirections colDir = GameValues.GetCollisionDirection(this, plattform);
                    Debug.WriteLine(colDir.ToString());
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
            if (false) // Check controller input
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

        private void UpdateInputs()
        {
            if (!IsAnyInput())
            {
                if (goIdleTimer.IsActive)
                {
                    goIdleTimer.Update(GameValues.Time);
                    if (goIdleTimer.TimerFinished)
                    {
                        if (currentDirectionX == MoveableDirections.Left)
                        {
                            EndMoveLeft();
                        }
                        else if (currentDirectionX == MoveableDirections.Right)
                        {
                            EndMoveRight();
                        }
                        //goIdleTimer.IsActive = false;
                    }
                }
                else
                {
                    goIdleTimer.Reset(GameValues.Time);
                    goIdleTimer.IsActive = true;
                }

                return;
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
        }

        #region V2
        public override void Update(GameTime gameTime)
        {
            if (!IsAlive) { return; }

            UpdateAllowedDirections();
            UpdateInputs();

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

            //if (velocity.X != 0 && velocity.Y != 0)
            //{
            //    isMoving = true;
            //}
            //if (isMoving) { }
            //else
            //{
            //    StartGoIdle();
            //}

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

        #region V1
        //private bool IsStationary()
        //{
        //    if (currentDirectionX == MoveableDirections.Idle && currentDirectionY == MoveableDirections.Idle)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        //private void UpdateMovementInputs()
        //{
        //    if (!IsAnyInput())
        //    {
        //        if (IsStationary())
        //        {
        //            return;
        //        }
        //        else if (currentDirectionX != MoveableDirections.Idle && currentDirectionY != MoveableDirections.Idle)
        //        {
        //            if (!isJumping && !isFalling)
        //            {
        //                StartGoIdle(); // Change to check for outside forces affecting movement velocity before going idle
        //            }
        //        }
        //    }

        //    if (canMoveLeft && GameValues.NewKeyboardState.IsKeyDown(moveLeftKey))
        //    {
        //        if (currentDirectionX == MoveableDirections.Left)
        //        {
        //            UpdateMoveLeft();
        //        }
        //        else
        //        {
        //            StartMoveLeft();
        //        }
        //    }
        //    if (canMoveRight && GameValues.NewKeyboardState.IsKeyDown(moveRightKey))
        //    {
        //        if (currentDirectionX == MoveableDirections.Right)
        //        {
        //            UpdateMoveRight();
        //        }
        //        else
        //        {
        //            StartMoveRight();
        //        }
        //    }
        //    if (canJump || isJumping)
        //    {
        //        if (isJumping)
        //        {
        //            UpdateJump();
        //        }
        //        else if (GameValues.IsKeyPressed(jumpKey))
        //        {
        //            StartJump();
        //        }
        //    }
        //}

        //public override void Update(GameTime gameTime)
        //{
        //    if (!IsAlive) { return; }

        //    if (!IsGrounded)
        //    {
        //        if (canMoveDown)
        //        {
        //            if (isFalling)
        //            {
        //                UpdateFall();
        //            }
        //            else
        //            {
        //                StartFall();
        //            }
        //        }
        //    }
        //    if (IsGrounded)
        //    {
        //        isFalling = false;
        //        canJump = true;
        //    }

        //    if (IsGrounded && !IsAnyInput())
        //    {
        //        activeAnimation = animations.GetValueOrDefault(PlayerActions.Idle);
        //    }

        //    if (Velocity.X == 0 && Velocity.Y == 0)
        //    {
        //        isMoving = false;
        //    }
        //    else
        //    {
        //        isMoving = true;
        //    }

        //    UpdateMovementInputs();

        //    if (isJumping)
        //    {
        //        jumpTimer.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
        //        if (jumpTimer.TimerFinished)
        //        {
        //            EndJump();
        //        }
        //    }

        //    if (!IsStationary())
        //    {
        //        // Stop movement if player can't move in the direction anymore
        //        switch (currentDirectionX)
        //        {
        //            case MoveableDirections.Left:
        //                if (!canMoveLeft)
        //                {
        //                    Velocity = new Vector2(0, Velocity.Y);
        //                    currentDirectionX = MoveableDirections.Idle;
        //                }
        //                break;
        //            case MoveableDirections.Right:
        //                if (!canMoveRight)
        //                {
        //                    Velocity = new Vector2(0, Velocity.Y);
        //                    currentDirectionX = MoveableDirections.Idle;
        //                }
        //                break;

        //            default:
        //                break;
        //        }
        //        switch (currentDirectionY)
        //        {
        //            case MoveableDirections.Up:
        //                if (!canMoveUp)
        //                {
        //                    Velocity = new Vector2(Velocity.X, 0);
        //                    currentDirectionY = MoveableDirections.Idle;
        //                }
        //                break;
        //            case MoveableDirections.Down:
        //                if (!canMoveDown)
        //                {
        //                    Velocity = new Vector2(Velocity.X, 0);
        //                    currentDirectionY = MoveableDirections.Idle;
        //                }
        //                break;

        //            default:
        //                break;
        //        }
        //    }

        //    base.Update(gameTime);
        //}

        //public void UpdatePlayer(GameTime gameTime)
        //{
        //    UpdateAllowedDirections();
        //    Update(gameTime);
        //    canMoveLeft = true;
        //    canMoveRight = true;
        //    canMoveUp = true;
        //    canMoveDown = true;
        //}
        #endregion V1

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
