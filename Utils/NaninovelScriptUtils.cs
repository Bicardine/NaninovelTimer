using Naninovel;

namespace NaninovelTimer.Utils
{
    public static class NaninovelScriptUtils
    {
        public static async UniTask Play(string scriptText)
        {
            var script = Script.FromScriptText($"ScriptUtils generated script", scriptText);
            var playlist = new ScriptPlaylist(script);
            await playlist.ExecuteAsync();
        }
    }
}