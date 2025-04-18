using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterPlayer : MonoBehaviour
{
    private Vector3 playerMovementInput;
    private Vector3 velocity;
    private CharacterController controller;
    public float walkSpeed;
    public float jumpForce;
    private float currentSpeed;

    // camera
    public float mouseSensitivity;
    public Transform cameraTransform;

    // jumping
    private float gravity = -10f;
    public bool onGround;
    public bool isJumping = false;

    // tracking camera vertical and horizontal movement
    private float yRotation = 0;
    private float xRotation = 0;

    public GameObject respawnPos;

    // sounds
    // AudioSource walkSound;

    // Start is called before the first frame update
    void Start()
    {
        /*if (PlayerPrefs.GetFloat(SceneManager.GetActiveScene().buildIndex + "x") != 0 && PlayerPrefs.GetFloat(SceneManager.GetActiveScene().buildIndex + "y") != 0)
        {
            transform.position = new Vector3(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().buildIndex + "x"), PlayerPrefs.GetFloat(SceneManager.GetActiveScene().buildIndex + "y"));
        }*/

        Time.timeScale = 1f;
        controller = GetComponent<CharacterController>();

        // lock the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        PlayerFollow();

        // restart build
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    private void PlayerFollow()
    {
        // getting mouse inputs and assigning them to variables
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // when mouse moves horizontally, player rotates around the y axis to look left/right
        // stores the mousex and adds to it
        yRotation += mouseX;

        //rotate the player left/right on y axis
        transform.rotation = Quaternion.Euler(0f, yRotation, 0);

        // decrease the x rotation when moving the mouse up, so the camera tilts up
        // increase the x rotation when moving the camera down so it tilts downwards
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30, 30); //prevent looking Too far back

        //cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        MovePlayer();

        float horizontalInput = Input.GetAxis("Horizontal");
        float depthInput = Input.GetAxis("Vertical");

        velocity = (transform.forward * depthInput * walkSpeed)
            + (transform.right * horizontalInput * walkSpeed)
            + (Vector3.up * velocity.y);
    }
    

    private void MovePlayer()
    {
        onGround = controller.isGrounded;

        if (onGround)
        {
            isJumping = false;
            jumpForce = 10;

            velocity.y = -1;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                velocity.y = jumpForce;
            }
        }
        else
        {
            velocity.y -= gravity * -2f * Time.deltaTime;
        }

        controller.Move(playerMovementInput * currentSpeed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);
    }
}
