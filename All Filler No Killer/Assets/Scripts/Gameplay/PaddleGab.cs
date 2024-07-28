using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleGab : MonoBehaviour
{
    // Attached to Player_Gab object in Level scenes.

    [SerializeField] float ExposedSpeed = 7;
    [SerializeField] Rigidbody2D GabRigidbody;
    public float speed;
    public Vector3 startPosition;

    private string playBallSfx = "Tennis_Gab";
    private float movement;
    Ball Ball;
    PaddleOpp PaddleOpp;

    private void Start()
    {
        speed = ExposedSpeed;
        startPosition = transform.position;
        Ball = FindObjectOfType<Ball>();
        PaddleOpp = FindObjectOfType<PaddleOpp>();
    }

    private void Update()
    {
        movement = Input.GetAxisRaw("VerticalGab");
        GabRigidbody.velocity = new Vector2(GabRigidbody.velocity.x, movement * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            FindObjectOfType<AudioManager>().PlaySound(playBallSfx);
        }
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Ball"))
    //    {
    //        Rigidbody2D ball = collision.rigidbody;
    //        Collider2D paddle = collision.otherCollider;

    //        // Gather information about the collision
    //        Vector2 ballDirection = ball.velocity.normalized;
    //        Vector2 contactDistance = ball.transform.position - paddle.bounds.center;
    //        Vector2 surfaceNormal = collision.GetContact(0).normal;
    //        Vector3 rotationAxis = Vector3.Cross(Vector3.up, surfaceNormal);

    //        // Rotate the direction of the ball based on the contact distance
    //        // to make the gameplay more dynamic and interesting
    //        float maxBounceAngle = 1f;
    //        float bounceAngle = (contactDistance.y / paddle.bounds.size.y) * maxBounceAngle;
    //        ballDirection = Quaternion.AngleAxis(bounceAngle, rotationAxis) * ballDirection;

    //        // Re-apply the new direction to the ball
    //        ball.velocity = ballDirection * ball.velocity.magnitude;
    //    }
    //}

    public void Reset()
    {
        GabRigidbody.velocity = Vector2.zero;
        speed = ExposedSpeed;
        PaddleOpp.speed = 8;
    }
}
