﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Models;

namespace TLW_Plattformer.RipyGame.Managers
{
    public class AnimationManager
    {
        private readonly Animation noneAnim;

        #region Player Animations
        private readonly Animation playerIdleAnim;
        //private readonly Animation playerMoveLeftAnim;
        private readonly Animation playerMoveAnim;
        private readonly Animation playerJumpAnim;
        private readonly Animation playerFallAnim;
        private readonly Animation playerCrouchAnim;
        private readonly Dictionary<PlayerActions, Animation> playerActionAnimations;
        #endregion Player Animations

        #region Enemy Animations
        private readonly Animation crystalGuardianIdleAnim;
        private readonly Animation crystalGuardianMoveAnim;
        private readonly Dictionary<EnemyActions, Animation> crystalGuardianActionAnimations;
        //private readonly Animation frostWraithIdleAnim;
        #endregion Enemy Animations

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
            int playerIdleAnimDurationSeconds = 3;
            int playerIdleFramesX = 6;
            float playerIdleFrameTime = 0.2f;
            Color playerIdleColor = Color.White;
            Vector2 playerIdleStartPos = new Vector2(0, 20);
            bool playerIdleIsRepeating = true;
            SpriteEffects playerIdleSpriteEffects = SpriteEffects.None;
            int playerIdleFrameWidth = 340; int playerIdleFrameHeight = 963;
            this.playerIdleAnim = new Animation
            (
                playerIdleAnimDurationSeconds,
                textureManager.EmberaxIdleSpritesheet,
                playerIdleFramesX,
                playerIdleFrameTime,
                playerIdleColor,
                playerIdleStartPos,
                playerIdleIsRepeating,
                playerIdleSpriteEffects,
                playerIdleFrameWidth, playerIdleFrameHeight
            );

            // Move Animations
            int playerMoveAnimDurationSeconds = 3;
            int playerMoveFramesX = 8;
            float playerMoveFrameTime = 0.1f;
            Color playerMoveColor = Color.White;

            Vector2 playerMoveStartPos = new Vector2(0, 0);
            Vector2 playerJumpStartPos = new Vector2(0, 16);
            Vector2 playerFallStartPos = new Vector2(0, 32);
            Vector2 playerCrouchStartPos = new Vector2(0, 16);

            bool playerMoveIsRepeating = true;
            SpriteEffects playerMoveSpriteEffects = SpriteEffects.None;
            SpriteEffects playerJumpSpriteEffects = SpriteEffects.None;
            SpriteEffects playerCrouchSpriteEffects = SpriteEffects.None;
            int playerMoveFrameWidth = 496; int playerMoveFrameHeight = 617;
            this.playerMoveAnim = new Animation
            (
                playerMoveAnimDurationSeconds,
                textureManager.EmberaxRunningSpritesheet,
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

            #region Enemy Animations
            this.crystalGuardianActionAnimations = new Dictionary<EnemyActions, Animation>();

            // Idle Animation
            int crystalGuardianIdleAnimDurationSeconds = 3;
            int crystalGuardianIdleFramesX = 6;
            float crystalGuardianIdleFrameTime = 0.2f;
            Color crystalGuardianIdleColor = Color.White;
            Vector2 crystalGuardianIdleStartPos = new Vector2(0, 20);
            bool crystalGuardianIdleIsRepeating = true;
            SpriteEffects crystalGuardianIdleSpriteEffects = SpriteEffects.None;
            int crystalGuardianIdleFrameWidth = 340; int crystalGuardianIdleFrameHeight = 963;
            this.crystalGuardianIdleAnim = new Animation
            (
                crystalGuardianIdleAnimDurationSeconds,
                textureManager.CrystalGuardianIdleSpritesheet,
                crystalGuardianIdleFramesX,
                crystalGuardianIdleFrameTime,
                crystalGuardianIdleColor,
                crystalGuardianIdleStartPos,
                crystalGuardianIdleIsRepeating,
                crystalGuardianIdleSpriteEffects,
                crystalGuardianIdleFrameWidth, crystalGuardianIdleFrameHeight
            );

            // Move Animations
            int crystalGuardianMoveAnimDurationSeconds = 3;
            int crystalGuardianMoveFramesX = 8;
            float crystalGuardianMoveFrameTime = 0.1f;
            Color crystalGuardianMoveColor = Color.White;

            Vector2 crystalGuardianMoveStartPos = new Vector2(0, 0);

            bool crystalGuardianMoveIsRepeating = true;
            SpriteEffects crystalGuardianMoveSpriteEffects = SpriteEffects.None;
            int crystalGuardianMoveFrameWidth = 496; int crystalGuardianMoveFrameHeight = 617;
            this.crystalGuardianMoveAnim = new Animation
            (
                crystalGuardianMoveAnimDurationSeconds,
                textureManager.CrystalGuardianRunningSpritesheet,
                crystalGuardianMoveFramesX,
                crystalGuardianMoveFrameTime,
                crystalGuardianMoveColor,
                crystalGuardianMoveStartPos,
                crystalGuardianMoveIsRepeating,
                crystalGuardianMoveSpriteEffects,
                crystalGuardianMoveFrameWidth, crystalGuardianMoveFrameHeight
            );

            crystalGuardianActionAnimations.Add(EnemyActions.Idle, crystalGuardianIdleAnim);
            crystalGuardianActionAnimations.Add(EnemyActions.Walk, crystalGuardianMoveAnim);
            #endregion Enemy Animations
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

        public Dictionary<EnemyActions, Animation> GetEnemyAnimations(EnemyTypes enemyType)
        {
            switch (enemyType)
            {
                case EnemyTypes.CrystalGuardian:
                    return crystalGuardianActionAnimations;
                    
                case EnemyTypes.FrostWraith:
                    break;
                case EnemyTypes.ShadowPhantom:
                    break;
                default:
                    break;
            }

            return crystalGuardianActionAnimations;
        }
    }

    public enum AnimationType
    {
        None,
    }
}
