using NaninovelTimer.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace NaninovelTimer.UI.BarRenderers
{
    public class FillTimerBarRenderer : TimerBarRenderer<float>
    {
        [SerializeField] private Image _image;

        public override void OnTimerUpdate(float elapsedTime, float targetTime)
        {
            var remainingTime = NaninovelTimerUtils.GetRemaningTime(elapsedTime, targetTime);
            var normalizedValue = NaninovelTimerUtils.GetNormalizedValue(remainingTime, targetTime);

            Render(normalizedValue);
        }

        public override void Render(float value)
        {
            _image.fillAmount = value;
        }
    }
}