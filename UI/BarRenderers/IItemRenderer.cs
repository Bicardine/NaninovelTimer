namespace NaninovelTimer.UI.BarRenderers
{
    public interface IItemRenderer<TDataType>
    {
        public void Render(TDataType data);
    }

}