using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using UnityEditor.Experimental.GraphView;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private int score;
    private int seconds;

    private float movementX;
    private float movementY;

    public float speed = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject winTextObject;

    private Coroutine countDown;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        seconds = 90;
        SetScoreText();
        SetTimerText();
        winTextObject.SetActive(false);

        countDown = StartCoroutine(CountDown());
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    IEnumerator CountDown()
    {
        while(seconds > 0)
        {
            yield return new WaitForSeconds(1f);

            seconds = seconds - 1;

            SetTimerText();
        }
    }
    
    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString();

        if (score >= 7500)
        {
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
            StopCoroutine(countDown);
        }
    }

    void SetTimerText()
    {
        timerText.text = "Time: " + seconds.ToString();
        if (seconds <= 0)
        {
            Destroy(gameObject);

            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);

        CountDown();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            score = score + other.gameObject.GetComponent<Rotator>().scoreValue;
            SetScoreText();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
            StopCoroutine(countDown);

        }
    }
    
}
