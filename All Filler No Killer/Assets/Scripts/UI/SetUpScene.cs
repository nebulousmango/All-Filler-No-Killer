using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpScene : MonoBehaviour
{
    [SerializeField] GameObject[] ActivateObjects;
    [SerializeField] GameObject[] DeactivateObjects;

    private void Start()
    {
        for (int i = 0; i < ActivateObjects.Length; i++)
        {
            if (ActivateObjects[i] != null)
            {
                ActivateObjects[i].SetActive(true);
            }
        }
        for (int i = 0; i < DeactivateObjects.Length; i++)
        {
            if (DeactivateObjects[i] != null)
            {
                DeactivateObjects[i].SetActive(false);
            }
        }
    }
}
