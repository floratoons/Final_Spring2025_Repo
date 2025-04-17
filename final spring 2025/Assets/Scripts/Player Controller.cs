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
    private float gravity = -9.81f;
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
        jumpForce = 150f;
        controller = GetComponent<CharacterController>();

        // lock the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

        //PlayerFollow();

        // restart build
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

    }

    /*
    private const float YMin = -50.0f;
    private const float YMax = 50.0f;

    public Transform lookAt;

    public Transform Player;

    public float distance = 10.0f;
    private float currentX = 0.0f;
    private float currentY = 0.0f;
    public float sensivity = 4.0f;

    // Update is called once per frame
    void LateUpdate()
    {
        currentX += Input.GetAxis("Mouse X") * sensivity * Time.deltaTime;
        currentY += Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime;

        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Vector3 Direction = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        transform.position = lookAt.position + rotation * Direction;

        transform.LookAt(lookAt.position);
    }
    */
    
    private void PlayerFollow()
    {
        // getting mouse inputs and assigning them to variables
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // when mouse moves horizontally, player rotates around the y axis to look left/right
        // stores the mousex and adds to it
        yRotation += mouseX;

        //rotate the player left/right on y axis
        transform.rotation = Quaternion.Euler(0f, yRotation, 0);

        // decrease the x rotation when moving the mouse up, so the camera tilts up
        // increase the x rotation when moving the camera down so it tilts downwards
        /*xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90); //prevent looking Too far back*/

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);

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
