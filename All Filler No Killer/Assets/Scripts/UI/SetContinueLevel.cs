using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SetContinueLevel : MonoBehaviour
{
    public string S_ContinueLevel;

    public void ContinueLevel()
    {
        S_ContinueLevel = FindObjectOfType<SaveSystem>().LastLevelPlayed;
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(S_ContinueLevel);
    }

}
