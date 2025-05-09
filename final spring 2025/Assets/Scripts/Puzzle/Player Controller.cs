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

    public bool isWalking = false;
    /*public bool isClimbing = false;
    public bool isIdle = true;
    Animator playerAnim;*/

    // tracking camera vertical and horizontal movement
    private float yRotation = 0;
    private float xRotation = 0;

    public GameObject respawnPos;

    public ItemPlace IPScript;

    // sounds
    AudioSource walkGrass;
    AudioSource walkStairs;
    float walkTimer;
    public AudioSource inAreaShine;
    public AudioSource placeClink;
    public AudioSource pickupDing;

    // Start is called before the first frame update

    void Start()
    {
        //playerAnim = GameObject.Find("PlayerCharacter").GetComponent<Animator>();
        walkTimer = 1f;

        Time.timeScale = 1f;
        controller = GetComponent<CharacterController>();

        // lock the cursor to the middle of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Awake()
    {
        // sound effects
        walkGrass = gameObject.GetComponents<AudioSource>()[0];
        walkStairs = gameObject.GetComponents<AudioSource>()[1];
        inAreaShine = gameObject.GetComponents<AudioSource>()[2];
        placeClink = gameObject.GetComponents<AudioSource>()[3];
        pickupDing = gameObject.GetComponents<AudioSource>()[4];
    }


    // Update is called once per frame
    void Update()
    {
        PlayerFollow();

        WalkSounds();

        // restart build, test keys
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            transform.position = new Vector3(-59,215,-2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            transform.position = new Vector3(97, 215, 294);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            SceneManager.LoadScene(1);
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
       
        if (depthInput != 0 || horizontalInput != 0)
        {
            isWalking = true;

        }
        else
        {
            isWalking = false;
        }
    }

    private void WalkSounds()
    {
        if (gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Columns"))
        {
            Debug.Log("Walking on stairs");
            walkStairs.Play();


        }
            walkTimer -= Time.deltaTime;

            if (walkTimer < 0)
            {
                walkGrass.pitch = Random.Range(0.8f, 1.0f);
                walkStairs.pitch = Random.Range(0.8f, 1.0f);
                walkTimer = 1;
            }

        /* else if (other.CompareTag("Stairs"))
            {
                
            }
        }

        else if (!isWalking)
        {
            walkGrass.Stop();
            walkStairs.Stop();

        }*/
    }
    

    private void MovePlayer()
    {
        onGround = controller.isGrounded;

        if (onGround)
        {
            isJumping = false;
            jumpForce = 20;

            //walkGrass.Play();

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
