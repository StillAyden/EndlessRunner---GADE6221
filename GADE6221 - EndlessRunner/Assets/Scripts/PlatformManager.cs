using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{

    [Header("Platforms")]
    [SerializeField] GameObject[] platformPrefabs;
    [SerializeField] List<GameObject> loadedPlatforms;
    float[] platformSpawnPoints;

    [Header("Spawning/Despawning")]
    [SerializeField] float platformPrefabSize = 10f;
    [SerializeField] int maxPlatformsLoaded = 5;
    int maxPlatformsChanged;
    [SerializeField] float despawnPoint = -10;

    [Header("Movement")]
    public float moveSpeed = 1f;

    private void Awake()
    {
        loadedPlatforms = new List<GameObject>();
        platformSpawnPoints = new float[maxPlatformsLoaded];

        PopulateSpawnPoints();
    }
    private void Start()
    {
        InitialPlatformSpawn(); 
    }
    private void Update()
    {
        //Check if amount of platforms changed, to add more spawnPoints
        if (maxPlatformsLoaded != maxPlatformsChanged)
        {
            PopulateSpawnPoints();
            maxPlatformsChanged = maxPlatformsLoaded;
        }

        TileManagement();


    }
    void TileManagement()
    {
        for (int k = 0; k < maxPlatformsLoaded; k++)
        {
            if (loadedPlatforms.Count < maxPlatformsLoaded)
            {
                SpawnNextPlatform(k);
            }

            MovePlatform(k);
            DespawnPlatform(k);
         
        }
    }

    private void MovePlatform(int index)
    {
        loadedPlatforms[index].transform.position = new Vector3(loadedPlatforms[index].transform.position.x,
                                                                loadedPlatforms[index].transform.position.y,
                                                                loadedPlatforms[index].transform.position.z + -moveSpeed * Time.deltaTime);
         
        ////Check for gaps between platforms
        //if (Vector3.Distance(loadedPlatforms[loadedPlatforms.Count - 1].transform.position, loadedPlatforms[loadedPlatforms.Count - 2].transform.position) < platformPrefabSize)
        //{
        //    float difference = (Vector3.Distance(loadedPlatforms[loadedPlatforms.Count - 1].transform.position, loadedPlatforms[loadedPlatforms.Count - 2].transform.position)) % 1;
        //    Debug.Log("Less " + difference);
        //    loadedPlatforms[loadedPlatforms.Count - 1].transform.position = new Vector3(0, 0, loadedPlatforms[loadedPlatforms.Count - 1].transform.position.z + difference);
        //}
        //else if (Vector3.Distance(loadedPlatforms[loadedPlatforms.Count - 1].transform.position, loadedPlatforms[loadedPlatforms.Count - 2].transform.position) > platformPrefabSize)
        //{
        //    float difference = (Vector3.Distance(loadedPlatforms[loadedPlatforms.Count - 1].transform.position, loadedPlatforms[loadedPlatforms.Count - 2].transform.position)) % 1;
        //    Debug.Log("More " + difference);
        //    loadedPlatforms[loadedPlatforms.Count - 1].transform.position = new Vector3(0, 0, loadedPlatforms[loadedPlatforms.Count - 1].transform.position.z - difference);
        //}
    }

    void PopulateSpawnPoints()
    {
        platformSpawnPoints = new float[maxPlatformsLoaded];
        //Spawn points
        for (int i = 0; i < maxPlatformsLoaded; i++)
        {
                platformSpawnPoints[i] = i * platformPrefabSize;
        }    
    }


    void InitialPlatformSpawn()
    {
        //Check if there are usable prefabs
        if (platformPrefabs.Length > 0)
        {
            for (int k = 0; k < maxPlatformsLoaded; k++)
            {
                //Spawn specified amount of empty tiles to begin with
                loadedPlatforms.Add(Instantiate(platformPrefabs[0], new Vector3(0, 0, platformSpawnPoints[k]), Quaternion.identity));
                loadedPlatforms[k].layer = 6;   
            }
        }
        else Debug.LogError("Array 'PlatformPrefabs' is Empty");
    }
    void SpawnNextPlatform(int index)
    {
        loadedPlatforms.Add(Instantiate(GetRandomPrefab(), new Vector3(0, 0, platformSpawnPoints[maxPlatformsLoaded - 1]), Quaternion.identity));
        loadedPlatforms[loadedPlatforms.Count - 1].layer = 6;
        
      
    }
    void DespawnPlatform(int index)
    {
        if (loadedPlatforms[index].transform.position.z < despawnPoint)
        {
            Destroy(loadedPlatforms[index]);
            loadedPlatforms.RemoveAt(index);
        }
    }
    GameObject GetRandomPrefab()
    {
        int rand = Random.Range(0, platformPrefabs.Length);

        return platformPrefabs[rand];
    }
}
