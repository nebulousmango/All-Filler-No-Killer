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
    [SerializeField] TextMeshPro oppScoreText;

    [Header("Player Gab")]
    [SerializeField] GameObject playerGabPaddle;
    [SerializeField] GameObject playerGabGoal;
    [SerializeField] TextMeshPro playerScoreText;

    [Header("Dialogue")]
    // GameMode = 0 is when Game is in Pong mode.
    // GameMode = 1 is when Game is in Talking mode.
    // GameMode = 2 is when Game is in LevelEnd mode.
    public int GameMode;

    [Header("Points")]
    [SerializeField] GameObject OppScoreText;
    [SerializeField] TextMeshPro OppScoreTextTMP;
    [SerializeField] GameObject GabScoreText;

    [Header("Level End")]
    [SerializeField] GameObject LevelEndPanel;
    [SerializeField] TextMeshProUGUI finalOppScoreText;
    [SerializeField] TextMeshProUGUI finalGabScoreText;

    public bool LevelOver = false;
    public int PlayerOppScore;
    public int PlayerGabScore;

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
        StartCoroutine(GabScoreIncreaseText());
        PlayerGabScore++;
        ResetPosition();
        ChangeToTextPlayer("" + PlayerGabScore);
        GameMode = 1;
        FindObjectOfType<StoryManager>().PlayHmm();
    }

    public void PlayerOppScored()
    {
        StartCoroutine(OppScoreIncreaseText());
        PlayerOppScore++;
        ResetPosition();
        ChangeToTextOpp("" + PlayerOppScore);
        GameMode = 1;
        FindObjectOfType<StoryManager>().PlayHmm();
    }

    private void ResetPosition()
    {
        ball.GetComponent<Ball>().Reset();
    }

    public void ChangeToTextOpp(string text)
    {
        oppScoreText.text = text;
    }

    public void ChangeToTextPlayer(string text)
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

    IEnumerator GabScoreIncreaseText()
    {
        GabScoreText.SetActive(true);
        yield return new WaitForSeconds(2);
        GabScoreText.SetActive(false);
    }

    IEnumerator OppScoreIncreaseText()
    {
        OppScoreText.SetActive(true);
        yield return new WaitForSeconds(2);
        OppScoreText.SetActive(false);
    }

    IEnumerator OppScoreIncreaseTextPlusTwo()
    {
        OppScoreTextTMP.text = ("+2");
        OppScoreText.SetActive(true);
        yield return new WaitForSeconds(2);
        OppScoreTextTMP.text = ("+1");
        OppScoreText.SetActive(false);
    }

    public void ScoreGood()
    {
        StartCoroutine(GabScoreIncreaseText());
    }

    public void ScoreBad()
    {
        StartCoroutine(OppScoreIncreaseText());
    }

    public void ScoreUgly()
    {
        StartCoroutine(OppScoreIncreaseTextPlusTwo());
    }
}
