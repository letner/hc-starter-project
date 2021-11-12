using UnityEngine;

namespace HyperCasualSDK.UI
{
    public class UIController : MonoBehaviour
    {
        [Header("Set in scene")] public LobbyPanel lobbyPanel;
        public SettingsPopup settingsPopup;
        public GameOverDialog gameOverDialog;
        public MultiplyRewardDialog multiplyRewardDialog;

        private bool _forceNoUI;

        private void Awake()
        {
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            SettingsButton.Events.ShowSettings.AddListener(() => settingsPopup.Show());

            AbstractLevelLoader.Events.LevelLoaded.AddListener(levelIndex => SwitchToPanel(MainPanelType.Lobby));
            
            GameStateMachine.Events.Play.AddListener(() => SwitchToPanel(MainPanelType.Gameplay));
            GameStateMachine.Events.GameOver.AddListener(() => ShowPopup(PopupType.GameOver));
            GameStateMachine.Events.RestartLevel.AddListener(() => SwitchToPanel(MainPanelType.Lobby));
            GameStateMachine.Events.Revive.AddListener(() => SwitchToPanel(MainPanelType.Lobby));
            GameStateMachine.Events.CelebrationEnded.AddListener(CelebrationEnded);

            AppearanceOptions.ForceEvents.NoUI.AddListener(() => _forceNoUI = true);
        }

        private void SwitchToPanel(MainPanelType mainPanelType)
        {
            switch (mainPanelType)
            {
                case MainPanelType.Gameplay:
                    ShowGameplay();
                    break;
                case MainPanelType.Lobby:
                    ShowLobbyPanel();
                    break;
            }
        }

        private void ShowGameplay()
        {
            lobbyPanel.Hide();
            gameOverDialog.Hide();
            multiplyRewardDialog.Hide();
            settingsPopup.Hide();
        }

        private void ShowLobbyPanel()
        {
            lobbyPanel.Show();
            gameOverDialog.Hide();
            multiplyRewardDialog.Hide();
            settingsPopup.Hide();
            if (_forceNoUI)
            {
                lobbyPanel.Hide();
            }
        }

        private void CelebrationEnded()
        {
            ShowPopup(PopupType.MultiplyReward);
        }

        private void ShowPopup(PopupType popupType)
        {
            switch (popupType)
            {
                case PopupType.Settings:
                    settingsPopup.Show();
                    break;
                case PopupType.MultiplyReward:
                    if (!_forceNoUI)
                    {
                        multiplyRewardDialog.Show();
                    }
                    break;
                case PopupType.GameOver:
                    if (!_forceNoUI)
                    {
                        gameOverDialog.Show();
                    }
                    break;
            }
        }
    }
    
    public enum MainPanelType
    {
        Gameplay, Shop, Lobby
    }

    public enum PopupType
    {
        Settings, MultiplyReward, GameOver
    }
}
