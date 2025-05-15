using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public bool inArea;

    public int pedestalNumber;
    Vector3 scale1 = new Vector3((float)0.5, (float)2.5, (float)0.75);
    Vector3 scale2 = new Vector3(3, 12, (float)3.5);
    public GameObject placedon1;
    public GameObject placedon2;

    public GameObject glow1;
    public GameObject glow2;

    public Transform placeSpot;
    //public int placedCount = 0;
    public bool itemPlaced = false;

    ItemManager ItemManagerScript;
    CharacterPlayer PlayerControllerScript;
    CameraShift CSScript;

    public GameObject placedGem;

    private void Start()
    {
        ItemManagerScript = GameObject.Find("Player").GetComponent<ItemManager>();
        PlayerControllerScript = GameObject.Find("Player").GetComponent<CharacterPlayer>();
        CSScript = GameObject.Find("Fade").GetComponent<CameraShift>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
            PlayerControllerScript.inAreaShine.Play();
            // visual cue
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // when in the area
        if (pedestalNumber == 1)
        {
            PlaceGem(glow1, scale1);
        }
        else if (pedestalNumber == 2)
        {
            PlaceGem(glow2, scale2);
        }
        // moves the gem to its spot on the pedestal once it's placed
        if (itemPlaced)
        {
            if (other.CompareTag("Gem"))
            {
                // get the place spot from the certain pedestal's trigger?
                placeSpot = gameObject.GetComponentInChildren<Transform>();
                // grab the gem that is in the pedestal's trigger
                placedGem = other.gameObject;
                //Debug.Log("Gem placed in area, " + (placedGem));
                //FindWithTag("Gem");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControllerScript.inAreaShine.Stop();
            inArea = false;
            //turn off visual
            glow1.SetActive(false);
            glow2.SetActive(false);
        }
    }

    public void PlaceGem(GameObject glow, Vector3 scale)
    {
        if (this.gameObject.activeInHierarchy && inArea)
        {
            if (placedGem = null)
            {
                glow.SetActive(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                PlayerControllerScript.placeClink.Play();

                StartCoroutine(CSScript.CameraSwitch());
                glow.SetActive(false);
                //placedCount++;
                itemPlaced = true;
                //place the object on a spot
                ItemManagerScript.moveToPlaceSpot(placeSpot, scale);
            }
        }
    }


}

