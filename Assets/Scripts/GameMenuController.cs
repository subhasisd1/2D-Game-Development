using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController : MonoBehaviour
{
    public GameObject gameMainMenu;
    public GameObject gameLevels;
    private int currentScene;

    // Start is called before the first frame update
    void Awake()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(currentScene + 1);
    }

    public void LoadGameLevel1()
    {
        gameMainMenu.SetActive(false);
        gameLevels.SetActive(false);
        SceneManager.LoadScene(0 + 1);

    }

    public void ShowLevels()
    {
        gameMainMenu.SetActive(false);
        gameLevels.SetActive(true);
    }

    public void GotoMenu()
    {
        gameMainMenu.SetActive(true);
        gameLevels.SetActive(false);
    }

}
