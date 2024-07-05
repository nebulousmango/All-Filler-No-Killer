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
    int PongSequenceInt;
    public bool PongFTUE;

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
    [SerializeField] float ExposedReadingTimer;
    float ReadingTimer;
    [SerializeField] GameObject TimerUI;
    [SerializeField] TMP_Text TimerText;

    private void Start()
    {
        ReadingTimer = ExposedReadingTimer;
        GameManager = FindObjectOfType<GameManager>();
        StoryManager = FindObjectOfType<StoryManager>();
        Ball = FindObjectOfType<Ball>();
        PaddleOpp = FindObjectOfType<PaddleOpp>();
        DialogueGabSing.text = GabDialogueList[0];
        if(Level1)
        {
            FtuePrompts[0].SetActive(true);
        }    
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
                        StartCoroutine(SwitchOnGabMultipleFTUE());
                        DialogueGabMult1.text = GabMultDialogueList[0];
                        DialogueGabMult2.text = GabMultDialogueList[1];
                        DialogueGabMult3.text = GabMultDialogueList[2];
                    }

                    if (DialogueSequenceInt == GabMultDialogueInt[1])
                    {
                        StoryManager.b_SwitchOffGabDialogue = true;
                        SwitchOnGabMultiple();
                        DialogueGabMult1.text = GabMultDialogueList[3];
                        DialogueGabMult2.text = GabMultDialogueList[4];
                        DialogueGabMult3.text = GabMultDialogueList[5];
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
                    if (DialogueSequenceInt % 2 == 1 && DialogueSequenceInt != GabMultDialogueInt[0] && DialogueSequenceInt != GabMultDialogueInt[1])
                    {
                        // Gab dialogue plays
                        // Switch off Opp's dialogue and switch on Gab's dialogue
                        StoryManager.b_SwitchOffOppDialogue = true;
                        StoryManager.b_SwitchOnGabDialogue = true; 
                        DialogueGabSing.text = GabDialogueList[(DialogueSequenceInt + 1) / 2];
                    }

                    if (DialogueSequenceInt == 0)
                    {
                        FtuePrompts[0].SetActive(false);
                    }
                }
            }
        }
    }

    void SwitchOnGabMultiple()
    {
        ReadingTimer = ExposedReadingTimer;
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

    IEnumerator SwitchOnGabMultipleFTUE()
    {
        PongFTUE = true;
        GabMultipleActive = true;
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
        FtuePrompts[1].SetActive(true);
        yield return new WaitForSeconds(4);
        FtuePrompts[2].SetActive(true);
        yield return new WaitForSeconds(4);
        FtuePrompts[1].SetActive(false);
        FtuePrompts[2].SetActive(false);
        ReadingTimer = ExposedReadingTimer;
        TimerUI.SetActive(true);
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
        if(PongFTUE)
        {
            StartCoroutine(SwitchOnPongFTUE());
        }
        PongSequenceInt++;
        GameManager.GameMode = 0;
        yield return new WaitForSeconds(0.1f);
        Ball.Reset();
        if(GabMultSelection1On)
        {
                if (GabMult1DialogueTypes[PongSequenceInt-1] == "A")
                {
                    GabMultTypeA();
                }
                if (GabMult1DialogueTypes[PongSequenceInt - 1] == "B")
                {
                    GabMultTypeB();
                }
                if (GabMult1DialogueTypes[PongSequenceInt - 1] == "C")
                {
                    GabMultTypeC();
                }
        }
        if (GabMultSelection2On)
        {
                if (GabMult2DialogueTypes[PongSequenceInt - 1] == "A")
                {
                    GabMultTypeA();
                }
                if (GabMult2DialogueTypes[PongSequenceInt - 1] == "B")
                {
                    GabMultTypeB();
                }
                if (GabMult2DialogueTypes[PongSequenceInt - 1] == "C")
                {
                    GabMultTypeC();
                }
        }
        if (GabMultSelection3On)
        {
                if (GabMult3DialogueTypes[PongSequenceInt - 1] == "A")
                {
                    GabMultTypeA();
                }
                if (GabMult3DialogueTypes[PongSequenceInt - 1] == "B")
                {
                    GabMultTypeB();
                }
                if (GabMult3DialogueTypes[PongSequenceInt - 1] == "C")
                {
                    GabMultTypeC();
                }
        }
    }

    IEnumerator SwitchOnPongFTUE()
    {
        FtuePrompts[3].SetActive(true);
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(4);
        Time.timeScale = 1.0f;
        FtuePrompts[3].SetActive(false);
        PongFTUE = false;
    }

    // Good dialogue option
    void GabMultTypeA()
    {
        Ball.BallVersionA();
        GameManager.PlayerGabScore++;
        GameManager.ChangeToTextPlayer("" + GameManager.PlayerGabScore);
    }
    
    // Bad dialogue option
    void GabMultTypeB()
    {
        Ball.BallVersionB();
        PaddleOpp.speed = 9;
        GameManager.PlayerOppScore++;
        GameManager.ChangeToTextPlayer("" + GameManager.PlayerGabScore);
    }

    // Ugly dialogue option
    void GabMultTypeC()
    {
        Ball.BallVersionC();
        GameManager.PlayerOppScore = GameManager.PlayerOppScore + 2;
        GameManager.ChangeToTextOpp("" + GameManager.PlayerOppScore);
    }

    void EndLevel()
    {
        GameManager.EndLevel();
        GameManager.LevelOver = true;
    }
}
