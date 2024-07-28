using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelProgress : MonoBehaviour
{
    // Attached to Canvas object in Level scenes.

    public string NextScene;
    private string CurrentScene;
    LevelsCompleted LevelsCompleted;

    private void Start()
    {
        LevelsCompleted = FindObjectOfType<LevelsCompleted>();
        Scene currentScene = SceneManager.GetActiveScene();
        CurrentScene = currentScene.name;
        if (CurrentScene == "02_Cutscene1")
        {
            PlayerPrefs.SetInt("LevelsCompletedInt", 0);
            PlayerPrefs.Save();
            LevelsCompleted.SetAllIncomplete();
        }
    }

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
            PlayerPrefs.SetInt("LevelsCompletedInt", 1);
            PlayerPrefs.Save();
        }
        if (CurrentScene == "05_Level2")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[1] = true;
            PlayerPrefs.SetInt("LevelsCompletedInt", 2);
            PlayerPrefs.Save();
        }
        if (CurrentScene == "06_Level3")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[2] = true;
            PlayerPrefs.SetInt("LevelsCompletedInt", 3);
            PlayerPrefs.Save();
        }
        if (CurrentScene == "07_Level4")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[3] = true;
            PlayerPrefs.SetInt("LevelsCompletedInt", 4);
            PlayerPrefs.Save();
        }
        if (CurrentScene == "09_Level5")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[4] = true;
            PlayerPrefs.SetInt("LevelsCompletedInt", 5);
            PlayerPrefs.Save();
        }
        if (CurrentScene == "10_Level6")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[5] = true;
            PlayerPrefs.SetInt("LevelsCompletedInt", 6);
            PlayerPrefs.Save();
        }
        if (CurrentScene == "11_Level7")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[6] = true;
            PlayerPrefs.SetInt("LevelsCompletedInt", 7);
            PlayerPrefs.Save();
        }
        if (CurrentScene == "12_Level8")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[7] = true;
            PlayerPrefs.SetInt("LevelsCompletedInt", 8);
            PlayerPrefs.Save();
        }
        if (CurrentScene == "13_Level9")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[8] = true;
            PlayerPrefs.SetInt("LevelsCompletedInt", 9);
            PlayerPrefs.Save();
        }
        if (CurrentScene == "15_Level10")
        {
            FindObjectOfType<LevelsCompleted>().IsLevelComplete[9] = true;
            PlayerPrefs.SetInt("LevelsCompletedInt", 10);
            PlayerPrefs.Save();
        }
    }
}
