using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace HyperCasualSDK
{
    public class SceneLoader : MonoBehaviour, IEventsOwner
    {
        // public SceneAsset gameplayScene;
        //
        // private void Awake()
        // {
        //     GameStateMachine.Events.LauncherLoaded.AddListener(() => StartCoroutine(LoadGameplayScene()));
        // }
        //
        // private IEnumerator LoadGameplayScene()
        // {
        //     var operation = SceneManager.LoadSceneAsync(gameplayScene.name, LoadSceneMode.Additive);
        //     
        //     while (!operation.isDone) {
        //         Debug.Log(operation.progress);
        //         Events.LoadingProgress.Invoke(operation.progress);
        //         yield return null;
        //     }
        // }

        public void RemoveAllListeners()
        {
            Events.LoadingProgress.RemoveAllListeners();
            Events.SceneLoaded.RemoveAllListeners();
        }

        private void OnApplicationQuit()
        {
            RemoveAllListeners();
        }

        public struct Events
        {
            public static readonly UnityEvent<float> LoadingProgress = new UnityEvent<float>();
            public static readonly UnityEvent SceneLoaded = new UnityEvent();
        }
    }
}