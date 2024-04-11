using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JunkUITest : MonoBehaviour
{

    GameManager gameManager;
    [SerializeField] private TextMeshProUGUI text;

    void Start() 
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        text.text = $@"Junkies: {gameManager.junk}
        Crop Health:  {gameManager.cropHealth}";
    }
}
