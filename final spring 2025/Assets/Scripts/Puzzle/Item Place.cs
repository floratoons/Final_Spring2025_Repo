using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public bool inArea;

    public int pedestalNumber;
    public Transform placeSpot1;
    public Transform placeSpot2;
    Vector3 scale1;
    Vector3 scale2;

    public Transform staticPlaceSpot;
    public bool itemPlaced = false;

    ItemManager ItemManagerScript;

    public GameObject placedGem;

    private void Start()
    {
        ItemManagerScript = GameObject.Find("Player").GetComponent<ItemManager>();

        scale1 = new Vector3((float)0.5, (float)2.5, (float)0.75);
        scale2 = new Vector3((float)3, (float)12, (float)3.5);

        if (pedestalNumber == 1)
        {
            staticPlaceSpot = placeSpot1;
        }
        else if (pedestalNumber == 2)
        {
            staticPlaceSpot = placeSpot2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
            // visual cue
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (itemPlaced)
        {
            if (other.CompareTag("Gem"))
            {
                // get the place spot from the certain pedestal's trigger?
                staticPlaceSpot = gameObject.GetComponentInChildren<Transform>();
                // grab the gem that is in the pedestal's trigger
                placedGem = other.gameObject;
                Debug.Log("Gem placed in area, " + (placedGem));
                //FindWithTag("Gem");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = false;
            //turn off visual
        }
    }

    void Update()
    {
        {
            if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && inArea && pedestalNumber == 1)
            {
                itemPlaced = true;
                //place the object on a spot
                ItemManagerScript.placeInSpot(staticPlaceSpot, scale1);
            }
            else if(this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && inArea && pedestalNumber == 2)
            {
                itemPlaced = true;
                //place the object on a spot
                ItemManagerScript.placeInSpot(staticPlaceSpot, scale2);
            }
        }
    }
}
