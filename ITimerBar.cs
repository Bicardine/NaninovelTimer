namespace NaninovelTimer
{
    public interface ITimerBar
    {
        void OnTimerUpdate(float elapsedTime, float targetTime);
    }
}