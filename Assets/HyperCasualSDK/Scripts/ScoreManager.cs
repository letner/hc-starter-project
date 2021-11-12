using System;
using HyperCasualSDK.UI;
using UnityEngine;
using UnityEngine.Events;

namespace HyperCasualSDK
{
    [Serializable]
    public class ScoreManager : MonoBehaviour, IEventsOwner
    {
        [Header("Initial values")]
        public int rewardForEachIncreaseAtStart = 1;
        public int unlockRandomOutfitPrice = 900;
        public int increaseRewardAtStartPrice = 1000;

        private int _scoreCount;
        public int ScoreCount 
        { 
            get => _scoreCount;
            private set
            {
                _scoreCount = value;
                Events.SetOverallScore.Invoke(_scoreCount);
            }
        }

        private int _receivedLevelScore;
        public int ReceivedLevelScore
        {
            get => _receivedLevelScore;
            private set
            {
                _receivedLevelScore = value;
                Events.SetReceivedScore.Invoke(_receivedLevelScore);
            }
        }

        private int _increaseRewardPrice;
        private int _rewardForEachIncrease;

        private Storage _storage = new Storage();

        public void Awake()
        {
            SubscribeToEvents();
        }

        public void ResetReceivedCoins()
        {
            ReceivedLevelScore = 0;
        }

        [Obsolete("Have to use event-based mechanism to call this method")]
        public void PayWithScore(int price)
        {
            UpdateOverallScore(ScoreCount - price, true);
        }

        public void ResetForTest()
        {
            ScoreCount = 0;
            ReceivedLevelScore = 0;
            _increaseRewardPrice = 10;
            _rewardForEachIncrease = 2;
        }

        private void SubscribeToEvents()
        {
            GameStateMachine.Events.LauncherLoaded.AddListener(Init);
            GameStateMachine.Events.IncreaseLevelScore.AddListener(() => { IncreaseReceivedLevelScore(_rewardForEachIncrease); });
            GameStateMachine.Events.PerfectlyIncreaseLevelScore.AddListener(IncreaseReceivedLevelScore);
            GameStateMachine.Events.RestartLevel.AddListener(ResetReceivedCoins);

            UpgradeRewardButton.ButtonEvents.UpgradeReward.AddListener(UpgradeReward);
            MultiplyRewardDialog.Events.AcceptRewardWithMultiply.AddListener(ApplyScore);
            MultiplyRewardDialog.Events.AcceptRewardWithoutMultiply.AddListener(ApplyScore);
        }

        private void UpgradeReward()
        {
            if (ScoreCount - _increaseRewardPrice >= 0)
            {
                ScoreCount -= _increaseRewardPrice;
                var previousReward = _rewardForEachIncrease;
                _rewardForEachIncrease = Mathf.RoundToInt(_rewardForEachIncrease * 1.15f);
                if (_rewardForEachIncrease == previousReward)
                {
                    _rewardForEachIncrease++;
                }
                _increaseRewardPrice = Mathf.RoundToInt(_increaseRewardPrice * 1.3f);
                SaveRewardPrefs();
                //TODO: Control Reward increase and price somewhere more related to reward (???)
                Events.SetRewardPrice.Invoke(new RewardAmountPrice(_rewardForEachIncrease, _increaseRewardPrice));
            }
        }

        private void IncreaseReceivedLevelScore(int amount)
        {
            ReceivedLevelScore += amount;
        }
        
        private void IncreaseReceivedLevelScore(Perfection perfection)
        {
            var multiplier = 1.0f;
            switch (perfection)
            {
                case Perfection.Perfect:
                    multiplier = 3.0f;
                    break;
                case Perfection.Excellent:
                    multiplier = 2.0f;
                    break;
                case Perfection.Great:
                    multiplier = 1.5f;
                    break;
                case Perfection.Good:
                    multiplier = 1f;
                    break;
                case Perfection.Ok:
                    multiplier = 1f;
                    break;
            }

            var addition = (int) (_rewardForEachIncrease * multiplier);
            ReceivedLevelScore += addition;
            //TODO: In AchievementDetector subscribe to Data.SetReceivedScore and calculate Perfection there
            // _uiController.ShowAchievement(perfection);
        }

        private void ApplyScore(MultiplierType multiplierType)
        {
            var multiplier = 1.0f;
            switch (multiplierType)
            {
                case MultiplierType.MultiplyX2:
                    multiplier = 2.0f;
                    break;
                case MultiplierType.MultiplyX3:
                    multiplier = 3.0f;
                    break;
                case MultiplierType.MultiplyX4:
                    multiplier = 4.0f;
                    break;
                case MultiplierType.MultiplyX5:
                    multiplier = 5.0f;
                    break;
                case MultiplierType.MultiplyX6:
                    multiplier = 6.0f;
                    break;
                case MultiplierType.MultiplyX7:
                    multiplier = 7.0f;
                    break;
            }

            ScoreCount += (int) (ReceivedLevelScore * multiplier);
            SaveScore();
        }
        
        private void ApplyScore()
        {
            ScoreCount += ReceivedLevelScore;
            SaveScore();
        }

        private void AddScoreInGame(int additionScore)
        {
            ScoreCount += additionScore;
        }

        private void MultiplyReceivedLevelScore(int amount)
        {
            ReceivedLevelScore *= amount;
        }

        private void UpdateOverallScore(int additionScore, bool save)
        {
            ScoreCount += additionScore;
            if (save)
            {
                SaveScore();
            }
        }

        private void Init()
        {
            ScoreCount = _storage.GetIntPref(StorablePref.ScoreCount);
            ReceivedLevelScore = 0;
            _increaseRewardPrice = _storage.GetIntPref(StorablePref.IncreaseRewardPrice);
            _rewardForEachIncrease = _storage.GetIntPref(StorablePref.RewardForEachIncrease);

            if (_increaseRewardPrice == 0)
            {
                _increaseRewardPrice = increaseRewardAtStartPrice;
            }

            if (_rewardForEachIncrease == 0)
            {
                _rewardForEachIncrease = rewardForEachIncreaseAtStart;
            }
        }

        private void SaveScore()
        {
            ReceivedLevelScore = 0;
            _storage.SavePref(StorablePref.ScoreCount, ScoreCount);
        }

        private void SaveRewardPrefs()
        {
            _storage.SavePref(StorablePref.RewardForEachIncrease, _rewardForEachIncrease);
            _storage.SavePref(StorablePref.IncreaseRewardPrice, _increaseRewardPrice);
        }

        private void OnApplicationQuit()
        {
            RemoveAllListeners();
        }

        public void RemoveAllListeners()
        {
            Events.SetOverallScore.RemoveAllListeners();
            Events.SetReceivedScore.RemoveAllListeners();
            Events.SetRewardPrice.RemoveAllListeners();
        }

        public struct Events
        {
            public static readonly UnityEvent<float> SetOverallScore = new UnityEvent<float>();
            public static readonly UnityEvent<float> SetReceivedScore = new UnityEvent<float>();
            public static readonly UnityEvent<RewardAmountPrice> SetRewardPrice = new UnityEvent<RewardAmountPrice>();
        }
    }
}
