using UnityEngine;
using UnityEngine.Events;

namespace HyperCasualSDK
{
    public class LevelCounter : MonoBehaviour, IEventsOwner
    {
        public int LevelIndex => _levelIndex;
        private int _levelIndex = -1;

        private Storage _storage = new Storage();

        private void Awake()
        {
            SubscribeToEvents();
        }

        private void Start()
        {
            Init();
        }

        private void Init()
        {
            _levelIndex = _storage.GetIntPref(StorablePref.LevelIndex);
            LoadLevel();
        }
        
        private void SubscribeToEvents()
        {
            GameStateMachine.Events.NextLevel.AddListener(NextLevel);
        }

        private void NextLevel()
        {
            _levelIndex++;
            _storage.SavePref(StorablePref.LevelIndex, _levelIndex);
            LoadLevel();
        }

        private void LoadLevel()
        {
            Events.LoadLevel.Invoke(_levelIndex);
        }

        public void RemoveAllListeners()
        {
            Events.LoadLevel.RemoveAllListeners();
            Events.SetProgress.RemoveAllListeners();
        }

        public struct Events
        {
            internal static readonly UnityEvent<int> LoadLevel = new UnityEvent<int>();
            public static readonly UnityEvent<float> SetProgress = new UnityEvent<float>();
        }
    }
}
