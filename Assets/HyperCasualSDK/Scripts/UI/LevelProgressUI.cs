using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HyperCasualSDK.UI
{
    public class LevelProgressUI : AbstractPanel
    {
        [SerializeField] private TextMeshProUGUI currentLevelNumber;
        [SerializeField] private TextMeshProUGUI nextLevelNumber;
        [SerializeField] private Image completionImage;

        private new void Awake()
        {
            base.Awake();
            GameStateMachine.Events.SetGameState.AddListener(GameStateChanged);
            AbstractLevelLoader.Events.LevelLoaded.AddListener(levelIndex => SetLevelNumber(levelIndex + 1));
            LevelCounter.Events.SetProgress.AddListener(SetProgress);
        }

        private void SetLevelNumber(int current)
        {
            currentLevelNumber.text = $"{current}";
            nextLevelNumber.text = $"{(current + 1)}";

            SetProgress(0);
        }

        private void GameStateChanged(GameState gameState)
        {
            switch (gameState)
            {
                case GameState.Gameplay:
                    Show();
                    break;
                case GameState.Lobby:
                    Hide();
                    break;
            }
        }
        
        private void SetProgress(float completion)
        {
            completionImage.rectTransform.anchorMax = new Vector2(completion, 1);
        }
    }
}
