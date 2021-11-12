using HyperCasualSDK;
using HyperCasualSDK.UI;
using NUnit.Framework;
using UnityEngine;

namespace Tests
{
    public class CheckScene
    {
        private const string ObjectNotFoundMessage = "There is no {0} on your scene";
        private const string ObjectIsNotSingularMessage = "You should have only one {0} on your scene";
        
        [Test]
        public void CheckSceneContainsSingularInputController()
        {
            CheckSceneContainsSingularObject<UIController>(className: "UIController");
        }

        [Test]
        public void CheckSceneContainsSingularUIController()
        {
            CheckSceneContainsSingularObject<UIController>(className: "UIController");
        }

        [Test]
        public void CheckSceneContainsSingularAudioAssistant()
        {
            CheckSceneContainsSingularObject<AudioAssistant>(className: "AudioAssistant");
        }

        [Test]
        public void CheckSceneContainsSingularGameStateMachine()
        {
            CheckSceneContainsSingularObject<GameStateMachine>(className: "GameStateMachine");
        }

        [Test]
        public void CheckSceneContainsSingularScoreManager()
        {
            CheckSceneContainsSingularObject<ScoreManager>(className: "ScoreManager");
        }

        [Test]
        public void CheckSceneContainsSingularLevelCounter()
        {
            CheckSceneContainsSingularObject<LevelCounter>(className: "LevelCounter");
        }

        [Test]
        public void CheckSceneContainsSingularAppearanceOptions()
        {
            CheckSceneContainsSingularObject<AppearanceOptions>(className: "AppearanceOptions");
        }

        private void CheckSceneContainsSingularObject<T>(string className)
        {
            var objects = Object.FindObjectsOfType(typeof(T)) as T[];
            Assert.IsNotNull(objects, ObjectNotFoundMessage, className);
            Assert.IsNotEmpty(objects, ObjectNotFoundMessage, className);
            Assert.LessOrEqual(1, objects.Length, ObjectIsNotSingularMessage, className);
        }
    }
}
