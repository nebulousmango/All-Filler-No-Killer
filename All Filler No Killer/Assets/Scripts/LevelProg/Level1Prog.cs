using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Prog : MonoBehaviour
{
    StoryManager storyManager;
    [SerializeField] GameObject FtuePrompt1;
    [SerializeField] GameObject FtuePrompt2;
    [SerializeField] GameObject FtuePrompt3;

    private void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
        StartCoroutine(FirstFtuePrompt());
    }

    private void Update()
    {
        for (int i = 0; i < storyManager.DialogueSequenceUnlocked.Length - 1; i++)
        {
            if (storyManager.DialogueSequenceUnlocked[i])
            {
                storyManager.DialogueSequenceUnlocked[i] = false;
                if(i==4)
                {
                    FtuePrompt2.SetActive(true);
                }

                if (i == 5)
                {
                    FtuePrompt2.SetActive(true);
                }

                if (i==0 || i%2==0)
                {
                    // Opp dialogue plays
                    // Switch off Gab's dialogue and switch on Opp's dialogue
                    storyManager.b_SwitchOffGabDialogue = true;
                    storyManager.b_SwitchOnOppDialogue = true;
                }
                if(i%2==1)
                {
                    // Gab dialogue plays
                    // Switch off Opp's dialogue and switch on Gab's dialogue
                    storyManager.b_SwitchOffOppDialogue = true;
                    storyManager.b_SwitchOnGabDialogue = true;
                }
                if(i==5)
                {
                    storyManager.b_SwitchOffOppDialogue = true;
                    storyManager.b_SwitchOnGabMultDialogue = true;
                }
                if(i==6)
                {
                    storyManager.b_SwitchOffGabMultDialogue = true;
                    storyManager.b_SwitchOnOppDialogue = true;
                }
            }
        }
    }

    IEnumerator FirstFtuePrompt()
    {
        yield return new WaitForSeconds(6);
        FtuePrompt1.SetActive(true);
    }
}
