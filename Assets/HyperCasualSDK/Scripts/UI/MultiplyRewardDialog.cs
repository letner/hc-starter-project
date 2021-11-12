using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HyperCasualSDK.UI
{
    public sealed class MultiplyRewardDialog : AbstractPanel, IEventsOwner
    {
        private const float DelayBeforeNoThanks = 4.0f;

        public AdsImageSwitcher multiplyButton;
        public ButtonWithListener noThanksButton;
        public TextMeshProUGUI rewardScoreText;
        public Text levelNumberText;

        private float _shownTime;
        private float _score;
        private int _levelIndex;

        private new void Awake()
        {
            base.Awake();
            multiplyButton.AddOnClickAction(MultiplyClicked);
            noThanksButton.AddOnClickAction(NoThanksClicked);
            
            ScoreManager.Events.SetReceivedScore.AddListener(score => _score = score);
            AbstractLevelLoader.Events.LevelLoaded.AddListener(levelNumber => _levelIndex = levelNumber);
        }

        private void Update()
        {
            if (canvasContainer.IsShown)
            {
                if (!noThanksButton.IsShown && Time.time > _shownTime + DelayBeforeNoThanks)
                {
                    noThanksButton.Show();
                }
            }
        }

        public new void Show()
        {
            var levelNumber = _levelIndex + 1;
            rewardScoreText.text = $"{_score:n0}";
            levelNumberText.text = $"{levelNumber}";
            _shownTime = Time.time;

            canvasContainer.Show();

            multiplyButton.Show();
            noThanksButton.Hide();
        }

        private void MultiplyClicked()
        {
            Events.AcceptRewardWithMultiply.Invoke(MultiplierType.MultiplyX2);
            AudioAssistant.Play(SoundEffectType.ButtonClick);
            Vibration.VibrateShort();
        }

        private void NoThanksClicked()
        {
            Events.AcceptRewardWithoutMultiply.Invoke();
            AudioAssistant.Play(SoundEffectType.ButtonClick);
        }

        private void OnApplicationQuit()
        {
            RemoveAllListeners();
        }

        public void RemoveAllListeners()
        {
            Events.AcceptRewardWithMultiply.RemoveAllListeners();
            Events.AcceptRewardWithoutMultiply.RemoveAllListeners();
        }

        public struct Events
        {
            public static readonly UnityEvent AcceptRewardWithoutMultiply = new UnityEvent();
            internal static readonly UnityEvent<MultiplierType> AcceptRewardWithMultiply = new UnityEvent<MultiplierType>();
        }

    }
}