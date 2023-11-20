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
        private readonly Animation playerMoveLeftAnim;
        private readonly Animation playerMoveRightAnim;
        private readonly Animation playerMoveUpAnim;
        private readonly Animation playerMoveDownAnim;
        private readonly Dictionary<PlayerActions, Animation> playerActionAnimations;

        private readonly Animation Anim;

        public AnimationManager(ContentManager contentManager, TextureManager textureManager)
        {
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
                textureManager.GetSpriteSheet(TextureTypes.NoneSpriteSheet),
                noneFramesX,
                noneFrameTime,
                noneColor,
                noneStartPos,
                noneIsRepeating,
                noneSpriteEffects,
                noneFrameWidth, noneFrameHeight
            );

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
                textureManager.GetSpriteSheet(TextureTypes.PlayerSpriteSheet),
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

            Vector2 playerMoveLeftStartPos = new Vector2(0, 0);
            Vector2 playerMoveRightStartPos = new Vector2(0, 0);
            Vector2 playerMoveUpStartPos = new Vector2(0, 16);
            Vector2 playerMoveDownStartPos = new Vector2(0, 16);

            bool playerMoveIsRepeating = true;
            SpriteEffects playerMoveLeftSpriteEffects = SpriteEffects.FlipHorizontally;
            SpriteEffects playerMoveRightSpriteEffects = SpriteEffects.None;
            SpriteEffects playerMoveUpSpriteEffects = SpriteEffects.None;
            SpriteEffects playerMoveDownSpriteEffects = SpriteEffects.FlipVertically;
            int playerMoveFrameWidth = 16; int playerMoveFrameHeight = 16;
            this.playerMoveLeftAnim = new Animation
            (
                playerMoveAnimDurationSeconds,
                textureManager.GetSpriteSheet(TextureTypes.PlayerSpriteSheet),
                playerMoveFramesX,
                playerMoveFrameTime,
                playerMoveColor,
                playerMoveLeftStartPos,
                playerMoveIsRepeating,
                playerMoveLeftSpriteEffects,
                playerMoveFrameWidth, playerMoveFrameHeight
            );
            this.playerMoveRightAnim = new Animation
            (
                playerMoveAnimDurationSeconds,
                textureManager.GetSpriteSheet(TextureTypes.PlayerSpriteSheet),
                playerMoveFramesX,
                playerMoveFrameTime,
                playerMoveColor,
                playerMoveRightStartPos,
                playerMoveIsRepeating,
                playerMoveRightSpriteEffects,
                playerMoveFrameWidth, playerMoveFrameHeight
            );
            this.playerMoveUpAnim = new Animation
            (
                playerMoveAnimDurationSeconds,
                textureManager.GetSpriteSheet(TextureTypes.PlayerSpriteSheet),
                playerMoveFramesX,
                playerMoveFrameTime,
                playerMoveColor,
                playerMoveUpStartPos,
                playerMoveIsRepeating,
                playerMoveUpSpriteEffects,
                playerMoveFrameWidth, playerMoveFrameHeight
            );
            this.playerMoveDownAnim = new Animation
            (
                playerMoveAnimDurationSeconds,
                textureManager.GetSpriteSheet(TextureTypes.PlayerSpriteSheet),
                playerMoveFramesX,
                playerMoveFrameTime,
                playerMoveColor,
                playerMoveDownStartPos,
                playerMoveIsRepeating,
                playerMoveDownSpriteEffects,
                playerMoveFrameWidth, playerMoveFrameHeight
            );

            //this.playerActionAnimations.Add(PlayerActions.Idle, playerIdleAnim);
            this.playerActionAnimations.Add(PlayerActions.HealthIdle, playerIdleAnim); // Ändra till en icon anim senare
            this.playerActionAnimations.Add(PlayerActions.Idle, playerMoveLeftAnim); // Ändra till en idle anim senare
            this.playerActionAnimations.Add(PlayerActions.MoveLeft, playerMoveLeftAnim);
            this.playerActionAnimations.Add(PlayerActions.MoveRight, playerMoveRightAnim);
            this.playerActionAnimations.Add(PlayerActions.MoveUp, playerMoveUpAnim);
            this.playerActionAnimations.Add(PlayerActions.MoveDown, playerMoveDownAnim);
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
