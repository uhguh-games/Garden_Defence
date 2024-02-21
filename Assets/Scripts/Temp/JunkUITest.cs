using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JunkUITest : MonoBehaviour
{
    [SerializeField] EventManagerSO eventManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] private Text text;

    private void OnEnable()
    {
        eventManager.onKill += JunkUpdate;
    }
    private void OnDisable()
    {
        eventManager.onKill -= JunkUpdate;
    }

    private void Awake()
    {
        JunkUpdate();
    }
    void JunkUpdate()
    {
        text.text = $"Junkies: {gameManager.junk}";
    }
}
