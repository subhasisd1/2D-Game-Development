using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class ScoreController : MonoBehaviour
{
   
    private TextMeshProUGUI scroreText;
    public TextMeshProUGUI lelvelText;
    private string sceneName;
    private int score = 0;

    private void Start()
    {
        RefreshUI();
    }

    private void Awake()
    {
        sceneName = SceneManager.GetActiveScene().name;
        lelvelText.text = sceneName.ToString();
        scroreText = GetComponent<TextMeshProUGUI>();
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
