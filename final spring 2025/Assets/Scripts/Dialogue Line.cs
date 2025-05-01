using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "DialogueLine/Object")]
// "scriptable object" is a data container that allows you to store large amounts
// of data (independently from a script)
public class DialogueLine : ScriptableObject
{
    public string speakerName;
    // keeps track of which scene section for which character portraits are on
    public int sceneSectionNum;

    [TextArea] public List<string> dialogueLinesList = new List<string>();
    // list of text to show
    // next line if there are no choices
    public DialogueLine nextLine;
    // choices
    public DialogueChoice[] choices;

    DialogueManager DialogueManagerScript;

    private void OnEnable()
    {
        //Debug.Log(${GameObject});
        // checking the scene section for which character portraits are on
        /*DialogueManagerScript = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();

        // if the scene section changes, update (characterportraitupdate)
        if (sceneSectionNum != DialogueManagerScript.currentSceneSectionNum)
        {
            DialogueManagerScript.CharacterPortraitUpdate(sceneSectionNum);
        }*/
    }

}

[System.Serializable]
public class DialogueChoice
{
    public string choiceText; // what the choice's button says
    public DialogueLine nextLine; // what happens/ is written out if you pick it

    public string requiredStat;
    public int requiredValue;

    public string rewardStat;
    public int rewardValue;

}