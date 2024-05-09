using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraLook : MonoBehaviour
{
    //Variables
    [SerializeField] float mouseSensitivity = 350.0f;
    [SerializeField] float xRotation = 0f;

    public Transform playerBody;


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor in game mode
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        //Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotate camera vertically
        xRotation -= mouseY;
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0); 

        xRotation = Mathf.Clamp(xRotation, -20f, 62f); //Set vertical rotation limits

        playerBody.Rotate(Vector3.up * mouseX); //Rotate camera horizontally
    }
}
