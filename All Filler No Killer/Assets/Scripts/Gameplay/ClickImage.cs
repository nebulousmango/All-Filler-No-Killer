using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickImage : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] GameObject TurnThisOn;
    [SerializeField] GameObject TurnThisOff;

    public void OnPointerDown(PointerEventData eventData)
    {
        TurnThisOn.SetActive(true);
        TurnThisOff.SetActive(false);
    }
}