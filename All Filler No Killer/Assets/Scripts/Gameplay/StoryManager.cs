using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [SerializeField] Animator dialogueAnimator;
    [SerializeField] GameObject dialogueText;
    [SerializeField] int readingTime;

    IEnumerator StartDialogue()
    {
        dialogueAnimator.SetBool("PlayDialogueAnimation", true);
        yield return new WaitForSeconds(0.5f);
        dialogueText.SetActive(true);
        yield return new WaitForSeconds(0.5f);
    }
}
