using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject Go_PauseMenuUI;
    [SerializeField] private GameObject Go_ControlsUI;
    static bool GameIsPaused = false;

    private void Start()
    {
        Go_PauseMenuUI.SetActive(false);
        Go_ControlsUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Go_PauseMenuUI.SetActive(true);
        Go_ControlsUI.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    void Resume()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Go_PauseMenuUI.SetActive(false);
        Go_ControlsUI.SetActive(false);
        GameIsPaused = false;
        Time.timeScale = 1;
    }

    public void Controls()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Go_ControlsUI.SetActive(true);
    }

    public void ChangePauseBool()
    {
        GameIsPaused = false;
    }
}
