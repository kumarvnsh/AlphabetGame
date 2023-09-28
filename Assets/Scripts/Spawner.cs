using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public static Spawner instance;
    public GameObject[] prefabsToSpawn;
    public float spawnSpeed;
    public float moveSpeed;

    public Text scoreText;
    public int score;

    IEnumerator SpawnPrefabs()
    {
        while(true)
        {
            // Choose a random prefab from the array
            GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Length)];
    
            // Instantiate the prefab at the top of the screen
            GameObject newObject =Instantiate(prefab, new Vector3(Random.Range(-7f, 7f), 10f, 0f), Quaternion.identity);
            
            // Get the Rigidbody component of the new object
            Rigidbody rb = newObject.GetComponent<Rigidbody>();
            
            // Set the velocity of the Rigidbody to move the object down
            rb.velocity = new Vector3(0f, -moveSpeed, 0f);
            
            // Add an OnMouseDown event to the new object
            //newObject.GetComponent<ClickablePrefab>().prefabName = prefab.name;
            ClickablePrefab clickablePrefab = newObject.GetComponent<ClickablePrefab>();
            clickablePrefab.prefabName = prefab.name;
            clickablePrefab.audioSource = newObject.GetComponent<AudioSource>();
            clickablePrefab.audioSource.clip = prefab.GetComponent<ClickablePrefab>().audioClip;


            // Wait for the next spawn time
            yield return new WaitForSeconds(1f / spawnSpeed);
        }
    }
    
    void Start()
    {
        StartCoroutine(SpawnPrefabs());
        instance = this;
    }

    private void Update()
    {
        if (score <= 0)
        {
            score = 0;
            scoreText.text = score.ToString();
        }

        if (score >= 10 && score < 20)
        {
            spawnSpeed = 0.6f;
            moveSpeed = 4f;
        }
        else if (score >= 20 && score < 40)
        {
            spawnSpeed = 0.8f;
            moveSpeed = 5f;
        }
        else if (score >= 40)
        {
            spawnSpeed = 1.0f;
            moveSpeed = 6f;
        }
    }
}
