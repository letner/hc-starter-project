using System;
using System.IO;
using UnityEngine;

namespace HyperCasualSDK.Tools
{
#if UNITY_EDITOR
    public class ScreenshotRecorder : MonoBehaviour
    {
        public bool captureScreenshots;
        public int framesBetweenShots;
        public string screenshotsSubfolder;

        private int _shotNumber;

        private string _fileNamePrefix;
        private string _screenshotsFolderPath;

        private void Start()
        {
            _fileNamePrefix = $"Screenshot-{DateTime.Now.ToShortTimeString().Trim()}-";
            _screenshotsFolderPath = $"Recordings/Screenshots/{screenshotsSubfolder}/";
            if (!Directory.Exists(_screenshotsFolderPath))
            {
                Directory.CreateDirectory(_screenshotsFolderPath);
            }
        }

        private void Update()
        {
            if (captureScreenshots && Time.frameCount % framesBetweenShots == 0) {
                var screenshotFileName = $"{_fileNamePrefix}{_shotNumber:0000}.png";
                ScreenCapture.CaptureScreenshot(_screenshotsFolderPath + screenshotFileName);
                Debug.Log($"Screenshot: {screenshotFileName}");
                _shotNumber++;
            }
        }
    }
#endif
}