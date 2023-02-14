using System;
using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int initspeed;
    
    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 position;
    private int balldir = 1;
    public GameObject score;
    
    public AudioClip wallsound;
    public AudioClip paddlesound;
    public AudioClip winsound;

    private AudioSource audioSource;
    
    public static bool go = true;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
        position = transform.position;  //send to the position 
        ResetBall(); 
    }

    private void Update()
    {
        velocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(CameraShake());
        if (collision.gameObject.CompareTag("TOUCH"))
        {
            int scoreAdded = 1;
            if (Math.Abs(rb.velocity.x) > 13)
                scoreAdded = 2;
            
            BallTouchWall(collision.gameObject.GetComponent<Player>().player, scoreAdded);
            ResetBall();
        }
        else if (collision.gameObject.CompareTag("paddle"))
        {
            BallTouchPaddle();
        }

        else
        {
            audioSource.clip = wallsound;
            audioSource.Play();
        }

        Vector3 direction = Vector3.Reflect(velocity.normalized, collision.contacts[0].normal); //rebonds
        rb.velocity = direction * velocity.magnitude;
    }

    void BallTouchPaddle()
    {
        velocity *= 1.1f;
        
        audioSource.clip = paddlesound;
        audioSource.pitch += 0.1f;
        audioSource.Play();
        
    }

    void BallTouchWall(int player, int scoreAdded)
    {
        score.GetComponent<Score>().WinAPoint(player, scoreAdded);
        if (player == 0)
        {
            balldir = 1;
        }
        else
        {
            balldir = -1;
        }
        
        audioSource.clip = winsound;
        audioSource.Play();
    }

    void ResetBall()
    {
        velocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = position;
        if (go)
        {
            BallStart();
        }

        audioSource.pitch = 1;
    }

    private void BallStart()
    {
        rb.AddForce(new Vector3(2,0,2)*initspeed * balldir);    
    }

    private IEnumerator CameraShake()
    {
        float time = 0.1f;
        var cameraPos = Camera.main.transform.position;
        while (time > 0)
        {
            Camera.main.transform.position = cameraPos + new Vector3(
                Mathf.PerlinNoise(0, Time.time * 20) * 2 - 1,
                Mathf.PerlinNoise(1, Time.time * 20) * 2 - 1,
                Mathf.PerlinNoise(2, Time.time * 20) * 2 - 1
            ) * 0.5f;
            time -= Time.deltaTime;
            yield return 0;
        }

        Camera.main.transform.position = cameraPos;

    }
}
