using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : MonoBehaviour
{
    [System.Serializable]
    public class SpawnType
    {
        public GameObject prefab;
        public int weight;
    }

    public SpawnType[] spawnTypes;
    public int poolSizePerType = 10;
    public float spawnInterval = 0.8f;
    public float objectLifetime = 6f;
    public Vector2 spawnArea = new Vector2(14f, 10f);
    public float minimumSize = 0.7f;
    public float maximumSize = 1.5f;

    private List<PooledObject>[] pools;
    private int collected;
    private Rect panel = new Rect(10, 10, 300, 180);

    private void Start()
    {
        pools = new List<PooledObject>[spawnTypes.Length];

        // Create every object once. Spawning later only reuses these objects.
        for (int type = 0; type < spawnTypes.Length; type++)
        {
            pools[type] = new List<PooledObject>();
            GameObject parent = new GameObject(spawnTypes[type].prefab.name + " Pool");
            parent.transform.SetParent(transform);

            for (int i = 0; i < poolSizePerType; i++)
            {
                GameObject instance = Instantiate(spawnTypes[type].prefab, parent.transform);
                instance.name = spawnTypes[type].prefab.name + " " + (i + 1);
                instance.SetActive(false);
                pools[type].Add(instance.GetComponent<PooledObject>());
            }
        }

        for (int i = 0; i < 3; i++)
            SpawnRandomObject();

        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnRandomObject();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnRandomObject();

        if (Input.GetKeyDown(KeyCode.R))
            ClearObjects();

        Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);
        if (Input.GetMouseButtonDown(0) && !panel.Contains(mouse))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                PooledObject item = hit.collider.GetComponent<PooledObject>();
                if (item != null)
                {
                    collected++;
                    item.Deactivate();
                }
            }
        }
    }

    public void SpawnRandomObject()
    {
        // Weighted random table: a weight of 80 is four times as likely as 20.
        int totalWeight = 0;
        foreach (SpawnType type in spawnTypes)
            totalWeight += type.weight;

        int roll = Random.Range(0, totalWeight);
        int chosenType = 0;
        for (int i = 0; i < spawnTypes.Length; i++)
        {
            if (roll < spawnTypes[i].weight)
            {
                chosenType = i;
                break;
            }
            roll -= spawnTypes[i].weight;
        }

        foreach (PooledObject item in pools[chosenType])
        {
            if (!item.gameObject.activeSelf)
            {
                float size = Random.Range(minimumSize, maximumSize);
                float x = Random.Range(-spawnArea.x / 2f, spawnArea.x / 2f);
                float z = Random.Range(-spawnArea.y / 2f, spawnArea.y / 2f);
                item.Activate(new Vector3(x, size / 2f, z), size, objectLifetime);
                return;
            }
        }
    }

    public void ClearObjects()
    {
        foreach (List<PooledObject> pool in pools)
            foreach (PooledObject item in pool)
                item.Deactivate();
    }

    private void OnGUI()
    {
        if (pools == null)
            return;

        int active = 0;
        foreach (List<PooledObject> pool in pools)
            foreach (PooledObject item in pool)
                if (item.gameObject.activeSelf)
                    active++;

        GUILayout.BeginArea(panel, GUI.skin.box);
        GUILayout.Label("Random Object Spawner");
        GUILayout.Label("Click a shape to collect it.");
        GUILayout.Label("Collected: " + collected + "   Active: " + active + "/" + (poolSizePerType * spawnTypes.Length));
        foreach (SpawnType type in spawnTypes)
            GUILayout.Label(type.prefab.name + " weight: " + type.weight);
        GUILayout.Label("Objects disappear after " + objectLifetime + " seconds.");
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("Spawn (Space)"))
            SpawnRandomObject();
        if (GUILayout.Button("Clear (R)"))
            ClearObjects();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
}
