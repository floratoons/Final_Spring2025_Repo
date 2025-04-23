using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public bool inArea;
    public Vector3 placeSpot;

    ItemManager ItemManagerScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
            // visual cue
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
