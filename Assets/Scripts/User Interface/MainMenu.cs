using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject optionsPanel;
    [SerializeField] GameObject guidePanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject backButton;
    [SerializeField] int SceneID;
    [SerializeField] List <GameObject> panels = new List<GameObject>();

    private void Start()
    {
        mainPanel.SetActive(true);
        optionsPanel.SetActive(false);
        guidePanel.SetActive(false);
        creditsPanel.SetActive(false);
        backButton.SetActive(false);
        menuPanel.SetActive(false);
    }

    public void HideOtherPanels(GameObject activatedPanel) 
    {
        foreach (GameObject panel in panels) 
        {
            if (panel != activatedPanel) 
            {
                activatedPanel.SetActive(true);
                panel.SetActive(false);
            }
        }
    }
    public void OpenMenu() 
    {
        HideOtherPanels(optionsPanel);
        menuPanel.SetActive(true);
        backButton.SetActive(true);
    }

    public void OpenMain() 
    {
        HideOtherPanels(mainPanel);
        menuPanel.SetActive(false);
        backButton.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

}
