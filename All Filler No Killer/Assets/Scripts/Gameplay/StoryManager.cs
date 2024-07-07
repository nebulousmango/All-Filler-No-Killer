using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    // Attached to GameManager object in Level scenes.

    int i = -1;
    LevelSequence LevelSequence;

    [Header("Speech bubble objects")]
    [SerializeField] GameObject SingleDialogueGab;
    [SerializeField] GameObject MultipleDialogueGab;
    [SerializeField] GameObject SingleDialogueOpp;

    [Header("Dialogue TMPro game objects")]
    [SerializeField] GameObject DialogueGabSing;
    [SerializeField] GameObject DialogueGabMult1;
    [SerializeField] GameObject DialogueGabMult2;
    [SerializeField] GameObject DialogueGabMult3;
    [SerializeField] GameObject DialogueOpp;

    [Header("Speech bubble animators")]
    [SerializeField] Animator animSingleDialogueGab;
    [SerializeField] Animator animSingleDialogueOpp;
    [SerializeField] Animator animMult1DialogueGab;
    [SerializeField] Animator animMult2DialogueGab;
    [SerializeField] Animator animMult3DialogueGab;

    [Header("Animation")]
    [SerializeField] float animPlayTime;
    public bool animPlaying;

    [Header("Bools for Gab single dialogue")]
    public bool b_SwitchOnGabDialogue;
    public bool b_SwitchOffGabDialogue;

    [Header("Bools for Opp single dialogue")]
    public bool b_SwitchOnOppDialogue;
    public bool b_SwitchOffOppDialogue;

    [Header("Bools for Gab multiple dialogue options")]
    public bool b_SwitchOnGabMultDialogue;
    public bool b_SwitchOffGabMultDialogue;

    [Header("Bools for dialogue sequence")]
    public bool[] DialogueSequenceUnlocked;

    private void Start()
    {
        SingleDialogueGab.SetActive(false);
        MultipleDialogueGab.SetActive(false);
        SingleDialogueOpp.SetActive(false);

        DialogueGabSing.SetActive(false);
        DialogueGabMult1.SetActive(false);
        DialogueGabMult2.SetActive(false);
        DialogueGabMult3.SetActive(false);
        DialogueOpp.SetActive(false);

        StartCoroutine(FirstSceneDialogue());
        int dialogueSequenceInt = (FindObjectOfType<LevelSequence>().LevelEndInt) + 2;
        DialogueSequenceUnlocked = new bool[dialogueSequenceInt];

        LevelSequence = FindObjectOfType<LevelSequence>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && LevelSequence.GabMultipleActive == false && !animPlaying)
        {
            i++;
            DialogueSequenceUnlocked[i] = true;
        }

        if (b_SwitchOnGabDialogue)
        {
            b_SwitchOnGabDialogue = false;
            b_SwitchOffGabDialogue = false;
            StartGabDialogueSing();
        }

        if (b_SwitchOffGabDialogue)
        {
            b_SwitchOnGabDialogue = false;
            b_SwitchOffGabDialogue = false;
            StopGabDialogueSing();
        }

        if (b_SwitchOnGabMultDialogue)
        {
            b_SwitchOnGabMultDialogue = false;
            b_SwitchOffGabMultDialogue = false;
            StartGabDialogueMult();
        }

        if (b_SwitchOffGabMultDialogue)
        {
            b_SwitchOnGabMultDialogue = false;
            b_SwitchOffGabMultDialogue = false;
            StopGabDialogueMult();
        }

        if (b_SwitchOnOppDialogue)
        {
            StartOppDialogue();
            b_SwitchOnOppDialogue = false;
            b_SwitchOffOppDialogue = false;
        }

        if (b_SwitchOffOppDialogue)
        {
            b_SwitchOnOppDialogue = false;
            b_SwitchOffOppDialogue = false;
            StopOppDialogue();
        }
    }

    public void AdvanceDialogueSequence()
    {
        i++;
        DialogueSequenceUnlocked[i] = true;
    }

    public void StartGabDialogueSing()
    {
        StartCoroutine(StartGabDialogueSingCoroutine());
    }

    public void StartGabDialogueMult()
    {
        StartCoroutine(StartGabDialogueMultCoroutine());
    }

    public void StartOppDialogue()
    {
        StartCoroutine(StartOppDialogueCoroutine());
    }

    public void StopGabDialogueSing()
    {
        StartCoroutine(StopGabDialogueSingCoroutine());
    }

    public void StopGabDialogueMult()
    {
        StartCoroutine(StopGabDialogueMultCoroutine());
    }

    public void StopOppDialogue()
    {
        StartCoroutine(StopOppDialogueCoroutine());
    }

    IEnumerator StartGabDialogueSingCoroutine()
    {
        animPlaying = true;
        SingleDialogueGab.SetActive(true);
        yield return new WaitForSeconds(animPlayTime);
        DialogueGabSing.SetActive(true);
        animPlaying = false;
    }

    IEnumerator StartGabDialogueMultCoroutine()
    {
        animPlaying = true;
        MultipleDialogueGab.SetActive(true);
        yield return new WaitForSeconds(animPlayTime);
        DialogueGabMult1.SetActive(true);
        DialogueGabMult2.SetActive(true);
        DialogueGabMult3.SetActive(true);
        animPlaying = false;
    }

    IEnumerator StartOppDialogueCoroutine()
    {
        animPlaying = true;
        SingleDialogueOpp.SetActive(true);
        yield return new WaitForSeconds(animPlayTime);
        DialogueOpp.SetActive(true);
        animPlaying = false;
    }

    IEnumerator StopGabDialogueSingCoroutine()
    {
        animPlaying = true;
        DialogueGabSing.SetActive(false);
        animSingleDialogueGab.SetBool("ExitBubble", true);
        yield return new WaitForSeconds(animPlayTime);
        SingleDialogueGab.SetActive(false);
        animPlaying = false;
    }

    IEnumerator StopGabDialogueMultCoroutine()
    {
        animPlaying = true;
        DialogueGabMult1.SetActive(false);
        DialogueGabMult2.SetActive(false);
        DialogueGabMult3.SetActive(false);
        animMult1DialogueGab.SetBool("ExitBubble", true);
        animMult2DialogueGab.SetBool("ExitBubble", true);
        animMult3DialogueGab.SetBool("ExitBubble", true);
        yield return new WaitForSeconds(animPlayTime);
        MultipleDialogueGab.SetActive(false);
        animPlaying = false;
    }

    IEnumerator StopOppDialogueCoroutine()
    {
        animPlaying = true;
        DialogueOpp.SetActive(false);
        animSingleDialogueOpp.SetBool("ExitBubble", true);
        yield return new WaitForSeconds(animPlayTime);
        SingleDialogueOpp.SetActive(false);
        animPlaying = false;
    }

    IEnumerator FirstSceneDialogue()
    {
        animPlaying = true;
        yield return new WaitForSeconds(1);
        b_SwitchOnGabDialogue = true;
        animPlaying = false;
    }
}
