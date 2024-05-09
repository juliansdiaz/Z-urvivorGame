using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Variables
    [SerializeField] float movementSpeed = 10.0f;
    [SerializeField] float jumpHeight = 2.0f;
    [SerializeField] Transform groundCheck;
    [SerializeField] float sphereRadius = 0.3f;
    [SerializeField] LayerMask groundMask;

    public Animator playerAnimator;
    float gravityScale = -9.81f;
    CharacterController playerController;
    Vector3 playerVelocity;
    bool isOnGround;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        PlayerJump();

        if(GameManager.Instance.health <= 0)
        {
            PlayerDeath();
        }
    }

    void MovePlayer()
    {
        //Get mouse input
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        //Play movement animation according to the movement value
        playerAnimator.SetFloat("VelX", xMove);
        playerAnimator.SetFloat("VelZ", zMove);

        //Set movement direction
        Vector3 Movement = transform.right * xMove + transform.forward * zMove;
        playerController.Move(Movement * movementSpeed * Time.deltaTime);

        playerVelocity.y += gravityScale * Time.deltaTime; //Make gravity affect the player
        playerController.Move(playerVelocity * Time.deltaTime); //Move player

        isOnGround = Physics.CheckSphere(groundCheck.position, sphereRadius, groundMask); //Check if player is on ground

        //push player down after jumping
        if (isOnGround && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f;
        }
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround) //Get player input
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2 * gravityScale);
            playerAnimator.SetBool("isJumping", true); //Play jump animation
        }

        if (!isOnGround)
        {
            playerAnimator.SetBool("isJumping", false); //Stop jump animation
        }
    }

    void PlayerDeath()
    {
        GameManager.Instance.GameOver();
    }
}
