using UnityEngine;

namespace HyperCasualSDK.UI
{
    public class LobbyPanel : AbstractPanel
    {
        [SerializeField] private UpgradeRewardButton upgradeRewardButton;

        public new void Awake()
        {
            base.Awake();
            ScoreManager.Events.SetRewardPrice.AddListener(rewardAmountPrice => upgradeRewardButton.UpgradeRewardPrice(rewardAmountPrice));
        }
    }
}
