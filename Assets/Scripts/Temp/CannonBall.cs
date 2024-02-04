using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    /*
    This is a script used for testing - it will be deleted later
    */

    [SerializeField] float cannonSpeed = 5f;
    [SerializeField] float cannonDamage = 1f;

    private void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * cannonSpeed);

        Invoke("DeleteCannonBall", 6f);
    }

    private void DeleteCannonBall() 
    {
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other) 
    {
        Monster monster = other.GetComponent<Monster>();

        if (other.tag == "Monster") 
        {
            if (monster != null) 
            {
                monster.TakeDamage(cannonDamage);
            }

            DeleteCannonBall();
        }
    }
}
