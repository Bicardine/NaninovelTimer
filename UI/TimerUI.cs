using Naninovel;
using Naninovel.UI;
using NaninovelTimer.Utils.Disposables;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Naninovel.Commands;
using NaninovelTimer.Utils;

namespace NaninovelTimer.UI
{
    public class TimerUI : CustomUI
    {
        [Tooltip("Will automatically execute @PurgeRollback while using the TimerUI")]
        [SerializeField] private bool _autoPurgeRollback = true;
        [Tooltip("Wait in seconds after the end of the main time before executing OnTargetTime")]
        [SerializeField][Range(0, 3)] private float _extraSeconds = 0.35f;
        [SerializeField] private UnityEvent _onTargetTime;

        private TimerData _data = new TimerData();

        private Coroutine _timerCoroutine;

        private NaninovelEngineCompositeDisposable _naniEngineCompositeDisposable = new NaninovelEngineCompositeDisposable();

        private List<ChoiceHandlerPanel> _choiceHandlerPanels = new List<ChoiceHandlerPanel>();

        private bool elapsedTimeIsLessThanTargetTime => _data.ElapsedTimeInSeconds < _data.TargetTimeInSeconds;

        private const float DEFAULT_ELAPSED_TIME_IN_SECONDS = 0f;

        public UnityEvent<float, float> OnTimerUpdate;

        [Serializable]
        private new class GameState
        {
            public TimerData Data;
        }

        protected override void Start()
        {
            base.Start();

            _naniEngineCompositeDisposable.Retain(SubscribeOnActorChoiceHandlerPanel);
        }

        private void PurgeRollbackIfAutoEnabled()
        {
            if (_autoPurgeRollback)
                Engine.GetService<IStateManager>()?.PurgeRollbackData();
        }

        private void SubscribeOnActorChoiceHandlerPanel()
        {
            var iUIManager = Engine.GetService<IUIManager>();
            Engine.GetService<IChoiceHandlerManager>().OnActorAdded += OnActorAdded;
            Engine.GetService<IChoiceHandlerManager>().OnActorRemoved += OnActorRemoved;
        }

        private void OnActorAdded(string actorId)
        {
            var iChoiceHandlerManager = Engine.GetService<IChoiceHandlerManager>();
            var iChoiceHandlerActor = iChoiceHandlerManager.GetActor(actorId);
            var choiceHandlerPanel = Engine.GetService<IUIManager>().GetUI(actorId) as ChoiceHandlerPanel;
            choiceHandlerPanel.OnChoice += OnChoice;
            _choiceHandlerPanels.Add(choiceHandlerPanel);
        }

        private void OnActorRemoved(string actorId)
        {
            var panel = _choiceHandlerPanels.First(panel => panel.name == actorId);
            if (panel != null) UnsubscribeOnChoiceAndRemovePanel(panel);
        }

        private void UnsubscribeOnChoiceAndRemovePanel(ChoiceHandlerPanel panel)
        {
            panel.OnChoice -= OnChoice;
            _choiceHandlerPanels.Remove(panel);
        }

        private void OnChoice(ChoiceState state)
        {
            Hide();
            PurgeRollbackIfAutoEnabled();
            SetRunningFalseAndSerializeState();
        }

        private void SetRunningFalseAndSerializeState()
        {
            var iStateManager = Engine.GetService<IStateManager>();
            var state = iStateManager.PeekRollbackStack();
            _data.IsRunning = false;
            SerializeState(state);
        }

        public void SetTimer(float targetTimeInSeconds, string scriptOnTargetTime, float elapsedTimeInSeconds = DEFAULT_ELAPSED_TIME_IN_SECONDS)
        {
            StopTimer();
            SetData(targetTimeInSeconds, scriptOnTargetTime, elapsedTimeInSeconds);
            Show();

            _timerCoroutine = StartCoroutine(StartTimerAsync());
        }

        private void SetData(float targetTimeInSeconds, string scriptOnTargetTime, float elapsedTimeInSeconds = DEFAULT_ELAPSED_TIME_IN_SECONDS)
        {
            _data.TargetTimeInSeconds = targetTimeInSeconds;
            _data.ScriptOnTargetTime = scriptOnTargetTime;
            _data.ElapsedTimeInSeconds = elapsedTimeInSeconds;
        }

        protected override void SerializeState(GameStateMap stateMap)
        {
            base.SerializeState(stateMap);

            var state = GetState();
            stateMap.SetState(state);
        }

        private GameState GetState()
        {
            var state = new GameState
            {
                Data = _data
            };
            return state;
        }

        protected override async UniTask DeserializeState(GameStateMap stateMap)
        {
            await base.DeserializeState(stateMap);

            StopTimer();
            Hide();

            var state = stateMap.GetState<GameState>();

            var shouldSetTimer = state is not null && state.Data.IsRunning && !_data.IsRunning;
            if (shouldSetTimer)
                SetTimer(state.Data.TargetTimeInSeconds, state.Data.ScriptOnTargetTime, state.Data.ElapsedTimeInSeconds);
        }

        protected override void HandleVisibilityChanged(bool visible)
        {
            if (visible) PurgeRollbackIfAutoEnabled();
            else StopTimer();

            base.HandleVisibilityChanged(visible);
        }

        public void StopTimer()
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);

            _timerCoroutine = null;

            _data.IsRunning = false;
        }

        private IEnumerator StartTimerAsync()
        {
            _data.IsRunning = true;

            while (elapsedTimeIsLessThanTargetTime)
            {
                _data.ElapsedTimeInSeconds += Time.deltaTime;

                yield return null;

                OnTimerUpdate?.Invoke(_data.ElapsedTimeInSeconds, _data.TargetTimeInSeconds);
            }

            yield return new WaitForSeconds(_extraSeconds);

            OnTargetTime();
        }

        private void OnTargetTime()
        {
            _onTargetTime?.Invoke();

            ClearChoiceHandler();

            Hide();

            PlayScript();
        }

        private async void PlayScript()
        {
            await NaninovelScriptUtils.Play(_data.ScriptOnTargetTime);
            
            PurgeRollbackIfAutoEnabled();
        }

        private void ClearChoiceHandler()
        {
            var clearChoiceHandler = new ClearChoiceHandler();
            clearChoiceHandler.ExecuteAsync();
        }
    }
}