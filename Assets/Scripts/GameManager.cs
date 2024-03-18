using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public int score;
    public int highscore;
    //public TMP_Text scoreText;
    public GameObject newHighscoreText;
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject pausePanel;
    public GameObject inGameButtons;

    // Start is called before the first frame update
    void Start()
    {
        
        startPanel.SetActive(true);
        newHighscoreText.SetActive(false);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        inGameButtons.SetActive(false);
    }

    private void Awake()
    {
        Time.timeScale = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void gameStart()
    {
        Time.timeScale = 1;
        startPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        inGameButtons.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0;
        inGameButtons.SetActive(false);
        winPanel.SetActive(true);
        
    }

    public void Lose()
    {
        Time.timeScale = 0;
        inGameButtons.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
