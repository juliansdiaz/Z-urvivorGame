using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GunAmmo")) //Collect AmmoBox to refill ammo
        {
            GameManager.Instance.ammo += other.gameObject.GetComponent<AmmoBox>().ammoAmount; //Increase ammo count value
            Destroy(other.gameObject); //Destroy Ammo box game object
        }
        else if (other.gameObject.CompareTag("MedKit") && GameManager.Instance.health != 100) //Collect medkit if health is not complete
        {
            GameManager.Instance.health += 20; //Increase health value
            Destroy(other.gameObject); //Destroy Medkit game object
        }
        else if(other.gameObject.CompareTag("Keys")) //Collect the keys
        {
            Destroy(other.gameObject); //Destroy the keys game object
            GameManager.Instance.playerHasTheKeys = true; 
            GameManager.Instance.objectivesText[0].text = "<s>1- Find the airship's keys</s>"; //Mark UI text objective as complete
        }
        else if(other.gameObject.CompareTag("Fuel")) // Collect the fuel
        {
            Destroy(other.gameObject);
            GameManager.Instance.playerHasTheFuel = true;
            GameManager.Instance.objectivesText[1].text = "<s>2- Find the fuel</s>"; //Mark UI text objective as complete
        }
        else if(other.gameObject.CompareTag("Airship")) //Get into the airship
        {
            GameManager.Instance.WinGame();
        }
    }

    //Get hurt by the enemy
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) 
        {
            GameManager.Instance.health -= 5;
            Debug.Log(GameManager.Instance.health);
        }
    }

}
