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
            Debug.Log("Gem= " + currentGemIndex);
        }
        else if (scrollInput < 0)
        {
            //scrollCountTest -= 1;
            //Debug.Log("scrolled down, scrollCount= " + scrollCountTest);
            int nextGemIndex = (currentGemIndex - 1) % gemList.Count;
            SwitchGem(nextGemIndex);
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

        Debug.Log("Switched Weapon");
    }
}
