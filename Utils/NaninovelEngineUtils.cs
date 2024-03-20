using Naninovel;
using NaninovelTimer.Utils.Disposables;
using System;

namespace NaninovelTimer.Utils
{
    public static class NaninovelEngineUtils
    {
        public static IDisposable TryCallAndGetActionDisposable(Action call)
        {
            if (Engine.Initialized) call();
            else Engine.OnInitializationFinished += call;

            return new ActionDisposable(() => Engine.OnInitializationFinished -= call);
        }
    }
}