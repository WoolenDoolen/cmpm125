using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public static class SceneSetup
{
    [MenuItem("Tools/Create Assignment 3 Scene")]
    public static void CreateScene()
    {
        System.IO.Directory.CreateDirectory("Assets/Materials");
        System.IO.Directory.CreateDirectory("Assets/Prefabs");
        System.IO.Directory.CreateDirectory("Assets/Scenes");
        AssetDatabase.Refresh();
        EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);

        Material floorMaterial = CreateMaterial("Floor", new Color(0.5f, 0.5f, 0.5f));
        Material cubeMaterial = CreateMaterial("Blue", new Color(0.15f, 0.4f, 0.85f));
        Material sphereMaterial = CreateMaterial("Yellow", new Color(1f, 0.8f, 0.15f));

        GameObject cube = CreatePrefab("Cube", PrimitiveType.Cube, cubeMaterial);
        GameObject sphere = CreatePrefab("Sphere", PrimitiveType.Sphere, sphereMaterial);

        GameObject floor = GameObject.CreatePrimitive(PrimitiveType.Plane);
        floor.name = "Floor";
        floor.transform.localScale = new Vector3(2f, 1f, 1.6f);
        floor.GetComponent<Renderer>().sharedMaterial = floorMaterial;

        GameObject cameraObject = new GameObject("Main Camera");
        cameraObject.tag = "MainCamera";
        Camera camera = cameraObject.AddComponent<Camera>();
        cameraObject.AddComponent<AudioListener>();
        camera.transform.position = new Vector3(0f, 13f, -17f);
        camera.transform.LookAt(Vector3.zero);
        camera.fieldOfView = 50f;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = new Color(0.2f, 0.25f, 0.3f);

        Light light = new GameObject("Directional Light").AddComponent<Light>();
        light.type = LightType.Directional;
        light.transform.rotation = Quaternion.Euler(50f, -30f, 0f);
        light.intensity = 1.2f;
        light.shadows = LightShadows.Soft;
        RenderSettings.ambientLight = new Color(0.55f, 0.55f, 0.55f);
        QualitySettings.shadows = ShadowQuality.All;
        QualitySettings.shadowDistance = 50f;

        WorldManager manager = new GameObject("World Manager").AddComponent<WorldManager>();
        manager.spawnTypes = new WorldManager.SpawnType[]
        {
            new WorldManager.SpawnType { prefab = cube, weight = 80 },
            new WorldManager.SpawnType { prefab = sphere, weight = 20 }
        };

        EditorSceneManager.SaveScene(manager.gameObject.scene, "Assets/Scenes/GenerativeWorld.unity");
        EditorBuildSettings.scenes = new EditorBuildSettingsScene[]
        {
            new EditorBuildSettingsScene("Assets/Scenes/GenerativeWorld.unity", true)
        };
        PlayerSettings.productName = "Assignment 3";
        PlayerSettings.companyName = "CMPM 125";
        PlayerSettings.defaultScreenWidth = 960;
        PlayerSettings.defaultScreenHeight = 640;
        PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
        PlayerSettings.resizableWindow = true;
        AssetDatabase.SaveAssets();
        Debug.Log("Assignment 3 scene created.");
    }

    private static Material CreateMaterial(string name, Color color)
    {
        string path = "Assets/Materials/" + name + ".mat";
        Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
        if (material == null)
        {
            material = new Material(Shader.Find("Standard"));
            AssetDatabase.CreateAsset(material, path);
        }
        material.color = color;
        material.SetFloat("_Glossiness", 0f);
        return material;
    }

    private static GameObject CreatePrefab(string name, PrimitiveType shape, Material material)
    {
        GameObject item = GameObject.CreatePrimitive(shape);
        item.name = name;
        item.GetComponent<Renderer>().sharedMaterial = material;
        item.AddComponent<PooledObject>();
        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(item, "Assets/Prefabs/" + name + ".prefab");
        Object.DestroyImmediate(item);
        return prefab;
    }

    public static void BuildGame()
    {
        System.IO.Directory.CreateDirectory("Build");
        var report = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes,
            "Build/Assignment3.exe", BuildTarget.StandaloneWindows64, BuildOptions.None);
        if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
            throw new System.Exception("Build failed: " + report.summary.result);
    }
}
