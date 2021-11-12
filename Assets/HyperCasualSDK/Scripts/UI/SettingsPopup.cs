using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace HyperCasualSDK.UI
{
    public class SettingsPopup : MonoBehaviour
    {
        private const string EffectsMixerChannel = "EffectsVolume";
        private const string MusicMixerChannel = "MusicVolume";
        private const float EffectsDefaultValue = 2.1f;
        private const float MusicDefaultValue = -12f;
        private const float MuteValue = -80f;

        public OneSettingButton soundButton;
        public OneSettingButton musicButton;
        public OneSettingButton vibrationButton;

        public Button settingsPanelItself;

        public AudioMixer audioMixer;

        private AudioSettings _audioSettings;

        private void Awake()
        {
            GameStateMachine.Events.GameLoaded.AddListener(InitPanelUI);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public bool IsSettingsShown()
        {
            return gameObject.activeSelf;
        }

        private void InitPanelUI()
        {
            _audioSettings = AudioSettings.Create();
            settingsPanelItself.onClick.AddListener(Hide);

            soundButton.UpdateState(_audioSettings.Sound);
            soundButton.SetSwitchAction(OnSoundClick);
            musicButton.UpdateState(_audioSettings.Music);
            musicButton.SetSwitchAction(OnMusicClick);
            vibrationButton.UpdateState(_audioSettings.Vibration);
            vibrationButton.SetSwitchAction(OnVibrationClick);

            audioMixer.SetFloat(EffectsMixerChannel, _audioSettings.Sound ? EffectsDefaultValue : MuteValue);
            audioMixer.SetFloat(MusicMixerChannel, _audioSettings.Music ? MusicDefaultValue : MuteValue);
            Vibration.Activate(_audioSettings.Vibration);
        }

        private void OnSoundClick()
        {
            _audioSettings.UpdateSound(soundButton.IsOn);
            audioMixer.SetFloat(EffectsMixerChannel, _audioSettings.Sound ? EffectsDefaultValue : MuteValue);
        }

        private void OnMusicClick()
        {
            _audioSettings.UpdateMusic(musicButton.IsOn);
            audioMixer.SetFloat(MusicMixerChannel, _audioSettings.Music ? MusicDefaultValue : MuteValue);
        }

        private void OnVibrationClick()
        {
            _audioSettings.UpdateVibration(vibrationButton.IsOn);
            Vibration.Activate(_audioSettings.Vibration);
        }
    }
}
