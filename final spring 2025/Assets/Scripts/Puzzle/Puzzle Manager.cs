using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // puzzle game


    CameraShift cameraScript;
    ItemManager IMScript;

    public GameObject red;
    public GameObject blue;
    public GameObject purple;

    public GameObject clue;
    public bool ifSolved = false;


    public void trySolve()
    {
        ifSolved = true;
    }


    void Start()
    {
        cameraScript = GameObject.Find("Fade").GetComponent<CameraShift>();
        IMScript = GameObject.Find("Player").GetComponent<ItemManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IMScript.hasBlue || IMScript.hasRed)
        {
            if (IMScript.hasBlue)
            {
                blue.SetActive(true);
            }
            if (IMScript.hasRed)
            {
                red.SetActive(true);
            }
            if (IMScript.hasBlue && IMScript.hasRed)
            {
                StartCoroutine(cameraScript.CameraSwitch());

                ifSolved = true;

                // stop playing the camera shift thingy.
                //cameraScript.birdsEyeCam.enabled = false;
                //GameObject.Destroy(cameraScript.birdsEyeCam);

                purple.SetActive(true);
                clue.SetActive(true);
            }
        }
    }
}
