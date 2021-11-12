namespace HyperCasualSDK
{
    
    [System.Serializable]
    public class AudioSettings
    {
        private Storage _storage = new Storage();

        public bool Sound { get; private set; }
        public bool Music { get; private set; }
        public bool Vibration { get; private set; }

        public static AudioSettings Create()
        {
            var audioSettings = new AudioSettings();
            audioSettings.LoadDataFromPrefs();
            return audioSettings;
        }

        public void UpdateSound(bool value)
        {
            Sound = value;
            _storage.SavePref(StorablePref.SoundSetting, value);
        }

        public void UpdateMusic(bool value)
        {
            Music = value;
            _storage.SavePref(StorablePref.MusicSetting, value);
        }

        public void UpdateVibration(bool value)
        {
            Vibration = value;
            _storage.SavePref(StorablePref.VibrationSetting, value);
        }

        public void UpdateAllSettings(bool sound, bool music, bool vibration)
        {
            this.Sound = sound;
            this.Music = music;
            this.Vibration = vibration;
        }

        private void LoadDataFromPrefs()
        {
            Sound = _storage.GetBoolPref(StorablePref.SoundSetting);
            Music = _storage.GetBoolPref(StorablePref.MusicSetting);
            Vibration = _storage.GetBoolPref(StorablePref.VibrationSetting);
        }
    }
}
