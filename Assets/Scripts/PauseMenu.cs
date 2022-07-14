using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject canvas;
    private static List<Transform> changedUI;
    

    private List<Transform> GetObjectsInLayer(GameObject canvas, int layer)
    {
        List<Transform> ret = new List<Transform>();
        foreach (Transform t in canvas.GetComponentsInChildren(typeof(Transform), false))
        {
            if (t.gameObject.layer == layer && t.gameObject.activeSelf == true)
            {
                if (t.gameObject != canvas)
                    ret.Add(t);
            }
        }
        return ret;
    }
    public void Resume()
    {
        if (changedUI != null)
        {
            foreach (Transform getObject in changedUI)
            {
                getObject.gameObject.SetActive(true);
            }
        }
        GameManager.gm.background.SetActive(false);
        GameManager.gm.pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        changedUI.Clear();
    }

    public void Pause()
    {
        changedUI = GetObjectsInLayer(canvas, 5);
        foreach (Transform getObject in changedUI)
        {
            getObject.gameObject.SetActive(false);
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
