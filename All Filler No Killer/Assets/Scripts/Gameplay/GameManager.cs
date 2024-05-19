using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Ball")]
    public GameObject ball;

    [Header("Player Opp")]
    public GameObject playerOppPaddle;
    public GameObject playerOppGoal;

    [Header("Player Gab")]
    public GameObject playerGabPaddle;
    public GameObject playerGabGoal;

    private int PlayerOppScore;
    private int PlayerGabScore;

    public void PlayerOppScored()
    {
        PlayerOppScore++;
        ResetPosition();
    }

    public void PlayerGabScored()
    {
        PlayerGabScore++;
        ResetPosition();
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
        playerOppPaddle.GetComponent<Paddle>().Reset();
        playerGabPaddle.GetComponent<Paddle>().Reset();
    }
}
