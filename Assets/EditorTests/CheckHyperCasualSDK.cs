using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine.TestTools;
using HyperCasualSDK;
using HyperCasualSDK.UI;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Tests
{
    public class CheckHyperCasualSDK
    {
        private readonly List<GameObject> _gameObjectsToDestroy = new List<GameObject>();

        [UnityTearDown]
        public IEnumerator PostUnityTest()
        {
            yield return null; 
            foreach (var objectToDestroy in _gameObjectsToDestroy)
            {
                Object.DestroyImmediate(objectToDestroy);
            }
            _gameObjectsToDestroy.Clear();
            yield return null;
        }
        
        [Test]
        public void CheckVibrator()
        {
            Vibration.Init();
            Vibration.VibrateLong();
            Vibration.VibrateShort();
            Assert.True(true);
        }
        
        [UnityTest]
        public IEnumerator CheckPersistentEventsInvoke()
        {
            var isInvoked = false;
            GameStateMachine.Events.GameLoaded.AddListener(() => isInvoked = true);
            GameStateMachine.Events.GameLoaded.Invoke();
            yield return null;
            Assert.True(isInvoked);
        }

        [UnityTest]
        public IEnumerator CreateUI()
        {
            var uiPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/HyperCasualSDK/Prefabs/UI/UI.prefab");
            Assert.IsNotNull(uiPrefab);
            var ui = Object.Instantiate(uiPrefab);
            Assert.IsNotNull(ui);
            var uiController = ui.GetComponent<UIController>();
            Assert.IsNotNull(uiController);
            GameStateMachine.Events.GameLoaded.Invoke();
            _gameObjectsToDestroy.Add(ui);
            yield return null;
        }
        
        [Test]
        public void CheckUIEvents()
        {
            var isInvoked = false;
            UpgradeRewardButton.ButtonEvents.UpgradeReward.AddListener(() => isInvoked = true);
            UpgradeRewardButton.ButtonEvents.UpgradeReward.Invoke();
            Assert.IsTrue(isInvoked);
        }
        
        [UnityTest]
        public IEnumerator TestScoreManager()
        {
            var objectForInputController = new GameObject("ScoreManager");
            _gameObjectsToDestroy.Add(objectForInputController);
            var scoreManager = objectForInputController.AddComponent(typeof(ScoreManager)) as ScoreManager;
            Assert.IsNotNull(scoreManager);
            scoreManager.Awake();
            yield return null; 
            scoreManager.ResetForTest();
            Assert.AreEqual(0, scoreManager.ReceivedLevelScore);
            Assert.AreEqual(0, scoreManager.ScoreCount);
            GameStateMachine.Events.IncreaseLevelScore.Invoke();
            Assert.AreEqual(2, scoreManager.ReceivedLevelScore);
            Assert.AreEqual(0, scoreManager.ScoreCount);
            GameStateMachine.Events.LevelFinished.Invoke();
            MultiplyRewardDialog.Events.AcceptRewardWithoutMultiply.Invoke();
            Assert.AreEqual(0, scoreManager.ReceivedLevelScore);
            Assert.AreEqual(2, scoreManager.ScoreCount);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestUIFlow()
        {
            var uiPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/HyperCasualSDK/Prefabs/UI/UI.prefab");
            var ui = Object.Instantiate(uiPrefab);
            _gameObjectsToDestroy.Add(ui);
            yield return null;
            GameStateMachine.Events.GameLoaded.Invoke();
            AbstractLevelLoader.Events.LevelLoaded.Invoke(0);
            GameStateMachine.Events.LevelFinished.Invoke();
            //TODO: Add here UIController check for events to be received 
            Assert.Pass();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestAudioAssistant()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/HyperCasualSDK/Prefabs/Audio/Audio Assistant.prefab");
            Assert.IsNotNull(prefab);
            var audioAssistantObject = Object.Instantiate(prefab);
            _gameObjectsToDestroy.Add(audioAssistantObject);
            Assert.IsNotNull(audioAssistantObject);
            var audioAssistant = audioAssistantObject.GetComponent<AudioAssistant>();
            Assert.IsNotNull(audioAssistant);
            yield return null;
            audioAssistant.Awake();
            AudioAssistant.Play(SoundEffectType.Tada);
            Assert.Pass();
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator CreateHyperCasualSDKObject()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/HyperCasualSDK/Prefabs/HyperCasualSDK.prefab");
            Assert.IsNotNull(prefab);
            var sdkObject = Object.Instantiate(prefab);
            Assert.IsNotNull(sdkObject);
            _gameObjectsToDestroy.Add(sdkObject);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestCinemachine()
        {
            var prefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/HyperCasualSDK/Prefabs/Camera/Cinemachine.prefab");
            Assert.IsNotNull(prefab);
            var cameraObject = Object.Instantiate(prefab);
            Assert.IsNotNull(cameraObject);
            var cinemachineController = cameraObject.GetComponent<CinemachineController>();
            Assert.IsNotNull(cinemachineController);
            _gameObjectsToDestroy.Add(cameraObject);
            yield return null;
        }
        
        [UnityTest]
        public IEnumerator TestInputController()
        {
            var objectForInputController = new GameObject("InputController");
            var inputController = objectForInputController.AddComponent(typeof(InputController)) as InputController;
            Assert.IsNotNull(inputController);
            yield return null;
            inputController.Awake();
            GameStateMachine.Events.Play.Invoke();
            yield return null;
            inputController.Update();
            Assert.Pass();
            _gameObjectsToDestroy.Add(objectForInputController);
            yield return null;
        }
    }
}
