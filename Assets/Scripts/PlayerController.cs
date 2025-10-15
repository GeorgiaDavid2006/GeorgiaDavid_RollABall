using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public UIController uiController;

    private Rigidbody rb;
    
    private float movementX;
    private float movementY;

    public float speed = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            uiController.score = uiController.score + other.gameObject.GetComponent<Rotator>().scoreValue;
            uiController.SetScoreText();

            other.gameObject.SetActive(false);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);

            uiController.GameOver();
        }
    }
   
}
