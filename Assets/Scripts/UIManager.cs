using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseBtn;

    public GameObject pausePanel;

    public void Pause()
    {
        pauseBtn.SetActive(false);
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseBtn.SetActive(true);
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void Main()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;
    }

    
}
