using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ScoreController : MonoBehaviour
{
   
    private TextMeshProUGUI scroreText;
    public TextMeshProUGUI lelvelText;
    private int score = 0;

    private void Start()
    {
        RefreshUI();
    }

    private void Awake()
    {
        lelvelText.text = "Level " + SceneManager.GetActiveScene().buildIndex;
        scroreText = GetComponent<TextMeshProUGUI>();

      //  Debug.Log(scroreText);
    }

    public void IncreaseScore(int incrmnt)
    {
        score += incrmnt;

        RefreshUI();
    }
    private void RefreshUI()
    {
        scroreText.text = "Score: " + score;
    }
}
