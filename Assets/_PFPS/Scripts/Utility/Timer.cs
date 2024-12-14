using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HeavenFalls
{
    public static class Timer
    {
        private static bool isPaused = false;
        
        public static float CountdownTimer(ref float timeRemaining, float deltaTime)
        {
            if (!isPaused && timeRemaining > 0)
            {
                timeRemaining -= deltaTime;
            }

            return timeRemaining;
        }

        public static float CountUpTimer(ref float timeElapsed, float deltaTime, float maxTime = Mathf.Infinity)
        {
            if (!isPaused && timeElapsed < maxTime)
            {
                timeElapsed += deltaTime;
            }

            return timeElapsed;
        }

        public static bool IsTimerExpired(float time)
        {
            return time <= 0;
        }

        public static void PauseTimer()
        {
            isPaused = true;
        }

        public static void ResumeTimer()
        {
            isPaused = false;
        }
        
        public static string ConvertToMinutesAndSeconds(float time)
        {
            var minutes = Mathf.FloorToInt(time / 60); 
            var seconds = Mathf.FloorToInt(time % 60); 

            return $"{minutes:00}:{seconds:00}"; 
        }
        
        public static void StopAndResetTimer(ref float timer, float resetTime)
        {
            PauseTimer();
            timer = resetTime;
        }
    }
}
