using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelListManager : MonoBehaviour
{
    // Attached to LevelListManager object in 04_LevelSelect scene.

    [SerializeField] GameObject[] LevelLockedObjects;
    [SerializeField] GameObject[] LevelUnlockedObjects;
    public bool[] IsLevelComplete;
    public LevelsCompleted levelsCompleted;
    public int LevelsCompletedInt;
    public int PlayerPrefsLevelsCompletedInt;
    [SerializeField] ScrollRect myScrollRect;

    private void Start()
    {
        levelsCompleted = FindObjectOfType<LevelsCompleted>();
        IsLevelComplete = levelsCompleted.IsLevelComplete;

        foreach(GameObject LevelUnlockedObject in LevelUnlockedObjects)
        {
            LevelUnlockedObject.SetActive(false);
        }

        foreach (GameObject LevelLockedObject in LevelLockedObjects)
        {
            LevelLockedObject.SetActive(true);
        }

        LevelLockedObjects[0].SetActive(false);
        LevelUnlockedObjects[0].SetActive(true);

        PlayerPrefsLevelsCompletedInt = PlayerPrefs.GetInt("LevelsCompletedInt");

        if (PlayerPrefsLevelsCompletedInt >= 0)
        {
            LevelsCompletedInt = PlayerPrefsLevelsCompletedInt;
        }

        if (LevelsCompletedInt == 0)
        {
            for (int i = 0; i < LevelsCompletedInt + 1; i++)
            {
                IsLevelComplete[i] = false;
            }
        }

        if (LevelsCompletedInt > 0)
        {
            for (int i = 0; i < LevelsCompletedInt; i++)
            {
                IsLevelComplete[i] = true;
            }
        }

        for (int i = 0; i < IsLevelComplete.Length-1; i++)
        {
            if (IsLevelComplete[i] == true)
            {
                LevelUnlockedObjects[i].SetActive(true);
                LevelLockedObjects[i].SetActive(false);
                LevelUnlockedObjects[i+1].SetActive(true);
                LevelLockedObjects[i+1].SetActive(false);
                LevelsCompletedInt = i+1;
            }
        }

        if (IsLevelComplete[0] == true)
        {
            myScrollRect.verticalNormalizedPosition = 0.75f;
        }

        if (IsLevelComplete[1] == true)
        {
            myScrollRect.verticalNormalizedPosition = 0.45f;
        }

        if (IsLevelComplete[2] == true)
        {
            myScrollRect.verticalNormalizedPosition = 0.15f;
        }

        if (IsLevelComplete[3] == true)
        {
            myScrollRect.verticalNormalizedPosition = 0;
        }
    }
}
