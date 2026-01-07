using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/Game.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "GuessTheChicken.aab";
        string apkPath = "GuessTheChicken.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64 = "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";
        string keystorePass = "all123";
        string keyAlias = "king";
        string keyPass = "all123";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}
