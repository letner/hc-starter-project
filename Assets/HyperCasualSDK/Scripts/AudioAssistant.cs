using System.Collections.Generic;
using UnityEngine;

namespace HyperCasualSDK
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioAssistant : MonoBehaviour
    {
        private const float MinDelayBetweenSounds = .2f;

        [SerializeField] public SoundEffect[] soundEffects;
        private Dictionary<SoundEffectType, AudioClip> _soundEffectDictionary;
        
        private AudioSource _audioSource;
        private static AudioAssistant _singleton;
        private float _lastPlayedTime;

        public void Awake()
        {
            _singleton = this;
            _audioSource = GetComponent<AudioSource>();
            _soundEffectDictionary = ConvertEffectListToDictionary();
            Vibration.Init();
        }
        
        public static void Play(SoundEffectType clip)
        {
            _singleton.PlayOnce(clip);
        }

        private void PlayOnce(SoundEffectType clip)
        {
            if (clip == SoundEffectType.Checkpoint || Time.time > _lastPlayedTime + MinDelayBetweenSounds)
            {
                _lastPlayedTime = Time.time;
                _audioSource.PlayOneShot(_soundEffectDictionary[clip]);
                Vibration.VibrateShort();
            }
        }

        private Dictionary<SoundEffectType, AudioClip> ConvertEffectListToDictionary()
        {
            var result = new Dictionary<SoundEffectType, AudioClip>();
            foreach (var soundEffect in soundEffects)
            {
                result.Add(soundEffect.type, soundEffect.audioClip);
            }
            return result;
        }
    }
    
    public enum SoundEffectType
    {
        ButtonClick = 0,
        Consume = 1,
        ElementMultiply = 2,
        Tada = 3,
        Checkpoint = 4,
    }
}
