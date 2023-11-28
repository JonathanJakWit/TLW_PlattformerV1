using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TLW_Plattformer.RipyGame.Components
{
    public class Timer
    {
        public bool TimerFinished;
        public bool IsActive;

        private int timeCounter;
        private int timeLimit;
        private float countDuration;
        private float currentTime;

        private int original_timeCounter;
        private int original_timeLimit;
        private float original_countDuration;

        public Timer(int timeCounter, int timeLimit, float countDuration, float currentTime)
        {
            this.TimerFinished = false;
            this.IsActive = true;

            this.timeCounter = timeCounter;
            this.timeLimit = timeLimit;
            this.countDuration = countDuration;
            this.currentTime = currentTime;

            this.original_timeCounter = timeCounter;
            this.original_timeLimit = timeLimit;
            this.original_countDuration = countDuration;
        }

        public Timer(int timeLimitSeconds, float currentGameTime)
        {
            this.TimerFinished = false;
            this.IsActive = true;

            this.timeCounter = 1;
            this.timeLimit = timeLimitSeconds;
            this.countDuration = 1f;
            this.currentTime = currentGameTime;

            this.original_timeCounter = timeCounter;
            this.original_timeLimit = timeLimit;
            this.original_countDuration = countDuration;
        }

        public void Update(float gameTimeSeconds)
        {
            currentTime += gameTimeSeconds;

            if (currentTime >= countDuration)
            {
                timeCounter++;
                currentTime -= countDuration;
            }
            if (timeCounter >= timeLimit)
            {
                timeCounter = 0;
                TimerFinished = true;
            }
        }

        public void Reset(float currentTimeSeconds)
        {
            TimerFinished = false;
            timeCounter = original_timeCounter;
            timeLimit = original_timeLimit;
            countDuration = original_countDuration;
            currentTime = currentTimeSeconds;
        }
    }
}
