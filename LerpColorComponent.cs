using NaninovelTimer.UI;
using NaninovelTimer.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace NaninovelTimer
{
    [RequireComponent(typeof(TimerUI))]
    public class LerpColorComponent : MonoBehaviour
    {
        [SerializeField] private Color _from = Color.white;
        [SerializeField] private Color _to = Color.red;

        public UnityEvent<Color> OnLerp;

        private TimerUI _timerUI;

        private void Awake()
        {
            _timerUI = GetComponent<TimerUI>();
            _timerUI.OnTimerUpdate.AddListener(OnTimerUpdate);
        }

        private void OnDestroy()
        {
            _timerUI.OnTimerUpdate.RemoveListener(OnTimerUpdate);
        }

        private void OnTimerUpdate(float elapsedTime, float targetTime)
        {
            var normalizedValue = NaninovelTimerUtils.GetNormalizedValue(elapsedTime, targetTime);
            Lerp(normalizedValue);
        }

        public void Lerp(float normalizedValue)
        {
            var color = Color.Lerp(_from, _to, normalizedValue);
            OnLerp?.Invoke(color);
        }
    }
}