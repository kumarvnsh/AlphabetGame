using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickablePrefab : MonoBehaviour
{
    public string prefabName;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public GameLogic gameLogic;

    //public int prefabIndex;
    // void OnMouseDown()
    // {
    //     // Debug.Log("Clicked on " + prefabName);
    //     // audioSource.Play();
    //     // StartCoroutine("delay");
    //     
    // }

    private void OnMouseDown()
    {
        // Get the index of the clicked prefab
        int prefabIndex = GetComponent<PrefabInfo>().prefabIndex;

        // Call the OnPrefabClicked function of the GameLogic script
        gameLogic.OnPrefabClicked(prefabIndex);
        
        // Debug statements
        if (prefabIndex == -1)
        {
            Debug.LogError("Prefab index not found for " + gameObject.name);
        }
        else
        {
            Debug.Log("Clicked on prefab " + prefabIndex + " (" + GameManager.Instance.prefabs[prefabIndex].name + ")");
        }
    }
    // IEnumerator delay()
    // {
    //     yield return new WaitForSeconds(0.5f);
    //     Destroy(gameObject);
    //     Spawner.instance.score += 1;
    //     Spawner.instance.scoreText.text = Spawner.instance.score.ToString();
    // }
}
