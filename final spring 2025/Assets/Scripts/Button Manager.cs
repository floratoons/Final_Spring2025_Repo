using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public void Scenes()
    {
        SceneManager.LoadScene(1);
    }
    public void Level1()
    {
        SceneManager.LoadScene(2);
    }

}