using UnityEngine;
using UnityEngine.Events;

namespace HyperCasualSDK
{
    public abstract class AbstractLevelLoader : MonoBehaviour, IEventsOwner
    {
        private int _loadedLevelIndex = -1;

        private void Awake()
        {
            LevelCounter.Events.LoadLevel.AddListener(Load);
        }

        private void Start()
        {
            GameStateMachine.Events.GameLoaded.Invoke();
        }

        protected virtual void Load(int index)
        {
            if (index != _loadedLevelIndex)
            {
                Destroy();
            }
            else
            {
                Debug.LogError("Attempt to load already loaded level or base LevelLoader.Load(int) method is not called in inheritor!");
            }
        }

        protected void Loaded(int index)
        {
            _loadedLevelIndex = index;
            Events.LevelLoaded.Invoke(index);
        }

        protected abstract void Destroy();
    
        public void RemoveAllListeners()
        {
            Events.LevelLoaded.RemoveAllListeners();
        }

        public struct Events
        {
            public static readonly UnityEvent<int> LevelLoaded = new UnityEvent<int>();
        }

    }
}
