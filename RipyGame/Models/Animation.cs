using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TLW_Plattformer.RipyGame.Globals;
using TLW_Plattformer.RipyGame.Components;

namespace TLW_Plattformer.RipyGame.Models
{
    public class Animation
    {
        public Timer DurationTimer;
        int durationSeconds;
        private readonly Texture2D _texture;
        public Color _color;
        private readonly List<Rectangle> _sourceRectangles = new List<Rectangle>();
        private readonly int _frames;
        private int _frame;
        private readonly float _frameTime;
        private float _frameTimeLeft;
        private bool _active = true;
        public bool IsRepeating;

        private float _frameWidth;
        private float _frameHeight;

        internal float TextureWidth;
        internal float TextureHeight;

        private SpriteEffects _spriteEffect;

        public Animation(int activeDurationSeconds, Texture2D spriteSheet, int framesX, float frameTime, Color color, Vector2 animationStartPos, bool isRepeating = true, SpriteEffects spriteEffect = SpriteEffects.None, int frameSizeX = 0, int frameSizeY = 0)
        {
            this.DurationTimer = new Timer(activeDurationSeconds, GameValues.Time);
            this.durationSeconds = activeDurationSeconds;

            _texture = spriteSheet;
            _color = color;
            _frameTime = frameTime;
            _frameTimeLeft = _frameTime;
            _frames = framesX;
            _spriteEffect = spriteEffect;
            this.IsRepeating = isRepeating;

            if (frameSizeX != 0)
            {
                _frameWidth = frameSizeX;
            }
            if (frameSizeY != 0)
            {
                _frameHeight = frameSizeY;
            }

            var frameWidth = (int)_frameWidth;
            var frameHeight = (int)_frameHeight;
            for (int i = 0; i < _frames; i++)
            {
                _sourceRectangles.Add(new(i * frameWidth + (int)animationStartPos.X, 0 + (int)animationStartPos.Y, frameWidth, frameHeight));
            }

            this.TextureWidth = frameSizeX;
            this.TextureHeight = frameSizeY;
        }

        public void Stop()
        {
            _active = false;
        }
        public void Start()
        {
            _active = true;
        }

        public void Reset()
        {
            _frame = 0;
            _frameTimeLeft = _frameTime;
            DurationTimer = new Timer(durationSeconds, GameValues.Time);
        }

        public void Update(GameTime gameTime)
        {
            float time = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (!_active && !IsRepeating)
            {
                return;
            }

            if (!IsRepeating && DurationTimer.TimerFinished)
            {
                _active = false;
                return;
            }

            _frameTimeLeft -= time;

            if (_frameTimeLeft <= 0)
            {
                _frameTimeLeft += _frameTime;
                _frame = (_frame + 1) % _frames;
            }
        }

        public void Draw(Vector2 position, SpriteBatch spriteBatch, float scale, float layerIndex, SpriteEffects spriteEffects=SpriteEffects.None)
        {
            if (spriteEffects == SpriteEffects.None)
            {
                // Draw with original animation spriteeffects
                spriteBatch.Draw(_texture, position, _sourceRectangles[_frame], _color, 0, Vector2.Zero, scale, _spriteEffect, layerIndex);
            }
            else
            {
                // Draw with parameter spriteeffects
                spriteBatch.Draw(_texture, position, _sourceRectangles[_frame], _color, 0, Vector2.Zero, scale, spriteEffects, layerIndex);
            }
        }
    }
}
