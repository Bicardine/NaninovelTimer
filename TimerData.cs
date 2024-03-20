using System;

namespace NaninovelTimer
{
    [Serializable]
    public class TimerData
    {
        public bool IsRunning;
        public float TargetTimeInSeconds;
        public string ScriptOnTargetTime;
        public float ElapsedTimeInSeconds;

        public TimerData() { }

        public TimerData(bool isRunning, float targetTimeInSeconds, string scriptOnTargetTime, float elapsedTimeInSeconds)
        {
            IsRunning = isRunning;
            TargetTimeInSeconds = targetTimeInSeconds;
            ScriptOnTargetTime = scriptOnTargetTime;
            ElapsedTimeInSeconds = elapsedTimeInSeconds;
        }
    }  
}