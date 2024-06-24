using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    // Attached to GameManager object in Level scenes.

    [SerializeField] GameObject SingleDialogueGab;
    [SerializeField] GameObject MultipleDialogueGab;
    [SerializeField] GameObject SingleDialogueOpp;

    [SerializeField] GameObject DialogueGabSing;
    [SerializeField] GameObject DialogueGabMult1;
    [SerializeField] GameObject DialogueGabMult2;
    [SerializeField] GameObject DialogueGabMult3;
    [SerializeField] GameObject DialogueOpp;

    [SerializeField] int readingTime;

    IEnumerator StartGabDialogueSing()
    {
        SingleDialogueGab.SetActive(true);
        yield return new WaitForSeconds(1);
        DialogueGabSing.SetActive(true);
        yield return new WaitForSeconds(readingTime);
    }

    IEnumerator StartGabDialogueMult()
    {
        MultipleDialogueGab.SetActive(true);
        yield return new WaitForSeconds(1);
        DialogueGabMult1.SetActive(true);
        DialogueGabMult2.SetActive(true);
        DialogueGabMult3.SetActive(true);
        yield return new WaitForSeconds(readingTime);
    }

    IEnumerator StartOppDialogue()
    {
        SingleDialogueOpp.SetActive(true);
        yield return new WaitForSeconds(1);
        DialogueOpp.SetActive(true);
        yield return new WaitForSeconds(readingTime);
    }

    IEnumerator StopGabDialogueSing()
    {
        yield return new WaitForSeconds(1);
        DialogueGabSing.SetActive(false);
        SingleDialogueGab.SetActive(false);
    }

    IEnumerator StopGabDialogueMult()
    {
        yield return new WaitForSeconds(1);
        DialogueGabMult1.SetActive(false);
        DialogueGabMult2.SetActive(false);
        DialogueGabMult3.SetActive(false);
        MultipleDialogueGab.SetActive(false);

    }

    IEnumerator StopOppDialogue()
    {
        yield return new WaitForSeconds(1);
        DialogueOpp.SetActive(false);
        SingleDialogueOpp.SetActive(false);
    }
}
