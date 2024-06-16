using UnityEngine;

public class OppPaddle : MonoBehaviour
{
    [SerializeField] GameObject ball;
    [SerializeField] float speed;
    [SerializeField] float lerpSpeed = 1f;
    [SerializeField] Rigidbody2D rb;

    public Vector3 startPosition;
    public bool isMoving = false;
    public float timeMoving = 0f;
    public Vector3 direction = Vector3.zero;

    [SerializeField] bool isPlayerMasc;
    [SerializeField] bool isPlayerFem;
    private static string[] voiceFemList = { "HmmFem1", "HmmFem2", "HmmFem3", "HmmFem4", "HmmFem5", "HmmFem6" };
    private static string[] voiceMascList = { "HmmMasc1", "HmmMasc2", "HmmMasc3", "HmmMasc4", "HmmMasc5", "HmmMasc6" };
    private string playFemVoice;
    private string playMascVoice;

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
            if (isPlayerMasc)
            {
                FindObjectOfType<AudioManager>().PlaySound(playMascVoice);
                playMascVoice = voiceMascList[Random.Range(0, voiceMascList.Length)];
            }
            if (isPlayerFem)
            {
                FindObjectOfType<AudioManager>().PlaySound(playFemVoice);
                playFemVoice = voiceFemList[Random.Range(0, voiceFemList.Length)];
            }
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }
}