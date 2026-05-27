using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

public static class BuildScript
{
    // 実行方法 (ターミナル):
    // /Applications/Unity/Hub/Editor/<VERSION>/Unity.app/Contents/MacOS/Unity -batchmode -quit -projectPath "<path>" -executeMethod BuildScript.PerformWebGLBuild -logFile build.log

    public static void PerformWebGLBuild()
    {
        var scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();
        if (scenes == null || scenes.Length == 0)
        {
            Debug.Log("No enabled scenes found — creating minimal Assets/Scenes/MainScene.unity for build.");

            // Ensure Scenes folder exists
            var scenesFolder = Path.Combine(Application.dataPath, "Scenes");
            if (!Directory.Exists(scenesFolder)) Directory.CreateDirectory(scenesFolder);

            // Create a new empty scene with a Camera and a Light
            var newScene = UnityEditor.SceneManagement.EditorSceneManager.NewScene(UnityEditor.SceneManagement.NewSceneSetup.DefaultGameObjects, UnityEditor.SceneManagement.NewSceneMode.Single);
            var cameraGO = GameObject.FindObjectOfType<UnityEngine.Camera>();
            if (cameraGO == null)
            {
                var cam = new GameObject("Main Camera");
                cam.AddComponent<UnityEngine.Camera>();
                cam.tag = "MainCamera";
            }
            var lightGO = GameObject.FindObjectOfType<UnityEngine.Light>();
            if (lightGO == null)
            {
                var light = new GameObject("Directional Light");
                var lightComp = light.AddComponent<UnityEngine.Light>();
                lightComp.type = LightType.Directional;
            }

            var scenePath = "Assets/Scenes/MainScene.unity";
            UnityEditor.SceneManagement.EditorSceneManager.SaveScene(newScene, scenePath);

            // Add to Build Settings
            var buildScenes = EditorBuildSettings.scenes.ToList();
            buildScenes.Add(new EditorBuildSettingsScene(scenePath, true));
            EditorBuildSettings.scenes = buildScenes.ToArray();

            scenes = new string[] { scenePath };
        }

        var projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
        var buildDir = Path.Combine(projectRoot, "Builds", "WebGL");
        if (!Directory.Exists(buildDir)) Directory.CreateDirectory(buildDir);

        var options = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildDir,
            target = BuildTarget.WebGL,
            options = BuildOptions.None
        };

        Debug.Log("Starting WebGL build to: " + buildDir);
        BuildReport report = BuildPipeline.BuildPlayer(options);
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("WebGL build succeeded: " + report.summary.totalSize + " bytes");
            EditorApplication.Exit(0);
        }
        else
        {
            Debug.LogError("WebGL build failed: " + report.summary.result);
            EditorApplication.Exit(1);
        }
    }
}
