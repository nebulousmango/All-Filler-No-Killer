using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    // Attached to Canvas object in Level scenes.

    public string NextScene;
    private string CurrentScene;

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
        if (CurrentScene == "03_Level1")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[0] = true;
        }
        if (CurrentScene == "05_Level2")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[1] = true;
        }
        if (CurrentScene == "06_Level3")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[2] = true;
        }
        if (CurrentScene == "07_Level4")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[3] = true;
        }
        if (CurrentScene == "09_Level5")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[4] = true;
        }
    }
}
