using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionalChoices : MonoBehaviour
{
    DialogueManager dialogueManager;
    DialogueLine dialogueLine;
    public TextMeshProUGUI dialogueText;

    public void SetUp(DialogueManager _dialogueManager, DialogueLine _dialogueLine, string _dialogueText)
    {
        dialogueManager = _dialogueManager;
        dialogueLine = _dialogueLine;
        dialogueText.text = _dialogueText;
    }
    
    public void SelectOption()
    {
        dialogueManager.UpdateDialogue(dialogueLine);
    }

}
