using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    MusicManager MMScript;
    ButtonManager BMScript;

    private Canvas menu;
    private SpriteRenderer notebook;
    private Canvas dialoguecanvas;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GM");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MMScript.MusicTadpole();

        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            MMScript.MusicAbstract();

            menu = GameObject.FindWithTag("Menu").GetComponent<Canvas>();
            notebook = GameObject.Find("Notebook").GetComponent<SpriteRenderer>();
            dialoguecanvas = GameObject.Find("DialogueBox").GetComponent<Canvas>();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            MMScript.MusicWater();

            menu = GameObject.FindWithTag("Menu").GetComponent<Canvas>();
            notebook = GameObject.Find("Notebook").GetComponent<SpriteRenderer>();
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            menu.enabled = true;
            notebook.enabled = true;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                dialoguecanvas.enabled = false;
            }
        }
        if (menu == true && Input.GetKey(KeyCode.Escape))
        {
            menu.enabled = false;
            notebook.enabled = false;
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                dialoguecanvas.enabled = true;
            }
        }
    }

}
