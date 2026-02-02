using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Ink.Runtime;

public class TextManager : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.04f;
    [SerializeField] private GameObject continueIcon;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    private bool canContinueToNextLine;

    private IEnumerator DisplayLine(Story story) 
    {
        string line = story.Continue();
        // set the text to the full line, but set the visible characters to 0
        dialogueText.text = line;
        dialogueText.maxVisibleCharacters = 0;
        // hide items while text is typing
        continueIcon.SetActive(false);
        HideChoices();

        canContinueToNextLine = false;

        bool isAddingRichTextTag = false;
        char[] letters = line.ToCharArray();
        for(int i = 0; i < letters.Length; i++)
        { 
            //TODO Torture yourself with regular expressions for zero fucking gain, because you're an idiot obsessed with "clean code"
            if(/*regex goes here*/)
            else 
            {
                dialogueText.maxVisibleCharacters++;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
        // display each letter one at a time
        foreach (char letter in line.ToCharArray())
        {
            // TODO Click to auto complete

            // check for rich text tag, if found, add it without waiting
            if (letter == '<' || isAddingRichTextTag) 
            {
                isAddingRichTextTag = true;
                if (letter == '>')
                {
                    isAddingRichTextTag = false;
                }
            }
            // if not rich text, add the next letter and wait a small time
            
        }

        // actions to take after the entire line has finished displaying
        continueIcon.SetActive(true);
        DisplayChoices(story.currentChoices);

        canContinueToNextLine = true;
    }

    private void DisplayChoices(List<Choice> currentChoices) 
    {

        // defensive check to make sure our UI can support the number of choices coming in
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                + currentChoices.Count);
        }

        // enable and initialize the choices up to the amount of choices for this line of dialogue
        for(int i = 0; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(i < currentChoices.Count);
            choicesText[i].text = i < currentChoices.Count? currentChoices[i].text : "";
        }

        //TODO Create auto select first choice
        //StartCoroutine(SelectFirstChoice());
    }

    private void HideChoices() 
    {
        foreach (GameObject choiceButton in choices) 
        {
            choiceButton.SetActive(false);
        }
    }
}
