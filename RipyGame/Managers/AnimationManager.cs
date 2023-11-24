using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Models;

namespace TLW_Plattformer.RipyGame.Managers
{
    public class AnimationManager
    {
        private readonly Animation noneAnim;

        private readonly Animation playerIdleAnim;
        //private readonly Animation playerMoveLeftAnim;
        private readonly Animation playerMoveAnim;
        private readonly Animation playerJumpAnim;
        private readonly Animation playerFallAnim;
        private readonly Animation playerCrouchAnim;
        private readonly Dictionary<PlayerActions, Animation> playerActionAnimations;

        private readonly Animation Anim;

        public AnimationManager(ContentManager contentManager, TextureManager textureManager)
        {
            #region None Anim
            int noneAnimDurationSeconds = 1;
            int noneFramesX = 1;
            float noneFrameTime = 0.1f;
            Color noneColor = Color.White;
            Vector2 noneStartPos = new Vector2(0, 0);
            bool noneIsRepeating = true;
            SpriteEffects noneSpriteEffects = SpriteEffects.None;
            int noneFrameWidth = 8; int noneFrameHeight = 8;
            this.noneAnim = new Animation
            (
                noneAnimDurationSeconds,
                textureManager.NoneTex,
                noneFramesX,
                noneFrameTime,
                noneColor,
                noneStartPos,
                noneIsRepeating,
                noneSpriteEffects,
                noneFrameWidth, noneFrameHeight
            );
            #endregion None Anim

            #region Player Animations
            this.playerActionAnimations = new Dictionary<PlayerActions, Animation>();

            // Idle Animation
            int playerIdleAnimDurationSeconds = 1;
            int playerIdleFramesX = 1;
            float playerIdleFrameTime = 0.5f;
            Color playerIdleColor = Color.White;
            Vector2 playerIdleStartPos = new Vector2(0, 32);
            bool playerIdleIsRepeating = true;
            SpriteEffects playerIdleSpriteEffects = SpriteEffects.None;
            int playerIdleFrameWidth = 16; int playerIdleFrameHeight = 16;
            this.playerIdleAnim = new Animation
            (
                playerIdleAnimDurationSeconds,
                textureManager.PlayerSpritesheet,
                playerIdleFramesX,
                playerIdleFrameTime,
                playerIdleColor,
                playerIdleStartPos,
                playerIdleIsRepeating,
                playerIdleSpriteEffects,
                playerIdleFrameWidth, playerIdleFrameHeight
            );

            // Move Animations
            int playerMoveAnimDurationSeconds = 1;
            int playerMoveFramesX = 2;
            float playerMoveFrameTime = 0.25f;
            Color playerMoveColor = Color.White;

            //Vector2 playerMoveLeftStartPos = new Vector2(0, 0);
            Vector2 playerMoveStartPos = new Vector2(0, 0);
            Vector2 playerJumpStartPos = new Vector2(0, 16);
            Vector2 playerFallStartPos = new Vector2(0, 32);
            Vector2 playerCrouchStartPos = new Vector2(0, 16);

            bool playerMoveIsRepeating = true;
            //SpriteEffects playerMoveLeftSpriteEffects = SpriteEffects.FlipHorizontally;
            SpriteEffects playerMoveSpriteEffects = SpriteEffects.None;
            SpriteEffects playerJumpSpriteEffects = SpriteEffects.None;
            SpriteEffects playerCrouchSpriteEffects = SpriteEffects.None;
            int playerMoveFrameWidth = 16; int playerMoveFrameHeight = 16;
            //this.playerMoveLeftAnim = new Animation
            //(
            //    playerMoveAnimDurationSeconds,
            //    textureManager.PlayerSpritesheet,
            //    playerMoveFramesX,
            //    playerMoveFrameTime,
            //    playerMoveColor,
            //    playerMoveLeftStartPos,
            //    playerMoveIsRepeating,
            //    playerMoveLeftSpriteEffects,
            //    playerMoveFrameWidth, playerMoveFrameHeight
            //);
            this.playerMoveAnim = new Animation
            (
                playerMoveAnimDurationSeconds,
                textureManager.PlayerSpritesheet,
                playerMoveFramesX,
                playerMoveFrameTime,
                playerMoveColor,
                playerMoveStartPos,
                playerMoveIsRepeating,
                playerMoveSpriteEffects,
                playerMoveFrameWidth, playerMoveFrameHeight
            );
            this.playerJumpAnim = new Animation
            (
                playerMoveAnimDurationSeconds,
                textureManager.PlayerSpritesheet,
                playerMoveFramesX,
                playerMoveFrameTime,
                playerMoveColor,
                playerJumpStartPos,
                playerMoveIsRepeating,
                playerJumpSpriteEffects,
                playerMoveFrameWidth, playerMoveFrameHeight
            );
            this.playerFallAnim = new Animation
            (
                playerMoveAnimDurationSeconds,
                textureManager.PlayerSpritesheet,
                playerMoveFramesX,
                playerMoveFrameTime,
                playerMoveColor,
                playerFallStartPos,
                playerMoveIsRepeating,
                playerJumpSpriteEffects,
                playerMoveFrameWidth, playerMoveFrameHeight
            );
            this.playerCrouchAnim = new Animation
            (
                playerMoveAnimDurationSeconds,
                textureManager.PlayerSpritesheet,
                playerMoveFramesX,
                playerMoveFrameTime,
                playerMoveColor,
                playerCrouchStartPos,
                playerMoveIsRepeating,
                playerCrouchSpriteEffects,
                playerMoveFrameWidth, playerMoveFrameHeight
            );

            //this.playerActionAnimations.Add(PlayerActions.Idle, playerIdleAnim);
            this.playerActionAnimations.Add(PlayerActions.HealthIdle, playerIdleAnim); // Ändra till en icon anim senare
            this.playerActionAnimations.Add(PlayerActions.Idle, playerIdleAnim);
            this.playerActionAnimations.Add(PlayerActions.MoveLeft, playerMoveAnim);
            this.playerActionAnimations.Add(PlayerActions.MoveRight, playerMoveAnim);
            this.playerActionAnimations.Add(PlayerActions.Jump, playerJumpAnim);
            this.playerActionAnimations.Add(PlayerActions.Fall, playerFallAnim);
            this.playerActionAnimations.Add(PlayerActions.Crouch, playerCrouchAnim);
            #endregion Player Animations
        }

        public Animation GetAnimation(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.None:
                    return noneAnim;

                default:
                    break;
            }

            return noneAnim;
        }

        public Dictionary<PlayerActions, Animation> GetPlayerAnimations()
        {
            return playerActionAnimations;
        }
    }

    public enum AnimationType
    {
        None,
    }
}
