using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GodMode : MonoBehaviour
{
    [SerializeField] GameObject Star;

    private void Start()
    {
        if (FindObjectOfType<LevelsCompleted>().GodMode == false)
        {
            Star.GetComponent<Animator>().SetBool("GodMode", false);
        }
        else if (FindObjectOfType<LevelsCompleted>().GodMode == true)
        {
            Star.GetComponent<Animator>().SetBool("GodMode", true);
        }
    }

    public void GodModeOn()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        if (FindObjectOfType<LevelsCompleted>().GodMode == false)
        {
            FindObjectOfType<LevelsCompleted>().GodMode = true;
            Star.GetComponent<Animator>().SetBool("GodMode", true);
        }
        else if (FindObjectOfType<LevelsCompleted>().GodMode == true)
        {
            FindObjectOfType<LevelsCompleted>().GodMode = false;
            Star.GetComponent<Animator>().SetBool("GodMode", false);
        }
    }
}
