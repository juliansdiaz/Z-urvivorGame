using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    //Variables
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject gunBullet;
    [SerializeField] float shootForce = 1500.0f;
    [SerializeField] float shootingRate = 0.5f;
    [SerializeField] AudioClip shootSound;

    AudioSource gunAudio;
    float shootingRateTime = 0f;

    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void PlaySound(AudioClip sfx)
    {
        gunAudio.PlayOneShot(sfx);
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1")) //Get player input
        {
            if ((Time.time > shootingRateTime) && GameManager.Instance.ammo > 0 ) //Check if shootingRateTime has passed and player has enough ammo
            {
                PlaySound(shootSound); //Play shootSound
                GameManager.Instance.ammo--; //Reduce ammo count by 1
                
                GameObject newBulletInstance;
                newBulletInstance = Instantiate(gunBullet, spawnPoint.position, Quaternion.Euler(Vector3.up)); //Instance new bullet game object

                newBulletInstance.GetComponent<Rigidbody>().AddForce(spawnPoint.forward * shootForce); //Push bullet game object with shootForce

                shootingRateTime = Time.time + shootingRate; //count shootingRateTime

                Destroy(newBulletInstance, 0.3f); //Destroy bullet game object after certain period of time
            }
            
        }
    }
}
