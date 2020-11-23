using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Sprite[] lives;
    public GameObject titleScreen;
    public Image livesImageDisplay;

    public Text scoreText, bestText;
    public int score, bestScore;

    private GameManager _gm;

    public void Start()
    {
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
        bestText.text = "Best: " + bestScore;
        _gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void UpdateLives(int currentLives)
    {

        livesImageDisplay.sprite = lives[currentLives];
    }

    public void UpdateScore()
    {
        score += 10;
        scoreText.text = "Score: " + score;
    }

    public void CheckForBestScore()
    {
        if (score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("HighScore", bestScore);
            bestText.text = "Best: " + bestScore;
        }
    }

    public void ShowTitleScreen()
    {
        titleScreen.SetActive(true);
        score = 0; //resets score
    }

    public void HideTitleScreen()
    {
        titleScreen.SetActive(false);
        scoreText.text = "Score: 0";
    }

    public void PausePlay()
    {
        _gm.PauseGame();
    }

    public void ResumePlay()
    {
        _gm.ResumeGame();
    }

    //not used
    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        //solves bug! added myself
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}