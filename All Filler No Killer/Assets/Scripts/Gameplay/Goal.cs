using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    // Attached to PlayerGoal_Opp and PlayerGoal_Gab objects in Level scenes.

    [SerializeField] bool isPlayerGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            if (isPlayerGoal)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerOppScored();
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerGabScored();
            }
        }
    }


}
