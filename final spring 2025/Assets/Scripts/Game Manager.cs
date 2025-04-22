using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    MusicManager MMScript;

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            MMScript.MusicTadpole();
        }

    }


}
