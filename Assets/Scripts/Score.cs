using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    private AudioSource audioSource;
    public TextMeshProUGUI text;
    public TextMeshProUGUI winText;

    private int scorep1;

    private int scorep2;

    public int maxpoints;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void WinAPoint(int player, int scoreAdded = 1)
    {
        if (player == 0)
        {
            scorep1 += scoreAdded;
            if (scorep1 >= maxpoints)
            {
                audioSource.Play();
                winText.color = Color.red;
                winText.text = "Game Over,\nLeft Paddle Wins!";
                winText.gameObject.SetActive(true);
                Debug.Log("Game Over, Left Paddle Wins!");
                Ball.go = false;
                ResetScore();
            }
        }
        else
        {
            scorep2 += scoreAdded;
            if (scorep2 >= maxpoints)
            {
                audioSource.Play();
                winText.color = Color.blue;
                winText.text = "Game Over,\nRight Paddle Wins!";
                winText.gameObject.SetActive(true);
                Debug.Log("Game Over, Right Paddle Wins!");
                Ball.go = false;
                ResetScore();
            }
        }

        int diff = Math.Abs(scorep1 * scorep2);
        if (diff >= 2)
        {
            if (scorep1 > scorep2)
            {
                Paddles.modifierLeft = 0.8f;
                Paddles.modifierRight = 1.2f;
            }

            else
            {
                Paddles.modifierLeft = 1.2f;
                Paddles.modifierRight = 0.8f;
            }
        }

        else
        {
            Paddles.modifierLeft = 1f;
            Paddles.modifierRight = 1;
        }

        text.text = $"{scorep1} | {scorep2}";
        StartCoroutine(ChangeColor());
        Debug.Log(text.text);
    }

    IEnumerator ChangeColor()
    {
        text.color = Color.red;
        yield return new WaitForSeconds(1);
        text.color = Color.black;
    }

    private void ResetScore()
    {
        scorep1 = 0;
        scorep2 = 0;
    }
}
