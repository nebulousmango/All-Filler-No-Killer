using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSequence : MonoBehaviour
{
    // Attached to LevelProg object in every Level scene.

    StoryManager StoryManager;
    int DialogueSequenceInt;
    bool GabMultipleActive;
    int DownArrowInt;

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

    [Header("Gab multiple dialogue UI")]
    [SerializeField] GameObject GabMultSelection1;
    [SerializeField] GameObject GabMultSelection2;
    [SerializeField] GameObject GabMultSelection3;
    [SerializeField] float ReadingTimer;
    [SerializeField] GameObject TimerUI;
    [SerializeField] TMP_Text TimerText;

    private void Start()
    {
        StoryManager = FindObjectOfType<StoryManager>();
        DialogueGabSing.text = GabDialogueList[0];
    }

    private void Update()
    {
        if(GabMultipleActive == true)
        {
            if (ReadingTimer > 0)
            {
                ReadingTimer -= Time.deltaTime;
                TimerText.text = ReadingTimer.ToString("N0");
            }
            else
            {
                StoryManager.AdvanceDialogueSequence();
                GabMultSelection1.SetActive(false);
                GabMultSelection2.SetActive(false);
                GabMultSelection3.SetActive(false);
                SwitchOffGabMultiple();
                StartCoroutine(SwitchOnPong());
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && GabMultipleActive == true)
        {
            DownArrowInt++;
            if (DownArrowInt == 1)
            {
                GabMultSelection1.SetActive(true);
                GabMultSelection2.SetActive(false);
                GabMultSelection3.SetActive(false);
            }
            if (DownArrowInt == 2)
            {
                GabMultSelection1.SetActive(false);
                GabMultSelection2.SetActive(true);
                GabMultSelection3.SetActive(false);
            }
            if (DownArrowInt == 3)
            {
                GabMultSelection1.SetActive(false);
                GabMultSelection2.SetActive(false);
                GabMultSelection3.SetActive(true);
            }
            if (DownArrowInt > 3)
            {
                DownArrowInt = 1;
                GabMultSelection1.SetActive(true);
                GabMultSelection2.SetActive(false);
                GabMultSelection3.SetActive(false);
            }
        }

        for (DialogueSequenceInt = 0; DialogueSequenceInt < StoryManager.DialogueSequenceUnlocked.Length - 1; DialogueSequenceInt++)
        {
            if (StoryManager.DialogueSequenceUnlocked[DialogueSequenceInt])
            {
                StoryManager.DialogueSequenceUnlocked[DialogueSequenceInt] = false;

                // Level 1 multiple Gab dialogue functions
                if (Level1)
                {
                    if (DialogueSequenceInt == GabMultDialogueInt[0])
                    {
                        StoryManager.b_SwitchOffGabDialogue = true;
                        SwitchOnGabMultiple();
                        DialogueGabMult1.text = GabMultDialogueList[0];
                        DialogueGabMult2.text = GabMultDialogueList[1];
                        DialogueGabMult3.text = GabMultDialogueList[2];
                    }
                    if (DialogueSequenceInt == GabMultDialogueInt[0] + 1)
                    {
                        SwitchOffGabMultiple();
                        StartCoroutine(SwitchOnPong());
                    }
                }
                if (DialogueSequenceInt == LevelEndInt)
                {
                    EndLevel();
                }

                // Checks for an even dialogueSequenceInt
                if (DialogueSequenceInt==0 || DialogueSequenceInt%2==0)
                {
                    // Opp dialogue plays
                    // Switch off Gab's dialogue and switch on Opp's dialogue
                    StoryManager.b_SwitchOffGabDialogue = true;
                    StoryManager.b_SwitchOnOppDialogue = true;
                    DialogueOpp.text = OppDialogueList[DialogueSequenceInt/2];
                }

                if (Level1)
                {
                    // Checks for an odd dialogueSequenceInt
                    if (DialogueSequenceInt % 2 == 1 && DialogueSequenceInt != GabMultDialogueInt[0])
                    {
                        // Gab dialogue plays
                        // Switch off Opp's dialogue and switch on Gab's dialogue
                        StoryManager.b_SwitchOffOppDialogue = true;
                        StoryManager.b_SwitchOnGabDialogue = true;
                        DialogueGabSing.text = GabDialogueList[(DialogueSequenceInt + 1) / 2];
                    }
                }
            }
        }
    }

    void SwitchOnGabMultiple()
    {
        GabMultipleActive = true;
        TimerUI.SetActive(true);
        StoryManager.b_SwitchOffGabDialogue = true;
        StoryManager.b_SwitchOffOppDialogue = true;
        StoryManager.b_SwitchOnGabMultDialogue = true;
    }

    void SwitchOffGabMultiple()
    {
        GabMultipleActive = false;
        TimerUI.SetActive(false);
        StoryManager.b_SwitchOffGabMultDialogue = true;
        StoryManager.b_SwitchOnOppDialogue = true;
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
