using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleGab : MonoBehaviour
{
    // Attached to Player_Gab object in Level scenes.

    [SerializeField] bool isPlayerOpp;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    public Vector3 startPosition;

    private string playBallSfx = "Tennis_5";
    private float movement;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if(isPlayerOpp)
        {
            movement = Input.GetAxisRaw("VerticalOpp");
        }
        else
        {
            movement = Input.GetAxisRaw("VerticalGab");
        }
        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
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
            float maxBounceAngle = 10f;
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
