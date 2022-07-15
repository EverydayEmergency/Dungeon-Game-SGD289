using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject canvas;
    public List<GameObject> changedUI = new List<GameObject>();
    public Button pauseButton;
    public Button backButton;

    private void Start()
    {
        pauseButton.onClick.AddListener(Pause);
        backButton.onClick.AddListener(Resume);
    }
   
    //private void GetObjectsInLayer(GameObject canvas, int layer)
    //{
    //    List<RectTransform> ret = new List<RectTransform>();
    //    foreach (RectTransform t in canvas.GetComponentsInChildren(typeof(Transform), false))
    //    {
    //        Debug.Log(t.name);
    //        if (t.gameObject.name != "Canvas")
    //        {
    //            changedUI.Add(t);
    //        }
    //    }       
    //}
    public void Resume()
    {
        for (int i = 0; i < changedUI.Count; i++)
        {
            changedUI[i].SetActive(!changedUI[i].activeSelf);
        }

        GameManager.gm.background.SetActive(false);
        GameManager.gm.pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;      
    }

    public void Pause()
    {
        for (int i = 0; i < changedUI.Count; i++)
        {
            changedUI[i].SetActive(!changedUI[i].activeSelf);
        }
        GameManager.gm.background.SetActive(true);
        GameManager.gm.pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    //public void LoadMenu()
    //{
    //    Time.timeScale = 1f;
    //    SceneManager.LoadScene("MainMenu");
    //}

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
