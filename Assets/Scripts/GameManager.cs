using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public GameObject[] prefabs;
    public float prefabSpeed = 3f;
    public GameLogic gameLogic;
    public static GameManager Instance;
    public Dictionary<AudioClip, GameObject> prefabMap = new Dictionary<AudioClip, GameObject>();
    public int spawnCount =4 ;
    private void Start()
    {
        Instance = this; 
        // Map each sound to its corresponding prefab
        for (int i = 0; i < sounds.Length; i++)
        {
            prefabMap[sounds[i]] = prefabs[i];
        }

        
        // Play a random sound at the start of the game
        AudioClip randomSound = sounds[Random.Range(0, sounds.Length)];
        AudioSource.PlayClipAtPoint(randomSound, Camera.main.transform.position);

        // Spawn four prefabs, including one that corresponds to the random sound
        int correctAnswerIndex = Random.Range(0, prefabs.Length);
        GameObject correctPrefab = prefabMap[randomSound];
        List<int> spawnedIndices = new List<int>(); // keep track of spawned indices
        bool hasSpawnedCorrectPrefab = false; // flag to check if correct prefab has been spawned
        for (int i = 0; i < prefabs.Length; i++) 
        {
           GameObject prefabToSpawn;
           Vector3 spawnPosition;
           if (i == correctAnswerIndex && !hasSpawnedCorrectPrefab)
           {
              // Use the prefab that corresponds to the random sound
              prefabToSpawn = correctPrefab;
              spawnPosition = GetSpawnPosition(Random.Range(0, 4));
              hasSpawnedCorrectPrefab = true;
           }
           else
           {
              // Use a random prefab, but make sure it hasn't been spawned before
              int randomIndex = Random.Range(0, prefabs.Length);
              while (spawnedIndices.Contains(randomIndex) || randomIndex == correctAnswerIndex)
              {
                randomIndex = Random.Range(0, prefabs.Length);
              }
              spawnedIndices.Add(randomIndex);
              prefabToSpawn = prefabs[randomIndex];
              spawnPosition = GetSpawnPosition(i);
           }

           // Spawn the prefab at the specified position
           GameObject prefabInstance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
           prefabInstance.GetComponent<Rigidbody>().velocity = new Vector3(0f, -prefabSpeed,0f);
           prefabInstance.tag = "alphabets";
           prefabInstance.GetComponent<ClickablePrefab>().gameLogic = gameLogic;
        }
    }
    

    // Returns a Vector3 representing the position to spawn a prefab at for a given index
    private Vector3 GetSpawnPosition(int index)
    {
        switch (index)
        {
            case 0:
                return new Vector3(-8f, 7f, 0f);
            case 1:
                return new Vector3(-4f, 7f, 0f);
            case 2:
                return new Vector3(4f, 7f, 0f);
            case 3:
                return new Vector3(8f, 7f, 0f);
            default:
                return new Vector3(0f,7f,0f);
        }
    }
}
