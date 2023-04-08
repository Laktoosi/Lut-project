using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject gameoverPanel;

    public TMP_Text scoreText;
    public TMP_Text LastScore;
    public Score score;

    

    public bool gameIsOver = false;

    public void Start()
    {

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Stage 1");
    }

    public void DisplayGameOverScreen()
    {
        gameoverPanel.SetActive(true);
        gameIsOver = true;
        LastScore.text = scoreText.text;
    }
}
