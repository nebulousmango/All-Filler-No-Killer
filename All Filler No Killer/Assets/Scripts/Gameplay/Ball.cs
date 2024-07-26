using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Attached to Ball object in Level scenes.

    [SerializeField] public float initialSpeed;
    [SerializeField] public Vector2 currentSpeed;
    [SerializeField] public float speedIncrease = 0.25f;
    [SerializeField] Rigidbody2D rb;
    public Vector3 startPosition;
    private int hitCounter;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, initialSpeed + (speedIncrease * hitCounter));
        currentSpeed = rb.velocity;
    }

    public void Launch()
    {
        initialSpeed = 5;
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        hitCounter = 0;
        rb.velocity = new Vector2(-1, 0) * (initialSpeed + (speedIncrease * hitCounter));
    }

    void PlayerBounce(Transform myObject)
    {
        hitCounter++;
        Vector2 ballPos = transform.position;
        Vector2 playerPos = myObject.position;

        float xDirection, yDirection;
        if(transform.position.x>0)
        {
            xDirection = -1;
        }
        else
        {
            xDirection = 1;
        }
        yDirection = (ballPos.y - playerPos.y) / myObject.GetComponent<Collider2D>().bounds.size.y;
        if (yDirection>=0 && yDirection<1)
        {
            yDirection = (Random.Range(0.4f, 0.6f));
        }
        rb.velocity = new Vector2(xDirection, yDirection) * (initialSpeed + (speedIncrease * hitCounter));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            PlayerBounce(collision.transform);
        }
    }

    public void OnHold()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
