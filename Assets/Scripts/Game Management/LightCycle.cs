using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightCycle : MonoBehaviour
{
    [SerializeField] private Light directionalLight;
    [SerializeField] float lerpSpeed = 0.3f;

    public Quaternion morningRotation = Quaternion.Euler(178f, 0f, 0f);
    public Quaternion dayRotation = Quaternion.Euler(90f, 0f, 0f);
    public Quaternion eveningRotation = Quaternion.Euler(-90f, 0f, 0f);
    public Quaternion nightRotation = Quaternion.Euler(2f, 0f, 0f);

   public void StartLightRoutine(Quaternion targetRotation)
    {
        StartCoroutine(ChangeLightRoutine(targetRotation));
    }

    IEnumerator ChangeLightRoutine(Quaternion targetRotation)
    {
        Quaternion startRotation = directionalLight.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < 1f) // This loop ensures smooth rotation
        {
            elapsedTime += Time.deltaTime * lerpSpeed;
            directionalLight.transform.rotation = Quaternion.Lerp(startRotation, targetRotation, elapsedTime);
            yield return null; // Wait for the next frame
        }
    }
}