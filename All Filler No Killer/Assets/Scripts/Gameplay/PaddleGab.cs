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

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}
