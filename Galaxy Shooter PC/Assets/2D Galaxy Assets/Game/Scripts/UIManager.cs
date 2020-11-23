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

    public Text scoreText;
    [SerializeField]
    private Text _bestText;
    public int score, bestScore;

    public void Start()
    {
        bestScore = PlayerPrefs.GetInt("HighScore", 0);
        if(_bestText != null)
        {
            _bestText.text = "Best: " + bestScore;
        }
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
            if (_bestText != null)
            {
                _bestText.text = "Best: " + bestScore;
            }
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

    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
        //solves bug! added myself
        Time.timeScale = 1;
    }
}