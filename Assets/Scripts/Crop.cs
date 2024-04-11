using System.Collections;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Crop : MonoBehaviour
{
    [SerializeField] private EventManagerSO eventManager;

    [SerializeField] private int eatingTime;



    private void OnTriggerEnter(Collider other)
    {
         if (other.gameObject.tag == "Monster")
         {
             Debug.Log("AAFGAAGFAFAWFAEWFAEWSFRWEAFRWAESOIFRWESIOGFHUWERIOGFHUWSIOEJHUFIOWEJHFRIOWEHJFIOWSEJUIOGVFJHUEWRIOGJHUIOWSEJHU");
            StartCoroutine(eating());
         }
    }

 IEnumerator eating()
 {
    yield return new WaitForSeconds(eatingTime);
    Destroy(this.gameObject);
     eventManager.CropEaten();
 }
}

public void YeatCrop()
{
    StartCoroutine(eating());
}
