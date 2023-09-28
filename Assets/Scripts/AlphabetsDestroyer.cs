using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlphabetsDestroyer : MonoBehaviour
{

    public int counter;
    private bool isGameOver = false;

    private void Update()
    {
        if (counter == 5)
        {
            isGameOver = true;
        }

        if (isGameOver)
        {
            Debug.Log("GameOver");
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("alphabets"))
        {
            Destroy(other.gameObject);
            // Spawner.instance.score -= 1;
            // Spawner.instance.scoreText.text = Spawner.instance.score.ToString();
            
            counter += 1;
        }
    }
}
