using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public bool inArea;
    public Transform placeSpot;

    ItemManager ItemManagerScript;

    public GameObject placedGem;

    private void Start()
    {
        placeSpot = gameObject.GetComponentInChildren<Transform>();
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
        if (other.CompareTag("Gem"))
        {
            placedGem = other.gameObject;
            Debug.Log("Gem placed in area, " + (placedGem));
            //FindWithTag("Gem");
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
        ItemManagerScript = GameObject.Find("Player").GetComponent<ItemManager>();

        {
            if (this.gameObject.activeInHierarchy && Input.GetMouseButtonDown(0) && inArea)
            {
                //place the object on a spot
                ItemManagerScript.placeInSpot();

            }
        }
    }
}
