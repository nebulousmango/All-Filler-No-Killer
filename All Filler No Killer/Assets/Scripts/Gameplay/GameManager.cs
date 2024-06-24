using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Attached to GameManager object in Level scenes.

    [Header("Object Groups")]
    [SerializeField] GameObject pongObjects;
    [SerializeField] GameObject talkObjects;

    [Header("Ball")]
    [SerializeField] GameObject ball;

    [Header("Player Opp")]
    [SerializeField] GameObject playerOppPaddle;
    [SerializeField] GameObject playerOppGoal;
    [SerializeField] TextMeshProUGUI oppScoreText;

    [Header("Player Gab")]
    [SerializeField] GameObject playerGabPaddle;
    [SerializeField] GameObject playerGabGoal;
    [SerializeField] TextMeshProUGUI playerScoreText;

    [Header("Dialogue")]
    public bool GameIsTalking;

    [Header("Level End")]
    [SerializeField] int levelEndScore;
    [SerializeField] GameObject InPlayObjects;
    [SerializeField] GameObject LevelEndPanel;
    [SerializeField] TextMeshProUGUI finalOppScoreText;
    [SerializeField] TextMeshProUGUI finalGabScoreText;

    public bool LevelOver = false;
    private int PlayerOppScore;
    private int PlayerGabScore;

    private void Start()
    {
        LevelEndPanel.SetActive(false);
        InPlayObjects.SetActive(true);
        LevelOver = false;
    }

    private void Update()
    {
        if((PlayerGabScore+ PlayerOppScore) == levelEndScore)
        {
            LevelOver = true;
            EndLevel();
        }
        if(GameIsTalking==true)
        {
            pongObjects.SetActive(false);
            talkObjects.SetActive(true);
        }
        if (GameIsTalking == false)
        {
            pongObjects.SetActive(true);
            talkObjects.SetActive(false);
        }
    }

    public void PlayerGabScored()
    {
        PlayerGabScore++;
        ResetPosition();
        ChangeToTextPlayer("" + PlayerGabScore);
    }

    public void PlayerOppScored()
    {
        PlayerOppScore++;
        ResetPosition();
        ChangeToTextOpp("" + PlayerOppScore);
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
    }

    void ChangeToTextOpp(string text)
    {
        oppScoreText.text = text;
    }

    void ChangeToTextPlayer(string text)
    {
        playerScoreText.text = text;
    }

    void EndLevel()
    {
        LevelEndPanel.SetActive(true);
        InPlayObjects.SetActive(false);
        finalGabScoreText.text = ("" + PlayerGabScore);
        finalOppScoreText.text = ("" + PlayerOppScore);
    }
}
