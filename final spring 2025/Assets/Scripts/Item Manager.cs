using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    
    public List<GameObject> gemList = new List<GameObject>();

    // index to track currently active gem
    private int currentGemIndex = -1; // start with no gem

    private float scrollInput;
    //private int scrollCountTest;

    Vector3 DefaultScale;

    ItemPlace ItemPlaceScript;
    ItemPickup ItemPickupScript;

    private void Start()
    {
        ItemPlaceScript = GameObject.FindWithTag("Ped").GetComponent<ItemPlace>();
        ItemPickupScript = GameObject.FindWithTag("Gem").GetComponent<ItemPickup>();

        DefaultScale = new Vector3((float)0.5, (float)2.5, (float)0.75);
    }

    void Update()
    {
        scrollInput = Input.GetAxis("Scroll");
        
        if (scrollInput > 0/*gemList.Count > 0*/)
        {
            //scrollCountTest += 1;
            //Debug.Log("scrolled up, scrollCount= " + scrollCountTest);
            // moves down the gem index list and then wraps back around to the beginning
            int nextGemIndex = (currentGemIndex + 1) % gemList.Count;
            SwitchGem(nextGemIndex);
            /*if (currentGemIndex > 6)
            {
                currentGemIndex = 0;
            }*/
            Debug.Log("Gem= " + currentGemIndex);
        }
        else if (scrollInput < 0)
        {
            //scrollCountTest -= 1;
            //Debug.Log("scrolled down, scrollCount= " + scrollCountTest);
            /*int nextGemIndex = (currentGemIndex - 1) % gemList.Count;
            SwitchGem(nextGemIndex);
            if (currentGemIndex < 0)
            {
                currentGemIndex = 6;
            }*/
            scrollInput = 0;
            
            Debug.Log("Gem= " + currentGemIndex);
        }
    }

    public void AddGem(GameObject gemPrefab)
    {
        //add the instantiated gem to the list
        gemList.Add(gemPrefab);
        gemPrefab.SetActive(false); // start with the gem disabled

        // immediately activate the first gem that's picked up
        if (gemList.Count == 1)
        {
            // switch gem function
            SwitchGem(0);
        }
    }

    void SwitchGem(int index)
    {
        // deactivate the currently active gem
        if (currentGemIndex != -1)
        {
            gemList[currentGemIndex].SetActive(false);
        }

        // assign the current gem index, and make the current gem in index true?
        currentGemIndex = index;
        gemList[currentGemIndex].SetActive(true);

        Debug.Log("Switched gem");
    }

    public void placeInSpot()
    {
        Debug.Log("Placed a gem");
        // get current gem in index
        Transform placeSlot = ItemPlaceScript.placeSpot;
        gemList[currentGemIndex].transform.localScale = gemList[currentGemIndex].transform.localScale;
        // move current gem to place slot
        // set the gem's size to its proper size
        gemList[currentGemIndex].transform.SetParent(placeSlot, worldPositionStays: false);
        gemList[currentGemIndex].transform.localScale = DefaultScale;

        // audio cue

        // ** camera stuff (animation)
        // ** flowers changing

        // ** remove gem from the list?
        /*other.GetComponent<ItemManager>().RemoveGem(gemList[currentGemIndex]);
        Destroy(gameObject);*/

        //if (correct)
        //{
        //    **register correct placement, lock/ turn off the place area
        //      visual cue on the placed gem to show that it's locked
        //}
        //else if (incorrect)
        //{
        //      audio cue that it's wrong
        //}


    }

}
