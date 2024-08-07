using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickImage : MonoBehaviour, IPointerDownHandler
{
    // Attached to Selector objects in 02_Cutscene1.

    [SerializeField] GameObject TurnThisOn;
    [SerializeField] GameObject TurnThisOff;
    [SerializeField] GameObject TurnThisOn1;
    [SerializeField] GameObject TurnThisOff2;

    public void OnPointerDown(PointerEventData eventData)
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        TurnThisOn.SetActive(true);
        TurnThisOff.SetActive(false);
        TurnThisOn1.SetActive(true);
        TurnThisOff2.SetActive(false);
    }
}