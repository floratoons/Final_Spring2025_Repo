using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShift : MonoBehaviour
{
    // Start is called before the first frame update

    public static CameraShift Instance;

    public Camera mainCam;
    public GameObject fadeTransition;
    public Camera birdsEyeCam;

    private int corCount = 0;

    //private bool played = false;

    GameManager GMScript;

    private void Start()
    {
        GMScript = GameObject.FindWithTag("GM").GetComponent<GameManager>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            StartCoroutine(CameraSwitch());
        }*/
    }

    /*public void AnimEvent(string s)
    {
        Debug.Log("AnimEvent: " + s + " called at: " + Time.time);
        played = true;
    }*/

    public IEnumerator CameraSwitch()
    {
        if (birdsEyeCam != null)
        {
            corCount++;
            Debug.Log($"CameraSwitch function called " + corCount + " times");

            fadeTransition.SetActive(true);
            yield return new WaitForSeconds(1f);
            mainCam.enabled = false;
            fadeTransition.SetActive(false);
            birdsEyeCam.enabled = true;

            yield return new WaitForSeconds(4f);

            fadeTransition.SetActive(true);

            yield return new WaitForSeconds(1f);
            birdsEyeCam.enabled = false;
            fadeTransition.SetActive(false);
            mainCam.enabled = true;
        }
        
        yield break;
    }
}
