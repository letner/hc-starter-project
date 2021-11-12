using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace HyperCasualSDK.UI
{
    public class SettingsButton : MonoBehaviour
    {

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Events.ShowSettings.Invoke();
            AudioAssistant.Play(SoundEffectType.ButtonClick);
        }

        public struct Events
        {
            internal static readonly UnityEvent ShowSettings = new UnityEvent();
        }
    }
}
