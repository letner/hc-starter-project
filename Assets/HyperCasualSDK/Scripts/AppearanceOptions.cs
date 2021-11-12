using UnityEngine;
using UnityEngine.Events;

namespace HyperCasualSDK
{
    public class AppearanceOptions : MonoBehaviour, IEventsOwner
    {
        public bool forceNoUI;
        public bool forceNoAds;
        public bool forceReviveAfterDeathAutomatically;
        public bool forceRestartAutomatically;
        public bool enableDebugViews;
        
        private void Start()
        {
            if (forceNoUI) ForceEvents.NoUI.Invoke();
            if (forceNoAds) ForceEvents.NoAds.Invoke();
            if (forceReviveAfterDeathAutomatically) ForceEvents.ReviveAfterDeathAutomatically.Invoke();
            if (forceRestartAutomatically) ForceEvents.RestartAutomatically.Invoke();
            if (enableDebugViews) ForceEvents.EnableDebugViews.Invoke();
        }

        private void OnApplicationQuit()
        {
            RemoveAllListeners();
        }

        public void RemoveAllListeners()
        {
            ForceEvents.NoUI.RemoveAllListeners();
            ForceEvents.NoAds.RemoveAllListeners();
            ForceEvents.ReviveAfterDeathAutomatically.RemoveAllListeners();
            ForceEvents.RestartAutomatically.RemoveAllListeners();
            ForceEvents.EnableDebugViews.RemoveAllListeners();
        }
        
        public struct ForceEvents
        {
            internal static readonly UnityEvent NoUI = new UnityEvent();
            internal static readonly UnityEvent NoAds = new UnityEvent();
            internal static readonly UnityEvent ReviveAfterDeathAutomatically = new UnityEvent();
            internal static readonly UnityEvent RestartAutomatically = new UnityEvent();
            internal static readonly UnityEvent EnableDebugViews = new UnityEvent();
        }

    }
    

}