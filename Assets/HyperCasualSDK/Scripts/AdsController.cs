using HyperCasualSDK.UI;
using UnityEngine;
using UnityEngine.Events;

namespace HyperCasualSDK
{
    public class AdsController : MonoBehaviour
    {
        public AbstractAdsPresenter abstractAdsPresenter;
        
        private bool _forceNoAds;
        
        private void Awake()
        {
            MultiplyRewardDialog.Events.AcceptRewardWithMultiply.AddListener(ShowRewardVideo);
            AppearanceOptions.ForceEvents.NoAds.AddListener(() => _forceNoAds = true);
        }

        private void ShowRewardVideo(MultiplierType multiplierType)
        {
            if (_forceNoAds)
            {
                GameStateMachine.Events.NextLevel.Invoke();
            }
            else
            {
                abstractAdsPresenter?.ShowRewardedVideo();
            }
        }

        public struct CallbackEvents
        {
            public static readonly UnityEvent RewardedVideoCompleted = new UnityEvent();
            public static readonly UnityEvent RewardedVideoSkipped = new UnityEvent();
        }
    }

    public abstract class AbstractAdsPresenter : MonoBehaviour
    {
        public abstract void ShowRewardedVideo();
        public abstract void ShowBanner();
        public abstract void ShowInterstitial();
    }
}
