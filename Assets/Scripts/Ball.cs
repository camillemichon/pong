using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int initspeed;
    private Rigidbody rb;
    private Vector3 velocity;
    private Vector3 position;
    private int balldir = 1;
    public GameObject score;
    
    public static bool go = true;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        position = transform.position;
        ResetBall();
    }

    private void Update()
    {
        velocity = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("TOUCH"))
        {
            BallTouchWall(collision.gameObject.GetComponent<Player>().player);
            ResetBall();
        }
        else if (collision.gameObject.CompareTag("paddle"))

        {
            BallTouchPaddle();
        }
            
            
            
        Vector3 direction = Vector3.Reflect(velocity.normalized, collision.contacts[0].normal); //rebonds
        rb.velocity = direction * velocity.magnitude;
        
        
    }

    void BallTouchPaddle()
    {
        velocity *= 1.1f;
    }

    void BallTouchWall(int player)
    {
        if (player == 0)
        {
            balldir = 1;
        }
        else
        {
            balldir = -1;
        }
        score.GetComponent<Score>().WinAPoint(player);
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
    }

    private void BallStart()
    {
        rb.AddForce(new Vector3(2,0,2)*initspeed * balldir);    
    }
}
