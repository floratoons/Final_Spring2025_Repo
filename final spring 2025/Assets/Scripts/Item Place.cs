using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public bool inArea;
    public Transform placeSlot;

    ItemPickup ItemPickupScript;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inArea = true;
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
                //place the object on a spot
                placeInSpot();
            }
        }

        void placeInSpot()
        {
            // instantiate and parent directly to gem place slot
            GameObject newGem = Instantiate(ItemPickupScript.gemPrefab, placeSlot.position, Quaternion.identity, placeSlot);

            // resetting the position and rotation to make sure it fits in the "socket"
            newGem.transform.localPosition = Vector3.zero;
            newGem.transform.localRotation = Quaternion.identity;

            // ** remove gem from the list, and take it out of inventory?
            
            /*other.GetComponent<ItemManager>().AddGem(newGem);
            Destroy(gameObject);*/

        }
    }
}
