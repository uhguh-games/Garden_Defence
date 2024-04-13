using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JunkUITest : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventmanager;
    GameManager gameManager;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI textWinLose;

    void Start() 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        text.text = $@"Junkies: {gameManager.junk}
        Crop Health:  {gameManager.cropHealth}";
    }

    public void Win()
    {
        Debug.Log("Win UI");
        textWinLose.enabled= true;
        textWinLose.text = "You win";
    }
}
