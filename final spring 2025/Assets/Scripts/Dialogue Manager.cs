using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // reference to our scriptable object
    public DialogueLine currentLine;

    // scene (reference to characters active in "scene" section)
    public int currentSceneSectionNum = 0;
    // all parents of character portraits
    public List<GameObject> sceneMemberHolders = new List<GameObject>();

    // container for dialogue lines
    public Transform dialogueParent;
    public GameObject dialoguePrefab;
    // prefab for button choices
    public GameObject choiceButtonPrefab;
    // container for our button choices
    public Transform choiceParent;
    public GameObject continueButton;

    public void StartingDialogue()
    {
        UpdateDialogue(currentLine);
    }

    public void CharacterPortraitUpdate(int newScene)
    {
        // depending on what the new scene is, update the images to be enabled or disabled

        // cycle through the sceneMemberHolders list for all the scene section parents
        // simplified version LETSGOOOOOOOOOO
        
        for (int i = 0; i < sceneMemberHolders.Count; i++)
        {
            // for the 1 active scenememberholder
            if (i == newScene)
            {
                sceneMemberHolders[i].SetActive(true);
                //Debug.Log($"Switched to scene section {newScene}.");
                // animations?
            }
            // for the inactive scenememberholders
            else if (i != newScene)
            {
                sceneMemberHolders[i].SetActive(false);
            }
        }
        currentSceneSectionNum = newScene;

        /*if (newScene == 0)
        {
            Debug.Log($"Switched to scene section {newScene}.");

            sceneMemberHolders[0].SetActive(true);
            sceneMemberHolders[1].SetActive(false);
            sceneMemberHolders[2].SetActive(false);
            sceneMemberHolders[3].SetActive(false);
            currentSceneSectionNum = 0;
        }
        else if (newScene == 1)
        {
            Debug.Log($"Switched to scene section {newScene}.");

            sceneMemberHolders[0].SetActive(false);
            sceneMemberHolders[1].SetActive(true);
            sceneMemberHolders[2].SetActive(false);
            sceneMemberHolders[3].SetActive(false);
            currentSceneSectionNum = 1;
        }
        else if (newScene == 2)
        {
            sceneMemberHolders[0].SetActive(false);
            sceneMemberHolders[1].SetActive(false);
            sceneMemberHolders[2].SetActive(true);
            sceneMemberHolders[3].SetActive(false);
            currentSceneSectionNum = 2;
        }
        else if (newScene == 3)
        {
            sceneMemberHolders[0].SetActive(false);
            sceneMemberHolders[1].SetActive(false);
            sceneMemberHolders[2].SetActive(false);
            sceneMemberHolders[3].SetActive(true);
            currentSceneSectionNum = 3;
        }*/
    }

    public void UpdateDialogue(DialogueLine dialogueLine)
    {
        currentLine = dialogueLine;
        StartCoroutine(DisplayDialogue(currentLine));
    }


    IEnumerator DisplayDialogue(DialogueLine line)
    {
        //if (line == null) return; // if there's no dialogue set up, exit the function

        foreach (string _dialogueLine in currentLine.dialogueLinesList)
        {

            // make a new copy of a button
            GameObject textBubble = Instantiate(dialoguePrefab, dialogueParent);
            CharacterPortraitUpdate(currentLine.sceneSectionNum);
            TextMeshProUGUI grabText = textBubble.GetComponent<TextMeshProUGUI>();
            // set the text to whatever string we're currently looping
            grabText.text = _dialogueLine;

            if (!string.IsNullOrEmpty(line.speakerName))
            {
                grabText.text = $"<b>{line.speakerName}: </b>{_dialogueLine}";
            }
            yield return new WaitForSeconds(1f);
        }

        // ensure continue button is below all of the instantiated text bubbles and buttons
        // continueButton.transform.SetAsLastSibling();
        // clear all the old choice buttons
        foreach (Transform _child in choiceParent) Destroy(_child.gameObject);
        // hide the continue button on default
        continueButton.gameObject.GetComponent<Button>().interactable = false;
        continueButton.gameObject.GetComponent<Image>().color = Color.grey;
        // button choices appear after the latest chat line
        choiceParent.transform.SetAsLastSibling();

        if (line.choices != null && line.choices.Length > 0)
        {
            foreach (DialogueChoice choice in line.choices)
            {
                // create a button
                GameObject newButtonChoice = Instantiate(choiceButtonPrefab, choiceParent);
                TextMeshProUGUI buttonText = newButtonChoice.GetComponentInChildren<TextMeshProUGUI>();

                bool meetsRequirement = true;

                // if there's a required stat placed in the choice (not empty)
                if (!string.IsNullOrEmpty(choice.requiredStat))
                {
                    // checks the player stats and returns the current value in the var
                    int playerStat = PlayerStats.Instance.GetStat(choice.requiredStat);
                    meetsRequirement = playerStat >= choice.requiredValue;
                }

                // update the buttontext
                buttonText.text = choice.choiceText;
                if (!meetsRequirement)
                {
                    buttonText.text += $"<color=red> Needs ({choice.requiredValue}) {choice.requiredStat}.";
                    //buttonText.text += "<color=red>" + choice.requiredStat + ": " + choice.requiredValue + "</color>";
                }

                Button buttonComp = newButtonChoice.GetComponent<Button>();
                buttonComp.onClick.AddListener(() =>
                {
                    if (!string.IsNullOrEmpty(choice.rewardStat))
                    {
                        PlayerStats.Instance.IncreaseStat(choice.rewardStat, choice.rewardValue);
                    }
                });

                buttonComp.interactable = meetsRequirement;

                if (meetsRequirement)
                {
                    newButtonChoice.GetComponent<OptionalChoices>().SetUp(this, choice.nextLine, choice.choiceText);
                }

            }
        }
        else if (line.nextLine != null)
        {
            Debug.Log("line.nextLine != null");
            // if there are no dialogue choices & just a next line, allow the continue button
            continueButton.gameObject.GetComponent<Button>().interactable = true;
            continueButton.gameObject.GetComponent<Image>().color = Color.white;

            continueButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                UpdateDialogue(line.nextLine);
                Debug.Log("Next line");
            });

            /*if (Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Space button clicked for next line");
                UpdateDialogue(line.nextLine);
                continueButton.gameObject.GetComponent<Image>().color = Color.grey;
            }*/
        }
    }

    /*public void continueSpace()
    {
        Debug.Log("Next line");
        UpdateDialogue(line.nextLine);
        continueButton.gameObject.GetComponent<Image>().color = Color.grey;
    }*/

        /*int GetPlayerStatValue(string StatName)
        {
            switch (StatName)
            {
                case "charisma": return PlayerStats.Instance.charisma;
                case "logic": return PlayerStats.Instance.logic;
                    default: return 0;
            }
        }*/
}
