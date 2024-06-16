using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GabPaddle : MonoBehaviour
{
    [SerializeField] bool isPlayerOpp;
    [SerializeField] float speed;
    [SerializeField] Rigidbody2D rb;
    public Vector3 startPosition;

    [SerializeField] bool isPlayerMasc;
    [SerializeField] bool isPlayerFem;
    private static string[] voiceFemList = { "HmmFem1", "HmmFem2", "HmmFem3", "HmmFem4", "HmmFem5", "HmmFem6" };
    private static string[] voiceMascList = { "HmmMasc1", "HmmMasc2", "HmmMasc3", "HmmMasc4", "HmmMasc5", "HmmMasc6" };
    private string playFemVoice;
    private string playMascVoice;

    private float movement;

    private void Awake()
    {
        playFemVoice = voiceFemList[Random.Range(0, voiceFemList.Length)];
        playMascVoice = voiceMascList[Random.Range(0, voiceMascList.Length)];
    }

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
