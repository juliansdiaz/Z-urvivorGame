using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    //Variables
    [SerializeField] float swayIntensity = 20.0f;

    Quaternion startRot;

    // Start is called before the first frame update
    void Start()
    {
        startRot = transform.localRotation; //set startRot to weapon's current rotation
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSway();
    }

    void WeaponSway()
    {
        //Get mouse input
        float swayX = Input.GetAxis("Mouse X");
        float swayY = Input.GetAxis("Mouse Y");

        Quaternion xAngle = Quaternion.AngleAxis(swayX * -1.25f, Vector3.up); //Set sway xangle and intensity
        Quaternion yAngle = Quaternion.AngleAxis(swayY * -1.25f, Vector3.right);//Set sway yangle and intensity

        Quaternion targetRot = startRot * xAngle * yAngle; //set sway rotation

        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRot, Time.deltaTime * swayIntensity); //shake weapon according to sway rotation
    }
}
