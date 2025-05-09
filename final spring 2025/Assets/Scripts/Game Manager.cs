using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    MusicManager MMScript;
    ButtonManager BMScript;
    SceneTransitioner STScript;
    DialogueManager DMScript;
    CameraShift cameraScript;

    public Canvas menu;
    public Canvas dialoguecanvas;
    public Canvas wincanvas;
    public Image menubutton;

    // puzzle game

    public GameObject red;
    public GameObject blue;
    public GameObject purple;

    public GameObject clue;

    //bool solvedPuzzle1 = false;


    // chapters:
    // last "currentline" from dialogue manager for the last dialogue line read
    // last "currentchapter" for the last chapter (all dialoguesections in a location in a day) finished
    // 



    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GM");

        /*if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);*/

    }

    private void Start()
    {
        MMScript = GameObject.Find("MM").GetComponent<MusicManager>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MMScript.MusicTadpole();

        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            MMScript.MusicAbstract();

            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;

            DMScript = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();

            // find the menu & notebook 
            menu = GameObject.FindWithTag("Menu").GetComponent<Canvas>();
            dialoguecanvas = GameObject.Find("DialogueBox").GetComponent<Canvas>();


            STScript = GameObject.Find("Camera").GetComponent<SceneTransitioner>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            MMScript.MusicWater();

            //cameraScript = GameObject.Find("Fade").GetComponent<CameraShift>();

        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                menu.enabled = true;
                dialoguecanvas.enabled = false;
            }
            if (menu == true && Input.GetKey(KeyCode.Escape))
            {
                menu.enabled = false;
                dialoguecanvas.enabled = true;
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                menubutton.enabled = false;
                menu.enabled = true;
            }
            
            if (menu == true && Input.GetKey(KeyCode.Escape))
            {
                menubutton.enabled = true;
                menu.enabled = false;
            }
        }
    }


    public void puzzleSolve()
    {
        StartCoroutine(cameraScript.CameraSwitch());

        purple.SetActive(true);
        clue.SetActive(true);

        //solvedPuzzle1 = true;
    }

    


}
