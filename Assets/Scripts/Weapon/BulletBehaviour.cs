using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy")) //Check if bullet is colliding with enemy
        {
            Destroy(collision.gameObject); //Destroy enemy game object
        }
    }
}
