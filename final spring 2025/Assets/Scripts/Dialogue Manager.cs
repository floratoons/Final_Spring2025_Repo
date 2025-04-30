using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // reference to our scriptable object
    public DialogueLine currentLine;
    // container for dialogue lines
    public Transform dialogueParent;
    public GameObject dialoguePrefab;
    // prefab for button choices
    public GameObject choiceButtonPrefab;
    // container for our button choices
    public Transform choiceParent;
    public Button continueButton; 

    // npc portraits & movement?

    public void StartingDialogue()
    {
        UpdateDialogue(currentLine);
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
        // SetActive(false);
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
            // if there are no dialogue choices, show the continue button
            continueButton.gameObject.GetComponent<Button>().interactable = true;
            continueButton.gameObject.GetComponent<Image>().color = Color.white;
            // clear everything
            // using the same button for different lines
            // so we don't want the previous dialogue lines to stack over each other
            continueButton.onClick.RemoveAllListeners();
                // when the button is clicked, run the code and it'll continue to the next line
                continueButton.onClick.AddListener(() =>
                {
                    UpdateDialogue(line.nextLine);
                    continueButton.gameObject.GetComponent<Button>().interactable = false;
                    continueButton.gameObject.GetComponent<Image>().color = Color.grey;
                });
        }

    }

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
