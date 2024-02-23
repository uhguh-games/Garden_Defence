using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JunkUITest : MonoBehaviour
{

    [SerializeField] GameManager gameManager;
    [SerializeField] private Text text;


    void Update()
    {
        text.text = $"Junkies: {gameManager.junk}";
    }
}
