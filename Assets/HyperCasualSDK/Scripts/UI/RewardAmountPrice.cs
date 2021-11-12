using System;

namespace HyperCasualSDK.UI
{
    [Serializable]
    public struct RewardAmountPrice
    {
        public int IncreaseAmount { get; private set; }
        public int RewardPrice { get; private set; }

        public RewardAmountPrice(int increaseAmount, int rewardPrice)
        {
            IncreaseAmount = increaseAmount;
            RewardPrice = rewardPrice;
        }
    }
}