using UnityEngine;

public class PaddleOpp : MonoBehaviour
{
    // Attached to Player_Opp object in Level scenes.

    [SerializeField] GameObject ball;
    [SerializeField] public float speed;
    [SerializeField] float lerpSpeed = 1f;
    [SerializeField] Rigidbody2D rb;

    public Vector3 startPosition;
    public bool isMoving = false;
    public float timeMoving = 0f;
    public Vector3 direction = Vector3.zero;

    private string playBallSfx = "Tennis_4";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (ball.transform.position.y > transform.position.y)
        {
            if (rb.velocity.y < 0) rb.velocity = Vector2.zero;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.up * speed, lerpSpeed * Time.deltaTime);
        }
        else if (ball.transform.position.y < transform.position.y)
        {
            if (rb.velocity.y > 0) rb.velocity = Vector2.zero;
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.down * speed, lerpSpeed * Time.deltaTime);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero * speed, lerpSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            FindObjectOfType<AudioManager>().PlaySound(playBallSfx);
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}