using Naninovel;
using System;
using System.Collections.Generic;

namespace NaninovelTimer.Utils.Disposables
{
    public class NaninovelEngineCompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _disposables = new List<IDisposable>();

        public void Retain(Action call)
        {
            _disposables.Add(NaninovelEngineUtils.TryCallAndGetActionDisposable(call));
            Engine.OnInitializationFinished += Dispose;
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
                disposable.Dispose();

            _disposables.Clear();
        }
    }
}