using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HyperCasualSDK.UI
{
    [RequireComponent(typeof(Button))]
    public class UpgradeRewardButton : MonoBehaviour
    {
        [SerializeField] private Text rewardAmountText;
        [SerializeField] private Text upgradePriceText;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        public void UpgradeRewardPrice(RewardAmountPrice rewardAmountPrice)
        {
            rewardAmountText.text = $"{rewardAmountPrice.IncreaseAmount:n0}";
            upgradePriceText.text = $"{rewardAmountPrice.RewardPrice:n0}";
        }

        private void OnClick()
        {
            ButtonEvents.UpgradeReward.Invoke();
            AudioAssistant.Play(SoundEffectType.ButtonClick);
        }

        public struct ButtonEvents
        {
            public static readonly UnityEvent UpgradeReward = new UnityEvent();
        }
    }
}
