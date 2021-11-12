using HyperCasualSDK.UI;
using UnityEngine;
using UnityEngine.Events;

namespace HyperCasualSDK
{
    public class GameStateMachine : MonoBehaviour, IEventsOwner
    {
        [Tooltip("How long the game would show a dead player before game over screen appears")] [Range(0, 3)]
        public float timeForGrief = 2.5f;

        [Tooltip("Show celebration animation and effects during this time")] [Range(0, 3)]
        public float timeForCelebration = 2.5f;

        private float _switchStateTime;
        private GameState _state;

        private GameState State
        {
            get => _state;
            set
            {
                _state = value;
                _switchStateTime = Time.time;
                Events.SetGameState.Invoke(_state);
            }
        }

        private bool _forceReviveAfterDeathAutomatically;
        private bool _forceRestartAutomatically;

        private void Awake()
        {
            SubscribeToEvents();
        }

        private void Start()
        {
            Events.LauncherLoaded.Invoke();
        }

        private void Update()
        {
            ProcessDelayedStateSwitch();
        }

        private void SubscribeToEvents()
        {
            AppearanceOptions.ForceEvents.ReviveAfterDeathAutomatically.AddListener(() => _forceReviveAfterDeathAutomatically = true);
            AppearanceOptions.ForceEvents.RestartAutomatically.AddListener(() => _forceRestartAutomatically = true);

            InputController.Events.TouchBegin.AddListener(StartGameplay);
            MultiplyRewardDialog.Events.AcceptRewardWithoutMultiply.AddListener(Events.NextLevel.Invoke);

            Events.RestartLevel.AddListener(() => State = GameState.Lobby);
            Events.Revive.AddListener(() => State = GameState.Lobby);
            Events.NextLevel.AddListener(NextLevel);
            Events.Play.AddListener(() => State = GameState.Gameplay);
            Events.PlayerLost.AddListener(() => State = GameState.PlayerLost);
            Events.PlayerSucceed.AddListener(PlayerSucceed);
        }

        private void ProcessDelayedStateSwitch()
        {
            switch (State)
            {
                case GameState.RewardedVideo:
                    //TODO: ProcessRewardedVideoStates();
                    break;
                case GameState.PlayerSucceed:
                    if (Time.time > _switchStateTime + timeForCelebration)
                    {
                        State = GameState.GameOver;
                        Events.CelebrationEnded.Invoke();
                    }
                    break;
                case GameState.PlayerLost:
                    if (Time.time > _switchStateTime + timeForGrief)
                    {
                        if (_forceReviveAfterDeathAutomatically)
                        {
                            Events.Revive.Invoke();
                            // State = GameState.RewardedVideo;
                            // GameplayState = GameplayState.ReviveRewardedVideoCompleted;
                            //TODO: Call Ads.VideoEvent(AdEventType.ReviveRewardedVideoCompleted) here
                        }
                        else if (_forceRestartAutomatically)
                        {
                            Events.RestartLevel.Invoke();
                        }
                        else
                        {
                            State = GameState.GameOver;
                            Events.GameOver.Invoke();
                        }
                    }

                    break;
                case GameState.ReviveSuggestion:
                    break;
                case GameState.Settings:
                    break;
                case GameState.LobbyStore:
                    break;
                case GameState.CharacterStore:
                    break;
            }
        }

        private void StartGameplay()
        {
            if (State == GameState.Lobby)
            {
                Events.Play.Invoke();
            }
        }

        private void PlayerSucceed()
        {
            AudioAssistant.Play(SoundEffectType.Tada);
            State = GameState.PlayerSucceed;
        }

        private void NextLevel()
        {
            Events.LevelFinished.Invoke();
            State = GameState.Lobby;
        }

        public struct Events
        {
            public static readonly UnityEvent GameLoaded = new UnityEvent();
            public static readonly UnityEvent LevelFinished = new UnityEvent();
            public static readonly UnityEvent Play = new UnityEvent();
            public static readonly UnityEvent IncreaseLevelScore = new UnityEvent();
            public static readonly UnityEvent<Perfection> PerfectlyIncreaseLevelScore = new UnityEvent<Perfection>();
            public static readonly UnityEvent<Vector3> CheckpointReached = new UnityEvent<Vector3>();
            public static readonly UnityEvent Revive = new UnityEvent();
            public static readonly UnityEvent PlayerSucceed = new UnityEvent();
            public static readonly UnityEvent PlayerLost = new UnityEvent();
            public static readonly UnityEvent RestartLevel = new UnityEvent();

            internal static readonly UnityEvent LauncherLoaded = new UnityEvent();
            internal static readonly UnityEvent<GameState> SetGameState = new UnityEvent<GameState>();
            internal static readonly UnityEvent CelebrationEnded = new UnityEvent();
            internal static readonly UnityEvent NextLevel = new UnityEvent();
            internal static readonly UnityEvent GameOver = new UnityEvent();
        }

        private void OnApplicationQuit()
        {
            RemoveAllListeners();
        }

        public void RemoveAllListeners()
        {
            Events.GameLoaded.RemoveAllListeners();
            Events.SetGameState.RemoveAllListeners();
            Events.Play.RemoveAllListeners();
            Events.LevelFinished.RemoveAllListeners();
            Events.IncreaseLevelScore.RemoveAllListeners();
            Events.PerfectlyIncreaseLevelScore.RemoveAllListeners();
            Events.CheckpointReached.RemoveAllListeners();
            Events.Revive.RemoveAllListeners();
            Events.PlayerSucceed.RemoveAllListeners();
            Events.PlayerLost.RemoveAllListeners();
            Events.GameOver.RemoveAllListeners();
            Events.RestartLevel.RemoveAllListeners();
            Events.CelebrationEnded.RemoveAllListeners();
            Events.NextLevel.RemoveAllListeners();
        }
    }

    public enum GameState
    {
        Lobby,
        Gameplay,
        InactiveGameplay,
        Paused,
        RewardedVideo, // == InactiveGameplay
        PlayerLost,
        PlayerSucceed,
        GameOver,
        ReviveSuggestion, // == InactiveGameplay
        Settings,
        LobbyStore,
        CharacterStore
    }
}
