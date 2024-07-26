using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpScene : MonoBehaviour
{
    [SerializeField] GameObject[] ActivateObjects;
    [SerializeField] GameObject[] DectivateObjects;

    private void Start()
    {
        for (int i = 0; i < ActivateObjects.Length; i++)
        {
            ActivateObjects[i].SetActive(true); 
        }
        for (int i = 0; i < DectivateObjects.Length; i++)
        {
            DectivateObjects[i].SetActive(false);
        }
    }
}
