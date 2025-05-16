using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClueClick : MonoBehaviour
{
    public bool inClueArea = false;
    public GameObject winCanvas;

    public GameObject sceneButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inClueArea = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetMouseButtonDown(0))
        {
            winCanvas.SetActive (true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (sceneButton != null)
            {
                if (SceneManager.GetActiveScene().buildIndex == 2)
                {
                    SceneManager.LoadScene(1);
                }
                else if(SceneManager.GetActiveScene().buildIndex == 3)
                {
                    SceneManager.LoadScene(0);
                }
            }
        }
    }
}
