using TMPro;
using UnityEngine;

namespace HyperCasualSDK.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        private const float TimeForEffect = 2.0f;

        public TextMeshProUGUI counterText;
        public AchievementMessage[] achievements;
        public ReceivedScorePopup receivedScorePopup;
        public GameObject scoreObtainingVFX;

        private int _oldAmount = -1;
        private int _targetAmount;

        private enum State
        {
            NoEffect,
            Increase,
            Decrease,
        }

        private State _state;
        private float _startEffectTime;
        private int _previouslyShownAchievement;

        private void Awake()
        {
            SubscribeToEvents();
            foreach (var achievement in achievements)
            {
                achievement.Hide();
            }
        }

        private void SubscribeToEvents()
        {
            AbstractLevelLoader.Events.LevelLoaded.AddListener(levelIndex => SetReceivedScore(0));
            GameStateMachine.Events.PerfectlyIncreaseLevelScore.AddListener(ShowAchievement);
            ScoreManager.Events.SetOverallScore.AddListener(score => SetOverallScore((int) score));
            ScoreManager.Events.SetReceivedScore.AddListener(score => SetReceivedScore((int) score));
        }

        private void Update()
        {
            switch (_state)
            {
                case State.Increase:
                case State.Decrease:
                    if (Time.time - _startEffectTime < TimeForEffect)
                    {
                        var effectAmount = (_targetAmount - _oldAmount) * (Time.time - _startEffectTime) / TimeForEffect + _oldAmount;
                        counterText.text = $"{effectAmount:n0}";
                    }
                    else
                    {
                        SetFinalAmount();
                        _state = State.NoEffect;
                    }

                    break;
            }
        }

        private void SetOverallScore(int amount)
        {
            SetFinalAmount();
            _startEffectTime = Time.time;
            _targetAmount = amount;
            if (_oldAmount == -1)
            {
                SetFinalAmount();
                _state = State.NoEffect;
            }
            else if (amount - _oldAmount > 0)
            {
                _state = State.Increase;
                if (scoreObtainingVFX != null)
                {
                    scoreObtainingVFX.SetActive(true);
                }
            }
            else
            {
                _state = State.Decrease;
            }
        }

        private void SetReceivedScore(int amount)
        {
            receivedScorePopup.UpdateAmount(amount);
        }

        private void ShowAchievement(Perfection perfection)
        {
            achievements[_previouslyShownAchievement].Hide();
            switch (perfection)
            {
                case Perfection.Perfect:
                    _previouslyShownAchievement = 0;
                    achievements[0].Show();
                    break;
                case Perfection.Excellent:
                    _previouslyShownAchievement = 1;
                    achievements[1].Show();
                    break;
                case Perfection.Great:
                    _previouslyShownAchievement = 2;
                    achievements[2].Show();
                    break;
                case Perfection.Good:
                    _previouslyShownAchievement = 3;
                    achievements[3].Show();
                    break;
            }
        }

        public void ObtainCoinsInGame(int newAmount)
        {
            _targetAmount = newAmount;
            SetFinalAmount();
        }

        private void SetFinalAmount()
        {
            counterText.text = $"{_targetAmount:n0}";
            _oldAmount = _targetAmount;
            if (scoreObtainingVFX != null)
            {
                scoreObtainingVFX.SetActive(false);
            }
        }
    }
}

public enum Perfection
{
    Perfect,
    Excellent,
    Great,
    Good,
    Ok,
    Nothing,
}
