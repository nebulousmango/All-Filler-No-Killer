using UnityEngine;

public class PaddleOpp : MonoBehaviour
{
    // Attached to Player_Opp object in Level scenes.

    [SerializeField] GameObject ball;
    [SerializeField] public float speed = 10;
    [SerializeField] float lerpSpeed = 1f;
    [SerializeField] Rigidbody2D rb;

    public Vector3 startPosition;
    public bool isMoving = false;
    public float timeMoving = 0f;
    public Vector3 direction = Vector3.zero;

    private string playBallSfx = "Tennis_Opp";

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D ball = collision.rigidbody;
            Collider2D paddle = collision.otherCollider;

            // Gather information about the collision
            Vector2 ballDirection = ball.velocity.normalized;
            Vector2 contactDistance = ball.transform.position - paddle.bounds.center;
            Vector2 surfaceNormal = collision.GetContact(0).normal;
            Vector3 rotationAxis = Vector3.Cross(Vector3.up, surfaceNormal);

            // Rotate the direction of the ball based on the contact distance
            // to make the gameplay more dynamic and interesting
            float maxBounceAngle = 1f;
            float bounceAngle = (contactDistance.y / paddle.bounds.size.y) * maxBounceAngle;
            ballDirection = Quaternion.AngleAxis(bounceAngle, rotationAxis) * ballDirection;

            // Re-apply the new direction to the ball
            ball.velocity = ballDirection * ball.velocity.magnitude;
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}