using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class GameLogic : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public static GameLogic instance;

    private void Start()
    {
        instance = this;
        score = 0;
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        if (score <= 0)
        {
            score = 0;
            scoreText.text = score.ToString();
        }
    }

    public void OnPrefabClicked(int prefabIndex)
    {
        Debug.Log("Clicked prefab index: " + prefabIndex);
        Debug.Log("Correct answer index: " + PlayerPrefs.GetInt("correctAnswerIndex"));
        
        // If the clicked prefab is the correct answer, increment the score
        if (prefabIndex == PlayerPrefs.GetInt("correctAnswerIndex"))
        {
            score++;
            scoreText.text = score.ToString();
            Debug.Log("correct");
        }
        // Otherwise, decrement the score
        else
        {
            score--;
            scoreText.text = score.ToString();
            Debug.Log("wrong");
        }

        // Destroy all of the prefabs and spawn new ones
        foreach (GameObject prefab in GameObject.FindGameObjectsWithTag("alphabets"))
        {
            Destroy(prefab);
        }

        SpawnPrefabs();
    }

    // This function is called by the GameManager to get the current score
    public int GetScore()
    {
        return score;
    }

   public void SpawnPrefabs()
{
    // Play a random sound
    AudioClip randomSound = GameManager.Instance.sounds[Random.Range(0, GameManager.Instance.sounds.Length)];
    AudioSource.PlayClipAtPoint(randomSound, Camera.main.transform.position);

    // Spawn four prefabs, including one that corresponds to the random sound
    int correctAnswerIndex = Random.Range(0, GameManager.Instance.prefabs.Length);
    PlayerPrefs.SetInt("correctAnswerIndex", correctAnswerIndex);

    // Create a list to hold the prefabs that have already been spawned
    List<GameObject> spawnedPrefabs = new List<GameObject>();

    for (int i = 0; i < GameManager.Instance.prefabs.Length; i++)
    {
        GameObject prefabToSpawn;
        if (i == correctAnswerIndex)
        {
            // Use the prefab that corresponds to the random sound
            prefabToSpawn = GameManager.Instance.prefabMap[randomSound];
        }
        else
        {
            // Use a random prefab that has not already been spawned
            do
            {
                prefabToSpawn = GameManager.Instance.prefabs[Random.Range(0, GameManager.Instance.prefabs.Length)];
            } while (spawnedPrefabs.Contains(prefabToSpawn));
        }

        // Spawn the prefab at a random position at the top of the screen, checking for overlaps
        Vector3 spawnPosition;
        bool overlaps;
        do
        {
            overlaps = false;
            spawnPosition = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            Collider[] hitColliders = Physics.OverlapSphere(spawnPosition, 1f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.CompareTag("alphabets"))
                {
                    overlaps = true;
                    break;
                }
            }
        } while (overlaps);

        GameObject prefabInstance = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        prefabInstance.GetComponent<Rigidbody>().velocity = new Vector3(0f, -GameManager.Instance.prefabSpeed, 0f);
        prefabInstance.tag = "alphabets";
        prefabInstance.GetComponent<ClickablePrefab>().gameLogic = this;

        // Add the spawned prefab to the list of spawned prefabs
        spawnedPrefabs.Add(prefabInstance);
    }
}

}   