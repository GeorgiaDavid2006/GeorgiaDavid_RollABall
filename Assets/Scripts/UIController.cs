using System;
using System.Collections;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject player;

    public Button startButton;

    public int score;
    public float seconds;

    public GameObject menuText;
    public GameObject Level;
    public GameObject scoreText;
    public GameObject timerText;
    public GameObject winTextObject;

    public bool isGameActive = false;

    public Coroutine countDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        seconds = 90;

        startButton = GetComponent<Button>();
        
        //countDown = StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        CountDownTimer();
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

        if (score >= 7500)
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

    public void GameOver()
    {
        //StopCoroutine(countDown);


        winTextObject.gameObject.SetActive(true);
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        
        player.SetActive(false);

        isGameActive = false;
    }
}
