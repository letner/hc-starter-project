using System;
using UnityEditor;

[InitializeOnLoad]
public static class JavaHomeFix
{
    static JavaHomeFix()
    {
        var newJdkPath = EditorApplication.applicationPath.Replace("Unity.app", "PlaybackEngines/AndroidPlayer/OpenJDK");
        if (Environment.GetEnvironmentVariable("JAVA_HOME") != newJdkPath) {
            Environment.SetEnvironmentVariable("JAVA_HOME", newJdkPath);
        }
    }
}
