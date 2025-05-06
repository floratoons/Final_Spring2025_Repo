using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    
    public List<GameObject> gemList = new List<GameObject>();

    public List<GameObject> placedList = new List<GameObject>();
    private bool hasBlue = false;
    private bool hasRed = false;

    // index to track currently active gem
    private int currentGemIndex = -1; // start with no gem

    private float scrollInput;
    //private int scrollCountTest;

    ItemPlace ItemPlaceScript;
    ItemPickup ItemPickupScript;

    private void Start()
    {
        ItemPlaceScript = GameObject.FindWithTag("Ped").GetComponent<ItemPlace>();
        ItemPickupScript = GameObject.FindWithTag("Gem").GetComponent<ItemPickup>();

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

        // assign the current gem index, and make the current gem in index true
        currentGemIndex = index;
        gemList[currentGemIndex].SetActive(true);

        Debug.Log("Switched gem");
    }

    public void placeInSpot(Transform placeSpot, Vector3 scale)
    {
        // get current gem in index
        gemList[currentGemIndex].transform.localScale = gemList[currentGemIndex].transform.localScale;
        // move current gem to place slot
        // set the gem's size to its proper size
        gemList[currentGemIndex].transform.SetParent(placeSpot, worldPositionStays: false);
        gemList[currentGemIndex].transform.localScale = scale;

        // audio cue

        // ** camera stuff (animation)
        // ** flowers changing

        // ** remove gem from the list?

        gemList.Remove(ItemPlaceScript.placedGem);
        placedList.Add(ItemPlaceScript.placedGem);

        /*if (placedList.Count == 2)
        {
            for (int i = 0; i < placedList.Count; i++)
            {
                // for the 1 active scenememberholder
                if (placedList[0].name.Contains("Hold_GemBlue") || placedList[1].name.Contains("Hold_GemBlue"))
                {
                    hasBlue = true;
                    Debug.Log("Blue placed");
                    
                }
                // for the inactive scenememberholders
                if (placedList[0].name.Contains("Hold_GemRed") || placedList[1].name.Contains("Hold_GemRed"))
                {
                    hasRed = true;
                    Debug.Log("Red placed");
                }
            }
        }*/

        

        /*
        if (correct)
        {
            **register correct placement, lock/ turn off the place area
              visual cue on the placed gem to show that it's locked
        }
        else if (incorrect)
        {
            audio cue that it's wrong
        }
        */


    }

}
