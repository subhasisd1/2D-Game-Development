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
        FindObjectOfType<ButtonAudioController>().Play(SoundType.StartButton);

       // StartCoroutine(StartNewGame())
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

        Debug.Log(gameLevels);

        gameMainMenu.SetActive(false);
        gameLevels.SetActive(true);
        FindObjectOfType<ButtonAudioController>().Play(SoundType.ButtonClick);

    }

    public void GotoMenu()
    {
        
        FindObjectOfType<ButtonAudioController>().Play(SoundType.BackButton);
        gameMainMenu.SetActive(true);
        gameLevels.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameMainMenu.SetActive(true);
            gameLevels.SetActive(false);
            FindObjectOfType<ButtonAudioController>().Play(SoundType.ButtonClick);
        }
    }


}
