using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    MusicManager MMScript;
    ButtonManager BMScript;

    private GameObject menu;

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
        menu = GameObject.Find("Menu");

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MMScript.MusicTadpole();
        }
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            MMScript.MusicAbstract();
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            MMScript.MusicWater();
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            menu.SetActive(true);
        }
        if (menu == true && Input.GetKey(KeyCode.Escape))
        {
            menu.SetActive(false);
        }
    }

}
