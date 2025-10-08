using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public PlayerController player;

    public Button startButton;

    public int score;
    public float seconds;

    public GameObject menuText;
    public GameObject Level;
    public GameObject scoreText;
    public GameObject timerText;
    public GameObject winTextObject;

    private bool isGameActive = false;

    public Coroutine countDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        seconds = 90;

        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);

        winTextObject.gameObject.SetActive(false);

        countDown = StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator CountDown()
    {
        Debug.Log("Timer Start");
        while (seconds > 0)
        {
            yield return new WaitForSeconds(1f);

            seconds = seconds - 1;

            SetTimerText();
        }
    }

    public void SetScoreText()
    {
        scoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + score.ToString();

        if (score >= 7500)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            StopCoroutine(countDown);
        }
    }

    public void SetTimerText()
    {
        timerText.GetComponent<TextMeshProUGUI>().text = "Time: " + seconds.ToString();
        if (seconds <= 0)
        {
            Destroy(gameObject);

            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    public void StartGame()
    {
        isGameActive = true;

        menuText.gameObject.SetActive(false);
        Level.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        timerText.gameObject.SetActive(true);

        SetScoreText();
        SetTimerText();
    }
}
