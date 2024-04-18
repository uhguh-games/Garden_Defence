using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject optionsCanvas;
    [SerializeField] private GameObject controlsCanvas;
    [SerializeField] private int SceneID;

    private void Start()
    {
        optionsCanvas.SetActive(false);
        mainCanvas.SetActive(true);
        controlsCanvas.SetActive(false);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneID);
    }

    public void ShowSettings()
    {
        optionsCanvas.SetActive(true);
        // mainCanvas.SetActive(false);
    }
    public void HideSettings()
    {
        optionsCanvas.SetActive(false);
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
