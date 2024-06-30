using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSequence : MonoBehaviour
{
    // Attached to LevelProg object in every Level scene.

    StoryManager storyManager;
    int dialogueSequenceInt;

    [Header("Level bools")]
    [SerializeField] bool Level1;
    [SerializeField] bool Level2;
    [SerializeField] bool Level3;
    [SerializeField] bool Level4;
    [SerializeField] bool Level5;

    [Header("FTUE prompts")]
    [SerializeField] GameObject[] FtuePrompts;

    [Header("TextMeshPro dialogue objects")]
    [SerializeField] TMP_Text DialogueGabSing;
    [SerializeField] TMP_Text DialogueOpp;
    [SerializeField] TMP_Text DialogueGabMult1;
    [SerializeField] TMP_Text DialogueGabMult2;
    [SerializeField] TMP_Text DialogueGabMult3;

    [Header("Gab and Opp dialogue lists")]
    [SerializeField] string[] GabDialogueList;
    [SerializeField] string[] OppDialogueList;
    [SerializeField] string[] GabMultDialogueList;

    [Header("Sequence ints")]
    [SerializeField] int[] GabMultDialogueInt;
    [SerializeField] public int LevelEndInt;

    private void Start()
    {
        storyManager = FindObjectOfType<StoryManager>();
        DialogueGabSing.text = GabDialogueList[0];
    }

    private void Update()
    {
        for (dialogueSequenceInt = 0; dialogueSequenceInt < storyManager.DialogueSequenceUnlocked.Length - 1; dialogueSequenceInt++)
        {
            if (storyManager.DialogueSequenceUnlocked[dialogueSequenceInt])
            {
                storyManager.DialogueSequenceUnlocked[dialogueSequenceInt] = false;

                // Level 1 multiple Gab dialogue functions
                if (Level1)
                {
                    if (dialogueSequenceInt == GabMultDialogueInt[0])
                    {
                        storyManager.b_SwitchOffGabDialogue = true;
                        SwitchOnGabMultiple();
                        DialogueGabMult1.text = GabMultDialogueList[0];
                        DialogueGabMult2.text = GabMultDialogueList[1];
                        DialogueGabMult3.text = GabMultDialogueList[2];
                    }
                    if (dialogueSequenceInt == GabMultDialogueInt[0] + 1)
                    {
                        SwitchOffGabMultiple();
                        StartCoroutine(SwitchOnPong());
                    }
                }
                if (dialogueSequenceInt == LevelEndInt)
                {
                    EndLevel();
                }

                // Checks for an even dialogueSequenceInt
                if (dialogueSequenceInt==0 || dialogueSequenceInt%2==0)
                {
                    // Opp dialogue plays
                    // Switch off Gab's dialogue and switch on Opp's dialogue
                    storyManager.b_SwitchOffGabDialogue = true;
                    storyManager.b_SwitchOnOppDialogue = true;
                    DialogueOpp.text = OppDialogueList[dialogueSequenceInt/2];
                }

                if (Level1)
                {
                    // Checks for an odd dialogueSequenceInt
                    if (dialogueSequenceInt % 2 == 1 && dialogueSequenceInt != GabMultDialogueInt[0])
                    {
                        // Gab dialogue plays
                        // Switch off Opp's dialogue and switch on Gab's dialogue
                        storyManager.b_SwitchOffOppDialogue = true;
                        storyManager.b_SwitchOnGabDialogue = true;
                        DialogueGabSing.text = GabDialogueList[(dialogueSequenceInt + 1) / 2];
                    }
                }
            }
        }
    }

    void SwitchOnGabMultiple()
    {
        storyManager.b_SwitchOffGabDialogue = true;
        storyManager.b_SwitchOffOppDialogue = true;
        storyManager.b_SwitchOnGabMultDialogue = true;
    }

    void SwitchOffGabMultiple()
    {
        storyManager.b_SwitchOffGabMultDialogue = true;
        storyManager.b_SwitchOnOppDialogue = true;
    }

    IEnumerator SwitchOnPong()
    {
        FindObjectOfType<GameManager>().GameMode = 0;
        yield return new WaitForSeconds(0.01f);
        FindObjectOfType<Ball>().Reset();
    }

    void EndLevel()
    {
        FindObjectOfType<GameManager>().EndLevel();
        FindObjectOfType<GameManager>().LevelOver = true;
    }
}
