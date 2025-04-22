using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    
    [SerializeField]
    Vector3[] sceneLocations;
    public Button locationButton0, locationButton1;

    public GameObject cam;
    public Button menuButton;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GM");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }


    public void Menu()
    {
        SceneManager.LoadScene(1);
    }

    public void Level1()
    {
        SceneManager.LoadScene(2);
    }

    /*public void Scenes()
    {
        if (locationButton[0])
        {
            Debug.Log($"Clicked to ({gameObject.tag})");
            cam.transform.position = sceneLocations[0];
        }
        else if (locationButton[1])
        {
            Debug.Log($"Clicked to ({gameObject.tag})");
            cam.transform.position = sceneLocations[1];
        }
        Timer();
        SceneManager.LoadScene(0);
    }*/


    // Update is called once per frame
    void Update()
    {
        cam = GameObject.Find("Main Camera");

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("1_Menu"))
        {
            locationButton0 = GameObject.Find("Work Button").GetComponent<Button>();
            locationButton1 = GameObject.Find("Town Button").GetComponent<Button>();

            // map navigation buttons with cam transform positions
            if (locationButton0 != null)
            {
                locationButton0.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene(0);
                    Debug.Log($"Clicked to ({sceneLocations[0]})");
                    cam.transform.position = sceneLocations[0];
                });
            }
            if (locationButton1 != null)
            {
                locationButton1.onClick.AddListener(() =>
                {
                    SceneManager.LoadScene(0);
                    Debug.Log($"Clicked to ({sceneLocations[1]})");
                    cam.transform.position = sceneLocations[1];
                });
            }
        }
        
        // from scenes to menu button
        if (menuButton != null)
        {
            menuButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(1);
            });
        }
    }

    /*private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1);
    }*/

}