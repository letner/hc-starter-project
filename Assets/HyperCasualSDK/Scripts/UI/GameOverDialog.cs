using UnityEngine;

namespace HyperCasualSDK.UI
{
    public sealed class GameOverDialog : AbstractPanel
    {
        private const float DelayBeforeNoThanks = 3.0f;
        private const float DelayBeforeAutoAction = 5.0f;
        
        [SerializeField] private AdsImageSwitcher reviveButton;
        [SerializeField] private ButtonWithListener noThanksButton;
        [SerializeField] private Countdown countdown;

        private float _shownTime;

        private new void Awake()
        {
            base.Awake();
            reviveButton.AddOnClickAction(ReviveClicked);
            noThanksButton.AddOnClickAction(NoThanksClicked);
        }

        private void Update()
        {
            if (canvasContainer.IsShown)
            {
                if (!noThanksButton.IsShown && Time.time > _shownTime + DelayBeforeNoThanks)
                {
                    noThanksButton.Show();
                }
                else
                {
                    var progress = 1f - (Time.time - _shownTime) / DelayBeforeAutoAction;
                    countdown.UpdateProgress(progress);
                    if (Time.time > _shownTime + DelayBeforeAutoAction)
                    {
                        NoThanksClicked();
                    }
                }
            }
        }

        public new void Show()
        {
            _shownTime = Time.time;
            canvasContainer.Show();
            countdown.Show();
            reviveButton.Show();
            noThanksButton.Hide();
        }

        private void ReviveClicked()
        {
            // ButtonEvents.Revive.Invoke();
            //TODO: PersistentEvents.Advertisement.ShowAd.Invoke(MultiplierType.Revive);
            // Temp solution:
            GameStateMachine.Events.Revive.Invoke();
            AudioAssistant.Play(SoundEffectType.ButtonClick);
        }

        private void NoThanksClicked()
        {
            GameStateMachine.Events.RestartLevel.Invoke();
            AudioAssistant.Play(SoundEffectType.ButtonClick);
        }
    }
}
