using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public bool inArea;

    public int pedestalNumber;
    public Transform placeSpot1;
    public Transform placeSpot2;

    public Transform staticPlaceSpot;
    public bool itemPlaced = false;

    ItemManager ItemManagerScript;

    public GameObject placedGem;

    private void Start()
    {
        ItemManagerScript = GameObject.Find("Player").GetComponent<ItemManager>();

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
        Debug.Log("Gem in area");
        
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
            if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && inArea)
            {
                itemPlaced = true;
                //place the object on a spot
                ItemManagerScript.placeInSpot(staticPlaceSpot);

            }
        }
    }
}
