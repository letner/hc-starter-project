using UnityEngine;

namespace HyperCasualSDK
{
    public class Storage
    {
        private const int SettingIsOn = 2;
        private const int SettingIsOff = 1;

        public int GetIntPref(StorablePref pref)
        {
            return PlayerPrefs.GetInt(pref.ToString());
        }

        public bool GetBoolPref(StorablePref pref, bool defaultValue = true)
        {
            var result = defaultValue;
            var intValue = PlayerPrefs.GetInt(pref.ToString());
            if (intValue == SettingIsOn) {
                result = true;
            } else if (intValue == SettingIsOff) {
                result = false;
            }
            return result;
        }


        public void SavePref<T>(StorablePref pref, T value)
        {
            switch (value)
            {
                case bool boolToSave:
                    SaveBool(pref, boolToSave);
                    break;
                case int intToSave:
                    SaveInt(pref, intToSave);
                    break;
                default:
                    Debug.Log($"Storage couldn't recognise the type of the value: {value}");
                    break;
            }
        }

        private void SaveBool(StorablePref pref, bool value)
        {
            var valueToSave = value ? SettingIsOn : SettingIsOff;
            PlayerPrefs.SetInt(pref.ToString(), valueToSave);
            PlayerPrefs.Save();
        }

        private void SaveInt(StorablePref pref, int value)
        {
            PlayerPrefs.SetInt(pref.ToString(), value);
            PlayerPrefs.Save();
        }
    }

    public enum StorablePref
    {
        ScoreCount,
        LevelIndex,
        IncreaseRewardPrice,
        RewardForEachIncrease,
        SoundSetting,
        MusicSetting,
        VibrationSetting,
    }

}