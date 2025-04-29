using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour
{
    [SerializeField]
    Vector3[] sceneLocations;

    public Button homeButton, wharfButton, workButton;

    public float speed = 1.0f;
    public Camera cam;
    public Canvas menuCanvas;

    GameManager GMScript;

    private void Start()
    {
        cam = gameObject.GetComponent<Camera>();
        GMScript = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {
        if (menuCanvas)
        {
            sceneTrans(sceneLocations[1], homeButton);
            sceneTrans(sceneLocations[2], wharfButton);
            sceneTrans(sceneLocations[3], workButton);
        }
    }

    public void sceneTrans(Vector3 scene, Button button)
    {
        if (button != null)
        {
            button.onClick.AddListener(() =>
            {
                Debug.Log($"Clicked to {button}, at {scene}");
                cam.transform.position = (scene);
                GMScript.menu.enabled = false;
                //add smoothness to transition
            });
        }
    }
}
