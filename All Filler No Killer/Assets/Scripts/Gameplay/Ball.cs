using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Attached to Ball object in Level scenes.

    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    public Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        Launch();
    }

    public void Reset()
    {
        rb.gravityScale = 0;
        speed = 4;
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        StartCoroutine(ResetBallCoroutine());
    }

    public void OnHold()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    private void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    private IEnumerator ResetBallCoroutine()
    {
        yield return new WaitForSeconds(1);
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(speed * x, speed * y);
    }

    public void BallVersionGood()
    {
        rb.gravityScale = 0;
        speed = 4;
    }

    public void BallVersionBad()
    {
        rb.gravityScale = 0.1f;
        speed = 4.6f;
    }

    public void BallVersionUgly()
    {
        rb.gravityScale = 0;
        speed = 4.5f;
    }
}
