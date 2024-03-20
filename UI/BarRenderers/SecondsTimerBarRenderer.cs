using NaninovelTimer.Utils;
using TMPro;
using UnityEngine;

namespace NaninovelTimer.UI.BarRenderers
{
    public class SecondsTimerBarRenderer : TimerBarRenderer<string>
    {
        [SerializeField] private TMP_Text _output;

        public override void OnTimerUpdate(float elapsedTime, float targetTime)
        {
            var remainingTimeInSeconds = NaninovelTimerUtils.GetRemaningTime(elapsedTime, targetTime);
            var stringRemainingSeconds = ((int)remainingTimeInSeconds).ToString();

            Render(stringRemainingSeconds);
        }

        public override void Render(string value)
        {
            _output.SetText(value);
        }
    }
}