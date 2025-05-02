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

    public Canvas menu;
    private SpriteRenderer notebook;
    public Canvas dialoguecanvas;

    // chapters
    // keep track of what the last "currentline" from dialogue manager 



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
        STScript = GameObject.Find("Camera").GetComponent<SceneTransitioner>();
        DMScript = GameObject.Find("Dialogue Manager").GetComponent < DialogueManager>();

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MMScript.MusicTadpole();

        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            MMScript.MusicAbstract();

            // find the menu & notebook 
            menu = GameObject.FindWithTag("Menu").GetComponent<Canvas>();
            dialoguecanvas = GameObject.Find("DialogueBox").GetComponent<Canvas>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            MMScript.MusicWater();

            // find the menu
            menu = GameObject.FindWithTag("Menu").GetComponent<Canvas>();
            notebook = GameObject.Find("Notebook").GetComponent<SpriteRenderer>();
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            menu.enabled = true;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                dialoguecanvas.enabled = false;
            }
        }
        if (menu == true && Input.GetKey(KeyCode.Escape))
        {
            menu.enabled = false;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                dialoguecanvas.enabled = true;
            }
        }
    }

}
