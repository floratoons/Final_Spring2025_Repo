using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    // Start is called before the first frame update

    public Camera mainCam;
    public GameObject fade;
    public Camera birdsEyeCam;

    GameManager GMScript;

    private void Start()
    {

        //GMScript = GameObject.Find("GM").GetComponent<>
    }

    IEnumerator cameraSwitch(Camera cam)
    {
        /*fade.SetActive(true);
        yield return new WaitUntil()
        mainCam.enabled = false;
        cam.enabled = true;*/
        fade.SetActive(false);

        yield return new WaitForSeconds(4f);

        fade.SetActive(true);
        cam.enabled = false;
        mainCam.enabled = true;
    }
}
