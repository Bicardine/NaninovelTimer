namespace NaninovelTimer.UI.BarRenderers
{
    public abstract class TimerBarRenderer<T> : TimerBarBehaviour, IItemRenderer<T>
    {
        public abstract void Render(T data);
    }
}