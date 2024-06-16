using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject Go_PauseMenuUI;
    [SerializeField] private GameObject Go_ControlsUI;
    [SerializeField] private GameObject Go_OverlayUI;
    static bool GameIsPaused = false;

    private void Start()
    {
        Cursor.visible = true;
        Go_PauseMenuUI.SetActive(false);
        Go_ControlsUI.SetActive(false);
        Go_OverlayUI.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Go_OverlayUI.SetActive(false);
        Time.timeScale = 0;
        GameIsPaused = true;
    }

    void Resume()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Go_PauseMenuUI.SetActive(false);
        Go_ControlsUI.SetActive(false);
        Go_OverlayUI.SetActive(true);
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
