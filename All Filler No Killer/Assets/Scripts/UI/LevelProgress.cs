using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    // Attached to Canvas object in Level scenes.

    public string NextScene;
    string CurrentScene;

    public void LoadNextLevel()
    {
        NextScene = "04_LevelSelect";
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(NextScene);
        StoreCurrentLevel();
    }

    void StoreCurrentLevel()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        CurrentScene = currentScene.name;
        FindObjectOfType<SaveSystem>().LastLevelPlayed = CurrentScene;
    }
}
