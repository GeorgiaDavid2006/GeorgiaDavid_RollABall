using System;
using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public GameObject player;

    public Button startButton;
    public Button continueButton;
    public Button howToPlayButton;
    public Button closeButton;

    public int score;
    public float seconds;

    public int maxScore;
    public int maxSeconds;

    public GameObject menuText;
    public GameObject Level;
    public GameObject scoreText;
    public GameObject timerText;
    public GameObject winTextObject;
    public GameObject tutorialTextObject;

    public bool isGameActive = false;

    public Coroutine countDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        seconds = maxSeconds;

        startButton = GetComponent<Button>();
        
        //countDown = StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTimer();

        if (player.transform.position.y < -15)
        {
            GameOver();
        }
    }

    private void CountDownTimer()
    {
        if(isGameActive == true)
        {
            seconds -= Time.deltaTime;

            SetTimerText();

            if (seconds <= 0)
            {
                GameOver();
            }
        }

    }


    public void SetScoreText()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();

        if (score >= maxScore)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            isGameActive = false;
        }
    }

    public void SetTimerText()
    {
        timerText.GetComponent<TextMeshProUGUI>().text = "Time: " + ((int)seconds).ToString();
    }

    public void StartGame()
    {
        menuText.gameObject.SetActive(false);
        Level.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);

        SetScoreText();
        SetTimerText();

        isGameActive = true;
    }

    public void NextLevel()
    {
        winTextObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DisplayTutorial()
    {
        menuText.SetActive(false);
        tutorialTextObject.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorialTextObject.SetActive(false);
        menuText.SetActive(true);
    }

    public void GameOver()
    {
        //StopCoroutine(countDown);


        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        continueButton.gameObject.SetActive(false);
        
        player.SetActive(false);

        isGameActive = false;
    }
}
