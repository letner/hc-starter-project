using System;
using UnityEngine;

#if UNITY_IOS
using System.Collections;
using System.Runtime.InteropServices;
#endif

namespace HyperCasualSDK
{
    public static class Vibration
    {
#if UNITY_IOS
    [DllImport ("__Internal")]
    private static extern void VibrateApple();
    [DllImport ("__Internal")]
    private static extern void VibrateShortApple();
    [DllImport ("__Internal")]
    private static extern void _VibratePeek();
#endif

#if UNITY_ANDROID
        private static AndroidJavaObject _vibrator;
        private static AndroidJavaClass _vibrationEffect;
#endif

        private static bool _isInitialized;
        private static bool _isActive = true;

        public static void Init()
        {
            if (_isInitialized) return;
#if UNITY_ANDROID
            if (Application.isMobilePlatform)
            {
                var unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                var currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                _vibrator = currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");

                if (AndroidVersion >= 26)
                {
                    _vibrationEffect = new AndroidJavaClass("android.os.VibrationEffect");
                }
            }
#endif
            _isInitialized = true;
        }

        public static void VibrateShort()
        {
            if (_isActive && Application.isMobilePlatform)
            {
#if UNITY_IOS
                VibrateShortApple();
#elif UNITY_ANDROID
                Vibrate(50);
#endif
            }
        }

        public static void VibrateLong()
        {
            if (_isActive && Application.isMobilePlatform)
            {
#if UNITY_IOS
                VibrateApple();
#elif UNITY_ANDROID
                Vibrate(100);
#endif
            }
        }

        public static void Activate(bool active)
        {
            _isActive = active;
        }

#if UNITY_ANDROID
        private static void Vibrate(long milliseconds)
        {
            if (_isActive && Application.isMobilePlatform)
            {
                if (AndroidVersion >= 26)
                {
                    var createOneShot = _vibrationEffect.CallStatic<AndroidJavaObject>("createOneShot", milliseconds, -1);
                    _vibrator.Call("vibrate", createOneShot);
                }
                else
                {
                    _vibrator.Call("vibrate", milliseconds);
                }
            }
        }
#endif

        private static int AndroidVersion
        {
            get
            {
                var iVersionNumber = 0;
                if (Application.platform == RuntimePlatform.Android)
                {
                    var androidVersion = SystemInfo.operatingSystem;
                    var sdkPos = androidVersion.IndexOf("API-", StringComparison.Ordinal);
                    iVersionNumber = int.Parse(androidVersion.Substring(sdkPos + 4, 2));
                }

                return iVersionNumber;
            }
        }
    }
}
