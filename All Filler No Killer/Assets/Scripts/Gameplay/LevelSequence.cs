using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSequence : MonoBehaviour
{
    // Attached to LevelProg object in every Level scene.

    GameManager GameManager;
    StoryManager StoryManager;
    private int DialogueSequenceInt;
    private int DownArrowInt;
    private int PongSequenceInt;
    private bool PongFTUE;
    private bool GabMultFTUE;
    private int MultValueInt;

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
    public string GabMultType;

    [SerializeField] GameObject OppReactBubble;
    [SerializeField] GameObject OppReactGood;
    [SerializeField] GameObject OppReactBad;
    [SerializeField] GameObject OppReactUgly;
    [SerializeField] GameObject GabMultSelection1;
    [SerializeField] GameObject GabMultSelection2;
    [SerializeField] GameObject GabMultSelection3;
    [SerializeField] bool GabMultSelection1On;
    [SerializeField] bool GabMultSelection2On;
    [SerializeField] bool GabMultSelection3On;
    [SerializeField] float ExposedReadingTimer;
    [SerializeField] float ReadingTimer;
    [SerializeField] GameObject TimerUI;
    [SerializeField] TMP_Text TimerText;
    bool InteractedWithMult;

    [Header("Pong objects")]
    [SerializeField] Ball Ball;
    [SerializeField] PaddleOpp PaddleOpp;

    private void Start()
    {
        ReadingTimer = ExposedReadingTimer;
        GameManager = FindObjectOfType<GameManager>();
        StoryManager = FindObjectOfType<StoryManager>();
        DialogueGabSing.text = GabDialogueList[0];

        if (Level1)
        {
            FtuePrompts[0].SetActive(true);
        }    
    }

    private void Update()
    {
        if(GabMultipleActive == true)
        {
            OppReactBubble.SetActive(true);
            SetMultValue();
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
                OppReactBubble.SetActive(false);
                OppReactGood.SetActive(false);
                OppReactBad.SetActive(false);
                OppReactUgly.SetActive(false);
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && GabMultipleActive == true)
        {
            InteractedWithMult = true;
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
                FindObjectOfType<AudioManager>().PlaySound("MultButton");
                GabMultSelection1.SetActive(false);
                GabMultSelection2.SetActive(true);
                GabMultSelection3.SetActive(false);
                GabMultSelection1On = false;
                GabMultSelection2On = true;
                GabMultSelection3On = false;
            }
            if (DownArrowInt == 3)
            {
                FindObjectOfType<AudioManager>().PlaySound("MultButton");
                GabMultSelection1.SetActive(false);
                GabMultSelection2.SetActive(false);
                GabMultSelection3.SetActive(true);
                GabMultSelection1On = false;
                GabMultSelection2On = false;
                GabMultSelection3On = true;
            }
            if (DownArrowInt > 3)
            {
                FindObjectOfType<AudioManager>().PlaySound("MultButton");
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

                #region LevelPersonalisation
                if (Level1)
                {
                    LevelOneMults();
                }

                if (Level2)
                {
                    LevelTwoMults();
                }

                if (Level3)
                {
                    LevelThreeMults();
                }

                if (Level4)
                {
                    LevelThreeMults();
                }

                if (Level5)
                {
                    LevelThreeMults();
                }
                #endregion

                if (DialogueSequenceInt == LevelEndInt)
                {
                    EndLevel();
                }

                // Checks for an even dialogueSequenceInt and play Opp dialogue
                if ((DialogueSequenceInt==0 || DialogueSequenceInt%2==0) && DialogueSequenceInt != LevelEndInt)
                {
                    SwitchOnOpp();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space) && InteractedWithMult && !GabMultFTUE)
        {
            ReadingTimer = 0;
        }
    }

    void SwitchOnGab()
    {
        // Gab dialogue plays
        // Switch off Opp's dialogue and switch on Gab's dialogue
        StoryManager.b_SwitchOffOppDialogue = true;
        StoryManager.b_SwitchOnGabDialogue = true;
        DialogueGabSing.text = GabDialogueList[(DialogueSequenceInt + 1) / 2];
    }

    void SwitchOnOpp()
    {
        // Opp dialogue plays
        // Switch off Gab's dialogue and switch on Opp's dialogue
        StoryManager.b_SwitchOffGabDialogue = true;
        StoryManager.b_SwitchOnOppDialogue = true;
        DialogueOpp.text = OppDialogueList[DialogueSequenceInt / 2];
    }

    void SwitchOnGabMultiple()
    {
        MultValueInt++;
        InteractedWithMult = false;
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
        MultValueInt++;
        GabMultFTUE = true;
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
        yield return new WaitForSeconds(5);
        FtuePrompts[2].SetActive(true);
        yield return new WaitForSeconds(3);
        FtuePrompts[1].SetActive(false);
        FtuePrompts[2].GetComponent<Animator>().SetBool("FtueExit", true);
        ReadingTimer = ExposedReadingTimer;
        TimerUI.SetActive(true);
        GabMultFTUE = false;
    }

    void SwitchOffGabMultiple()
    {
        GabMultipleActive = false;
        TimerUI.SetActive(false);
        StoryManager.b_SwitchOffGabMultDialogue = true;
        StoryManager.b_SwitchOnOppDialogue = true;
    }

    void SetMultValue()
    {
        if (GabMultSelection1On)
        {
            if (GabMult1DialogueTypes[MultValueInt - 1] == "A")
            {
                GabMultType = "A: Good";
                OppReactGood.SetActive(true);
                OppReactBad.SetActive(false);
                OppReactUgly.SetActive(false);
            }
            if (GabMult1DialogueTypes[MultValueInt - 1] == "B")
            {
                GabMultType = "B: Bad";
                OppReactGood.SetActive(false);
                OppReactBad.SetActive(true);
                OppReactUgly.SetActive(false);
            }
            if (GabMult1DialogueTypes[MultValueInt - 1] == "C")
            {
                GabMultType = "C: Ugly";
                OppReactGood.SetActive(false);
                OppReactBad.SetActive(false);
                OppReactUgly.SetActive(true);
            }
        }
        if (GabMultSelection2On)
        {
            if (GabMult2DialogueTypes[MultValueInt - 1] == "A")
            {
                GabMultType = "A: Good";
                OppReactGood.SetActive(true);
                OppReactBad.SetActive(false);
                OppReactUgly.SetActive(false);
            }
            if (GabMult2DialogueTypes[MultValueInt - 1] == "B")
            {
                GabMultType = "B: Bad";
                OppReactGood.SetActive(false);
                OppReactBad.SetActive(true);
                OppReactUgly.SetActive(false);
            }
            if (GabMult2DialogueTypes[MultValueInt - 1] == "C")
            {
                GabMultType = "C: Ugly";
                OppReactGood.SetActive(false);
                OppReactBad.SetActive(false);
                OppReactUgly.SetActive(true);
            }
        }
        if (GabMultSelection3On)
        {
            if (GabMult3DialogueTypes[MultValueInt - 1] == "A")
            {
                GabMultType = "A: Good";
                OppReactGood.SetActive(true);
                OppReactBad.SetActive(false);
                OppReactUgly.SetActive(false);
            }
            if (GabMult3DialogueTypes[MultValueInt - 1] == "B")
            {
                GabMultType = "B: Bad";
                OppReactGood.SetActive(false);
                OppReactBad.SetActive(true);
                OppReactUgly.SetActive(false);
            }
            if (GabMult3DialogueTypes[MultValueInt - 1] == "C")
            {
                GabMultType = "C: Ugly";
                OppReactGood.SetActive(false);
                OppReactBad.SetActive(false);
                OppReactUgly.SetActive(true);
            }
        }
    }

    IEnumerator SwitchOnPong()
    {
        if (PongFTUE)
        {
            StartCoroutine(SwitchOnPongFTUE());
        }
        if (!PongFTUE)
        {
            Ball.GetComponent<Rigidbody2D>().isKinematic = true;
            PongSequenceInt++;
            GameManager.GameMode = 0;
            yield return new WaitForSeconds(0.5f);
            Ball.Reset();
            Ball.GetComponent<Rigidbody2D>().isKinematic = false;
            if (GabMultSelection1On)
            {
                if (GabMult1DialogueTypes[PongSequenceInt - 1] == "A")
                {
                    GabMultGood();
                }
                if (GabMult1DialogueTypes[PongSequenceInt - 1] == "B")
                {
                    GabMultBad();
                }
                if (GabMult1DialogueTypes[PongSequenceInt - 1] == "C")
                {
                    GabMultUgly();
                }
            }
            if (GabMultSelection2On)
            {
                if (GabMult2DialogueTypes[PongSequenceInt - 1] == "A")
                {
                    GabMultGood();
                }
                if (GabMult2DialogueTypes[PongSequenceInt - 1] == "B")
                {
                    GabMultBad();
                }
                if (GabMult2DialogueTypes[PongSequenceInt - 1] == "C")
                {
                    GabMultUgly();
                }
            }
            if (GabMultSelection3On)
            {
                if (GabMult3DialogueTypes[PongSequenceInt - 1] == "A")
                {
                    GabMultGood();
                }
                if (GabMult3DialogueTypes[PongSequenceInt - 1] == "B")
                {
                    GabMultBad();
                }
                if (GabMult3DialogueTypes[PongSequenceInt - 1] == "C")
                {
                    GabMultUgly();
                }
            }
        }
    }

    IEnumerator SwitchOnPongFTUE()
    {
        FtuePrompts[3].SetActive(true);
        GameManager.GameMode = 0;
        yield return new WaitForSeconds(3);
        FtuePrompts[3].GetComponent<Animator>().SetBool("FtueExit", true);
        Ball.Reset();
        PongFTUE = false;
        PongSequenceInt++;
        if (GabMultSelection1On)
        {
            if (GabMult1DialogueTypes[PongSequenceInt - 1] == "A")
            {
                GabMultGood();
            }
            if (GabMult1DialogueTypes[PongSequenceInt - 1] == "B")
            {
                GabMultBad();
            }
            if (GabMult1DialogueTypes[PongSequenceInt - 1] == "C")
            {
                GabMultUgly();
            }
        }
        if (GabMultSelection2On)
        {
            if (GabMult2DialogueTypes[PongSequenceInt - 1] == "A")
            {
                GabMultGood();
            }
            if (GabMult2DialogueTypes[PongSequenceInt - 1] == "B")
            {
                GabMultBad();
            }
            if (GabMult2DialogueTypes[PongSequenceInt - 1] == "C")
            {
                GabMultUgly();
            }
        }
        if (GabMultSelection3On)
        {
            if (GabMult3DialogueTypes[PongSequenceInt - 1] == "A")
            {
                GabMultGood();
            }
            if (GabMult3DialogueTypes[PongSequenceInt - 1] == "B")
            {
                GabMultBad();
            }
            if (GabMult3DialogueTypes[PongSequenceInt - 1] == "C")
            {
                GabMultUgly();
            }
        }
    }

    // Good dialogue option
    void GabMultGood()
    {
        GabMultType = "A: Good";
        Ball.BallVersionGood();
        GameManager.PlayerGabScore++;
        GameManager.ChangeToTextPlayer("" + GameManager.PlayerGabScore);
        GameManager.ScoreGood();
    }

    // Bad dialogue option
    void GabMultBad()
    {
        GabMultType = "B: Bad";
        Ball.BallVersionBad();
        PaddleOpp.speed = 9;
        GameManager.PlayerOppScore++;
        GameManager.ChangeToTextPlayer("" + GameManager.PlayerGabScore);
        GameManager.ScoreBad();
    }

    // Ugly dialogue option
    void GabMultUgly()
    {
        GabMultType = "C: Ugly";
        Ball.BallVersionUgly();
        PaddleOpp.speed = 11;
        GameManager.PlayerOppScore++;
        GameManager.ChangeToTextOpp("" + GameManager.PlayerOppScore);
        GameManager.ScoreUgly();
    }

    void EndLevel()
    {
        GameManager.EndLevel();
        GameManager.LevelOver = true;
    }

    void LevelOneMults()
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

        // Checks for an odd dialogueSequenceInt and play Gab dialogue
        if (DialogueSequenceInt % 2 == 1 && DialogueSequenceInt != GabMultDialogueInt[0] && DialogueSequenceInt != GabMultDialogueInt[1])
        {
            SwitchOnGab();
        }

        if (DialogueSequenceInt == 0)
        {
            FtuePrompts[0].GetComponent<Animator>().SetBool("FtueExit", true);
        }
    }

    void LevelTwoMults()
    {
        if (DialogueSequenceInt == GabMultDialogueInt[0])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
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

        if (DialogueSequenceInt == GabMultDialogueInt[2])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[6];
            DialogueGabMult2.text = GabMultDialogueList[7];
            DialogueGabMult3.text = GabMultDialogueList[8];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[3])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[9];
            DialogueGabMult2.text = GabMultDialogueList[10];
            DialogueGabMult3.text = GabMultDialogueList[11];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[4])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[12];
            DialogueGabMult2.text = GabMultDialogueList[13];
            DialogueGabMult3.text = GabMultDialogueList[14];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[5])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[15];
            DialogueGabMult2.text = GabMultDialogueList[16];
            DialogueGabMult3.text = GabMultDialogueList[17];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[6])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[18];
            DialogueGabMult2.text = GabMultDialogueList[19];
            DialogueGabMult3.text = GabMultDialogueList[20];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[7])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[21];
            DialogueGabMult2.text = GabMultDialogueList[22];
            DialogueGabMult3.text = GabMultDialogueList[23];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[8])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[24];
            DialogueGabMult2.text = GabMultDialogueList[25];
            DialogueGabMult3.text = GabMultDialogueList[26];
        }

        // Checks for an odd dialogueSequenceInt and play Gab dialogue
        if (DialogueSequenceInt % 2 == 1 && DialogueSequenceInt != GabMultDialogueInt[0]
        && DialogueSequenceInt != GabMultDialogueInt[1] && DialogueSequenceInt != GabMultDialogueInt[2]
        && DialogueSequenceInt != GabMultDialogueInt[3] && DialogueSequenceInt != GabMultDialogueInt[4]
        && DialogueSequenceInt != GabMultDialogueInt[5] && DialogueSequenceInt != GabMultDialogueInt[6]
        && DialogueSequenceInt != GabMultDialogueInt[7] && DialogueSequenceInt != GabMultDialogueInt[8])
        {
            SwitchOnGab();
        }
    }

    void LevelThreeMults()
    {
        if (DialogueSequenceInt == GabMultDialogueInt[0])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
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

        if (DialogueSequenceInt == GabMultDialogueInt[2])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[6];
            DialogueGabMult2.text = GabMultDialogueList[7];
            DialogueGabMult3.text = GabMultDialogueList[8];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[3])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[9];
            DialogueGabMult2.text = GabMultDialogueList[10];
            DialogueGabMult3.text = GabMultDialogueList[11];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[4])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[12];
            DialogueGabMult2.text = GabMultDialogueList[13];
            DialogueGabMult3.text = GabMultDialogueList[14];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[5])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[15];
            DialogueGabMult2.text = GabMultDialogueList[16];
            DialogueGabMult3.text = GabMultDialogueList[17];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[6])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[18];
            DialogueGabMult2.text = GabMultDialogueList[19];
            DialogueGabMult3.text = GabMultDialogueList[20];
        }

        if (DialogueSequenceInt == GabMultDialogueInt[7])
        {
            StoryManager.b_SwitchOffGabDialogue = true;
            SwitchOnGabMultiple();
            DialogueGabMult1.text = GabMultDialogueList[21];
            DialogueGabMult2.text = GabMultDialogueList[22];
            DialogueGabMult3.text = GabMultDialogueList[23];
        }

        // Checks for an odd dialogueSequenceInt and play Gab dialogue
        if (DialogueSequenceInt % 2 == 1 && DialogueSequenceInt != GabMultDialogueInt[0]
            && DialogueSequenceInt != GabMultDialogueInt[1] && DialogueSequenceInt != GabMultDialogueInt[2]
             && DialogueSequenceInt != GabMultDialogueInt[3] && DialogueSequenceInt != GabMultDialogueInt[4]
              && DialogueSequenceInt != GabMultDialogueInt[5] && DialogueSequenceInt != GabMultDialogueInt[6]
               && DialogueSequenceInt != GabMultDialogueInt[7])
        {
            SwitchOnGab();
        }
    }
}
