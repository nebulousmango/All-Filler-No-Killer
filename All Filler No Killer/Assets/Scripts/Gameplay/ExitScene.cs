using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour, IPointerDownHandler
{
    // Attached to Selector objects in 02_Cutscene1.

    [SerializeField] GameObject TurnThisOn;
    [SerializeField] GameObject TurnThisOff;
    [SerializeField] private string S_nextScene;

    public void OnPointerDown(PointerEventData eventData)
    {
        TurnThisOn.SetActive(true);
        TurnThisOff.SetActive(false);
        StartCoroutine(NextScene());
    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(S_nextScene);
    }
}