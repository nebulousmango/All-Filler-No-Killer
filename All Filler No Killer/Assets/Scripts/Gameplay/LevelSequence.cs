using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSequence : MonoBehaviour
{
    // Attached to LevelProg object in every Level scene.

    GameManager GameManager;
    StoryManager StoryManager;
    Ball Ball;
    PaddleOpp PaddleOpp;
    int DialogueSequenceInt;
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
    [SerializeField] string[] GabMult1DialogueTypes;
    [SerializeField] string[] GabMult2DialogueTypes;
    [SerializeField] string[] GabMult3DialogueTypes;

    [Header("Sequence ints")]
    [SerializeField] int[] GabMultDialogueInt;
    [SerializeField] public int LevelEndInt;

    [Header("Gab multiple dialogue")]
    public bool GabMultipleActive;
    [SerializeField] GameObject GabMultSelection1;
    [SerializeField] GameObject GabMultSelection2;
    [SerializeField] GameObject GabMultSelection3;
    [SerializeField] bool GabMultSelection1On;
    [SerializeField] bool GabMultSelection2On;
    [SerializeField] bool GabMultSelection3On;
    [SerializeField] float ReadingTimer;
    [SerializeField] GameObject TimerUI;
    [SerializeField] TMP_Text TimerText;
    [SerializeField] int GabDialogueScore;
    [SerializeField] int OppDialogueScore;

    private void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
        StoryManager = FindObjectOfType<StoryManager>();
        Ball = FindObjectOfType<Ball>();
        PaddleOpp = FindObjectOfType<PaddleOpp>();
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
                GabMultSelection1On = true;
                GabMultSelection2On = false;
                GabMultSelection3On = false;
            }
            if (DownArrowInt == 2)
            {
                GabMultSelection1.SetActive(false);
                GabMultSelection2.SetActive(true);
                GabMultSelection3.SetActive(false);
                GabMultSelection1On = false;
                GabMultSelection2On = true;
                GabMultSelection3On = false;
            }
            if (DownArrowInt == 3)
            {
                GabMultSelection1.SetActive(false);
                GabMultSelection2.SetActive(false);
                GabMultSelection3.SetActive(true);
                GabMultSelection1On = false;
                GabMultSelection2On = false;
                GabMultSelection3On = true;
            }
            if (DownArrowInt > 3)
            {
                DownArrowInt = 1;
                GabMultSelection1.SetActive(true);
                GabMultSelection2.SetActive(false);
                GabMultSelection3.SetActive(false);
                GabMultSelection1On = true;
                GabMultSelection2On = false;
                GabMultSelection3On = false;
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
        GabMultSelection1.SetActive(true);
        GabMultSelection2.SetActive(false);
        GabMultSelection3.SetActive(false);
        GabMultSelection1On = true;
        GabMultSelection2On = false;
        GabMultSelection3On = false;
        DownArrowInt = 1;
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
        GameManager.GameMode = 0;
        yield return new WaitForSeconds(0.01f);
        Ball.Reset();
        if(GabMultSelection1On)
        {
            for (int i = 0; i < GabMult1DialogueTypes.Length; i++)
            {
                if (GabMult1DialogueTypes[i] == "A")
                {
                    GabMultTypeA();
                }
                if (GabMult1DialogueTypes[i] == "B")
                {
                    GabMultTypeB();
                }
                if (GabMult1DialogueTypes[i] == "C")
                {
                    GabMultTypeC();
                }
            }
        }
        if (GabMultSelection2On)
        {
            for (int i = 0; i < GabMult2DialogueTypes.Length; i++)
            {
                if (GabMult2DialogueTypes[i] == "A")
                {
                    GabMultTypeA();
                }
                if (GabMult2DialogueTypes[i] == "B")
                {
                    GabMultTypeB();
                }
                if (GabMult2DialogueTypes[i] == "C")
                {
                    GabMultTypeC();
                }
            }
        }
        if (GabMultSelection3On)
        {
            for (int i = 0; i < GabMult3DialogueTypes.Length; i++)
            {
                if (GabMult3DialogueTypes[i] == "A")
                {
                    GabMultTypeA();
                }
                if (GabMult3DialogueTypes[i] == "B")
                {
                    GabMultTypeB();
                }
                if (GabMult3DialogueTypes[i] == "C")
                {
                    GabMultTypeC();
                }
            }
        }
    }

    void GabMultTypeA()
    {
        Ball.BallVersionA();
        GameManager.PlayerGabScore--;
        GameManager.ChangeToTextPlayer("" + GameManager.PlayerGabScore);
    }

    void GabMultTypeB()
    {
        Ball.BallVersionB();
        PaddleOpp.speed = 9;
        GameManager.PlayerGabScore++;
        GameManager.ChangeToTextPlayer("" + GameManager.PlayerGabScore);
    }

    void GabMultTypeC()
    {
        Ball.BallVersionC();
        GameManager.PlayerOppScore++;
        GameManager.ChangeToTextOpp("" + GameManager.PlayerOppScore);
    }

    void EndLevel()
    {
        GameManager.EndLevel();
        GameManager.LevelOver = true;
    }
}
