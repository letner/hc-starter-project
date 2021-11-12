using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

namespace HyperCasualSDK.UI
{
    public class OneSettingButton : MonoBehaviour
    {
        [SerializeField] private GameObject on;
        [SerializeField] private GameObject off;

        public bool IsOn { get; private set; }

        private UnityAction _switchAction;

        private void On()
        {
            on.SetActive(true);
            off.SetActive(false);
            IsOn = true;
        }

        private void Off()
        {
            on.SetActive(false);
            off.SetActive(true);
            IsOn = false;
        }

        public void UpdateState(bool isOn)
        {
            IsOn = isOn;
            if (isOn)
            {
                On();
            }
            else
            {
                Off();
            }
        }

        public void SetSwitchAction(UnityAction action)
        {
            _switchAction = action;
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            IsOn = !IsOn;
            AudioAssistant.Play(SoundEffectType.ButtonClick);
            Vibration.VibrateShort();
            UpdateState(IsOn);
            _switchAction();
        }
    }
}
