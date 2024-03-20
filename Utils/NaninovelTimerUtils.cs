namespace NaninovelTimer.Utils
{
    public static class NaninovelTimerUtils
    {
        public static float GetNormalizedValue(float value, float targetValue)
        {
            return value / targetValue;
        }

        public static float GetRemaningTime(float elapsedTimeInSeconds, float targetTimeInSeconds)
        {
            return targetTimeInSeconds - elapsedTimeInSeconds;
        }
    }
}