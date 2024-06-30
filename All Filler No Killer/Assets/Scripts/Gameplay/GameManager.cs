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
    // GameMode = 0 is when Game is in Pong mode.
    // GameMode = 1 is when Game is in Talking mode.
    // GameMode = 2 is when Game is in LevelEnd mode.
    public int GameMode;

    [Header("Level End")]
    [SerializeField] GameObject LevelEndPanel;
    [SerializeField] TextMeshProUGUI finalOppScoreText;
    [SerializeField] TextMeshProUGUI finalGabScoreText;

    public bool LevelOver = false;
    private int PlayerOppScore;
    private int PlayerGabScore;

    private void Start()
    {
        LevelEndPanel.SetActive(false);
        LevelOver = false;
        GameMode = 1;
    }

    private void Update()
    {
        if(GameMode==1)
        {
            pongObjects.SetActive(false);
            talkObjects.SetActive(true);
        }
        if (GameMode == 0)
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
        GameMode = 1;
    }

    public void PlayerOppScored()
    {
        PlayerOppScore++;
        ResetPosition();
        ChangeToTextOpp("" + PlayerOppScore);
        GameMode = 1;
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

    public void EndLevel()
    {
        StartCoroutine(SwitchOffSceneObjects());
    }

    IEnumerator SwitchOffSceneObjects()
    {
        GameMode = 3;
        pongObjects.SetActive(false);
        talkObjects.SetActive(false);
        yield return new WaitForSeconds(0.01f);
        LevelEndPanel.SetActive(true);
        finalGabScoreText.text = ("" + PlayerGabScore);
        finalOppScoreText.text = ("" + PlayerOppScore);
    }
}
