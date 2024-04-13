using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject settingsCanvas;
    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private int SceneID;

    private void Start()
    {
        settingsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneID);
    }

    public void ShowSettings()
    {
        settingsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }
    public void HideSettings()
    {
        settingsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
    public void ShowControls()
    {
        controlsCanvas.SetActive(true);
        mainCanvas.SetActive(false);
    }
    public void HideControls() 
    {
        controlsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
    }
    public void CloseGame()
    {
        Application.Quit();
    }

}
