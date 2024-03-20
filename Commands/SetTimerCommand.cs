using Naninovel;
using Naninovel.Commands;
using NaninovelTimer.UI;
using System.Text;

namespace NaninovelTimer.Commands
{
    [CommandAlias("setTimer")]
    public class SetTimerCommand : Command
    {
        [RequiredParameter][ParameterAlias(NamelessParameterAlias)] public DecimalParameter TargetTimeInSeconds;
        [ParameterAlias("goto"), ResourceContext(ScriptsConfiguration.DefaultPathPrefix, 0)]
        public NamedStringParameter GotoPath;

        public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
        {
            var uiManager = Engine.GetService<IUIManager>();
            var timerUI = uiManager.GetUI<TimerUI>();

            var builder = new StringBuilder();
            builder.AppendLine($"{Naninovel.Parsing.Identifiers.CommandLine}{nameof(Goto)} {GotoPath.Name ?? string.Empty}{(GotoPath.NamedValue.HasValue ? $".{GotoPath.NamedValue.Value}" : string.Empty)}");
            var scriptOnTargetTime = builder.ToString().TrimFull();

            timerUI.SetTimer(TargetTimeInSeconds, scriptOnTargetTime);


            return UniTask.CompletedTask;
        }
    }
}