using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Attached to Canvas object in 04_LevelSelect scene.

    [SerializeField] private string[] LevelNames;

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
        SceneManager.LoadScene(LevelNames[0]);
    }

    public void LoadLevelB()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelNames[1]);
    }

    public void LoadLevelC()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelNames[2]);
    }
    public void LoadLevelD()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelNames[3]);
    }

    public void LoadLevelE()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelNames[4]);
    }

    public void LoadLevelF()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelNames[5]);
    }

    public void LoadLevelG()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelNames[6]);
    }

    public void LoadLevelH()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelNames[7]);
    }

    public void LoadLevelI()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelNames[8]);
    }

    public void LoadLevelJ()
    {
        FindObjectOfType<AudioManager>().PlaySound("Button");
        Time.timeScale = 1;
        SceneManager.LoadScene(LevelNames[9]);
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