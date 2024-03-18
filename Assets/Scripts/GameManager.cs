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
    public int manualHighscore = 0;
    //public TMP_Text scoreText;
    public GameObject newHighscoreText;
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject pausePanel;
    public GameObject inGameButtons;

    private GameObject[] highscoreTexts;
    private GameObject[] scoreTexts;
    private bool newHS = false;

    private void getHighscore()
    {
        if(manualHighscore != 0)
        {
            PlayerPrefs.SetInt("Highscore", manualHighscore);
        }
        highscore = PlayerPrefs.GetInt("Highscore");
        print("playerprefs is:" + highscore.ToString());
        writeHighscore();
    }

    private void writeHighscore()
    {
        print("hi");
        foreach (GameObject hsObject in highscoreTexts)
        {
            print("hello");
            TextMeshProUGUI hsTMP = hsObject.GetComponent<TextMeshProUGUI>();
            print(hsTMP.text);
            print(highscore);
            hsTMP.text = "Highscore: " + highscore.ToString();
            print(hsTMP.text);
        }
    }
    
    /*
    private void writeHighscore(GameObject parentObj)
    {
        Transform parentTransform = parentObj.transform.FindWithTag("Highscore")
    }
    */
    // Start is called before the first frame update
    void Start()
    {

        highscoreTexts = GameObject.FindGameObjectsWithTag("Highscore");
        scoreTexts = GameObject.FindGameObjectsWithTag("Score");
        getHighscore();
        score = 0;
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
        if(newHS)
        {
            newHighscoreText.SetActive(true);
        }
        
    }

    public void Lose()
    {
        Time.timeScale = 0;
        inGameButtons.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(1);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void IncreaseScore(int addedPoints)
    {
        score = score + addedPoints;
        print("score increased");

        foreach (GameObject scoreObject in scoreTexts)
        {
            TextMeshProUGUI scoreTMP = scoreObject.GetComponent<TextMeshProUGUI>();
            scoreTMP.text = "Score: " + score.ToString();
        }

        if (score > highscore)
        {
            newHS = true;
            PlayerPrefs.SetInt("Highscore", score);
            writeHighscore();
        }
    }
}
