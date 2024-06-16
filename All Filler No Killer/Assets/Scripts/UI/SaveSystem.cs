using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    public string LastLevelPlayed;

    void SaveLevel()
    {
        PlayerPrefs.SetString("Level", LastLevelPlayed);
    }

    public void LoadLastLevel()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LastLevelPlayed);
    }
}
