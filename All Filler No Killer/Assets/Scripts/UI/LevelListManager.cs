using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelListManager : MonoBehaviour
{
    // Attached to LevelListManager object in 04_LevelSelect scene.

    [SerializeField] GameObject[] LevelLockedObjects;
    [SerializeField] GameObject[] LevelUnlockedObjects;
    public bool[] IsLevelComplete;
    public LevelsCompleted levelsCompleted;
    public int LevelsCompletedInt;
    
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

        LevelsCompletedInt = PlayerPrefs.GetInt("LevelsCompletedInt");

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
    }
}
