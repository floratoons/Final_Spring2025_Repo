using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    
    public List<GameObject> gemList = new List<GameObject>();

    public List<GameObject> placedList = new List<GameObject>();
    public bool hasBlue = false;
    public bool hasRed = false;
    private bool solved = false;

    // index to track currently active gem
    private int currentGemIndex = -1; // start with no gem

    private float scrollInput;
    //private int scrollCountTest;

    ItemPlace IPlaceScript;
    ItemPickup IPickupScript;
    GameManager GMScript;
    PuzzleManager PMScript;

    CameraShift CSScript;

    private void Start()
    {
        IPlaceScript = GameObject.FindWithTag("Ped").GetComponent<ItemPlace>();
        IPickupScript = GameObject.FindWithTag("Gem").GetComponent<ItemPickup>();
        GMScript = GameObject.FindWithTag("GM").GetComponent<GameManager>();
        CSScript = GameObject.Find("Fade").GetComponent<CameraShift>();
        PMScript = GameObject.Find("Puzzle Manager").GetComponent<PuzzleManager>();

    }

    void Update()
    {
        scrollInput = Input.GetAxis("Scroll");
        if (gemList.Count >= 1)
        {
            if (scrollInput > 0/*gemList.Count > 0*/)
            {
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
    }

    public void AddGem(GameObject gemPrefab, List<GameObject> list)
    {
        //add the instantiated gem to the list
        gemList.Add(gemPrefab);
        gemPrefab.SetActive(false); // start with the gem disabled

        // immediately activate the first gem that's picked up
        if (list == gemList)
        {
            if (list.Count == 1)
            {
                // switch gem function
                SwitchGem(0);
            }
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

    public void moveToPlaceSpot(Transform placeSpot, Vector3 scale)
    {
        // get current gem in index
        gemList[currentGemIndex].transform.localScale = gemList[currentGemIndex].transform.localScale;
        // move current gem to place slot
        // set the gem's size to its proper size
        gemList[currentGemIndex].transform.SetParent(placeSpot, worldPositionStays: false);
        gemList[currentGemIndex].transform.localScale = scale;

        // audio cue

        // ** camera stuff (animation)
        StartCoroutine(CSScript.CameraSwitch());


        // ** flowers changing

        // ** remove gem from the list?
        placedList.Add(gemList[currentGemIndex]);
        gemList.Remove(gemList[currentGemIndex]);

        if (placedList.Count >= 1)
        {
            if (placedList[0].name.Contains("Hold_GemBlue") || placedList[1].name.Contains("Hold_GemBlue"))
            {
                hasBlue = true;
                Debug.Log("Blue placed");
            }
            if (placedList[0].name.Contains("Hold_GemRed") || placedList[1].name.Contains("Hold_GemRed"))
            {
                hasRed = true;
                Debug.Log("Red placed");
            }
            if (hasRed && hasBlue)
            {
                Debug.Log("Placed blue & red!");
            }
        }

        if (IPlaceScript.pedestalNumber == 1 && IPlaceScript.itemPlaced)
        {
            Debug.Log("Placed gem in spot 1 (taken)");
            AddGem(IPlaceScript.placedGem, gemList);
            placedList.Remove(gemList[0]);
        }
        if (IPlaceScript.pedestalNumber == 2 && IPlaceScript.itemPlaced)
        {
            Debug.Log("Placed gem in spot 2 (taken)");
            AddGem(IPlaceScript.placedGem, gemList);
            placedList.Remove(gemList[1]);
        }
    }


}
