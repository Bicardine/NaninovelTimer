using Naninovel;
using NaninovelTimer.UI;
using NaninovelTimer.Utils.Disposables;
using UnityEngine;

namespace NaninovelTimer
{
    public abstract class TimerBarBehaviour : MonoBehaviour, ITimerBar
    {
        private TimerUI _timerUI;

        private readonly NaninovelEngineCompositeDisposable _naniEngineCompositeDisposable = new NaninovelEngineCompositeDisposable();

        private void Awake()
        {
            _naniEngineCompositeDisposable.Retain(InitTimerUI);
        }

        private void InitTimerUI()
        {
            _timerUI = GetTimerUI();
            _timerUI.OnTimerUpdate.AddListener(OnTimerUpdate);
        }

        private void OnDestroy()
        {
            _timerUI.OnTimerUpdate.RemoveListener(OnTimerUpdate);
        }

        private TimerUI GetTimerUI()
        {
            var iUIManager = Engine.GetService<IUIManager>();
            var timerUI = iUIManager.GetUI<TimerUI>();
            return timerUI;
        }


        public abstract void OnTimerUpdate(float elapsedTimeInSeconds, float targetTimeInSeconds);
    }
}