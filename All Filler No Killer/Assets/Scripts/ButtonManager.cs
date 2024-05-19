using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private string S_LevelA;
    [SerializeField] private string S_LevelB;
    [SerializeField] private string S_LevelC;
    [SerializeField] private string S_LevelD;

    public void LoadLevelA()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(S_LevelA);
    }

    public void LoadLevelB()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(S_LevelB);
    }

    public void LoadLevelC()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(S_LevelC);
    }
    public void LoadLevelD()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(S_LevelD);
    }

    public void RestartLevel()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        FindObjectOfType<PauseMenu>().ChangePauseBool();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Application.Quit();
    }
}