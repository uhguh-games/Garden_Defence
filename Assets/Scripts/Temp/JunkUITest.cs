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
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI textWinLose;

    EconomyManager economyManager;

    void Start() 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        economyManager = GameObject.Find("EconomyManager").GetComponent<EconomyManager>();
    }


    void Update()
    {
        text.text = $"{economyManager.playersJunk}";
        healthText.text = $"{gameManager.cropHealth}";
    }

    public void Win()
    {
        Debug.Log("Win UI");
        textWinLose.enabled= true;
        textWinLose.text = "You win";
    }
}
