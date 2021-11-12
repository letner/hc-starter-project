using System.Text;
using TMPro;
using UnityEngine;

namespace HyperCasualSDK.UI
{
    public class DebugGameplay : MonoBehaviour
    {
        public ButtonWithListener progress;
        public ButtonWithListener lose;
        public ButtonWithListener succeed;
        public ButtonWithListener increaseScore;
        public TextMeshProUGUI debugConsole;

        private StringBuilder _debugConsoleContent;
        
        private void Awake()
        {
            Hide();
            AppearanceOptions.ForceEvents.EnableDebugViews.AddListener(Show);
        }

        private void Show()
        {
            _debugConsoleContent = new StringBuilder();
            gameObject.SetActive(true);
            progress.AddOnClickAction(() => LevelCounter.Events.SetProgress.Invoke(1.0f));
            lose.AddOnClickAction(GameStateMachine.Events.PlayerLost.Invoke);
            succeed.AddOnClickAction(GameStateMachine.Events.PlayerSucceed.Invoke);
            increaseScore.AddOnClickAction(GameStateMachine.Events.IncreaseLevelScore.Invoke);
            Application.logMessageReceived += DebugLog;
        }

        private void Hide()
        {
            gameObject.SetActive(false);
            Application.logMessageReceived -= DebugLog;
        }

        private void OnApplicationQuit()
        {
            Application.logMessageReceived -= DebugLog;
        }

        private void DebugLog(string message, string stackTrace, LogType type)
        {
            _debugConsoleContent.Append(message).Append("\n");
            debugConsole.text = _debugConsoleContent.ToString();
        }
    }
}
