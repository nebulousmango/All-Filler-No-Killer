using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayerOpp;
    public float speed;
    public Rigidbody2D rb;
    public Vector3 startPosition;

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

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }

}
