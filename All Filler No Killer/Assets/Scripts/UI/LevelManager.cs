using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Attached to Canvas object in 04_LevelSelect scene.

    [SerializeField] private string LevelOne;
    [SerializeField] private string LevelTwo;
    [SerializeField] private string LevelThree;
    [SerializeField] private string LevelFour;
    [SerializeField] private string LevelFive;

    public void LoadStartMenu()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene("00_StartMenu");
    }

    public void LoadLevelA()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelOne);
    }

    public void LoadLevelB()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelTwo);
    }

    public void LoadLevelC()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelThree);
    }
    public void LoadLevelD()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelFour);
    }

    public void LoadLevelE()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelFive);
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