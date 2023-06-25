using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    public void pauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    
    public void restartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameplayScene");
    }

    public void mainMenu()
    {
        StaticConfiguration.staticIsReturningFromGameplay = true;
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1f;
    }
}
