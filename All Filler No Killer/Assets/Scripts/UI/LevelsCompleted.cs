using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelsCompleted : MonoBehaviour
{
    // Attached to LevelManager object in 00_StartMenu scene.

    public bool[] IsLevelComplete;

    public void SetAllIncomplete()
    {
        for (int i = 0; i < IsLevelComplete.Length; i++)
        {
            IsLevelComplete[i] = false;
        }
    }
}
