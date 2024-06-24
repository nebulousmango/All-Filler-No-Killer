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

        for (int i = 0; i < IsLevelComplete.Length; i++)
        {
            if (IsLevelComplete[i] == true)
            {
                LevelUnlockedObjects[i].SetActive(true);
                LevelLockedObjects[i].SetActive(false);
                LevelUnlockedObjects[i+1].SetActive(true);
                LevelLockedObjects[i+1].SetActive(false);
            }
        }
    }
}
