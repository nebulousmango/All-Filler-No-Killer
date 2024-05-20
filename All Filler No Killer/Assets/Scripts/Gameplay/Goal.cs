using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] bool isPlayerOppGoal;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            if (isPlayerOppGoal)
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerGabScored();
            }
            else
            {
                GameObject.Find("GameManager").GetComponent<GameManager>().PlayerOppScored();
            }
        }
    }


}
