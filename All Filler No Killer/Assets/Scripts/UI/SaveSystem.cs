using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystem : MonoBehaviour
{
    // Attached to SaveManager object in 00_StartMenu scene.

    public string LastLevelPlayed;

    public void SaveLevelProgress()
    {
        PlayerPrefs.SetString("Level", LastLevelPlayed);
        PlayerPrefs.Save();
    }

    public void LoadLastLevel()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LastLevelPlayed);
    }
}
