using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    public void Fade(Image image, bool fadeOut)
    {
        StartCoroutine(FadeRoutine(image, fadeOut));
    }

    IEnumerator FadeRoutine(Image image, bool fadeOut)
    {
        if (fadeOut) 
        {
            for (float f = 1f; f >= -0.05f; f -= 0.05f)
            {
                Color color = image.color;
                color.a = f;
                image.color = color;
                yield return new WaitForSeconds(0.05f);
            }
        } 
        else 
        {
            for (float f = 0f; f <= 1.05f; f += 0.05f)
            {
                Color color = image.color;
                color.a = f;
                image.color = color;
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
