using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI text;

    private int scorep1;

    private int scorep2;

    public int maxpoints;

    public void WinAPoint(int player)
    {
        if (player == 0)
        {
            scorep1 += 1;
            if (scorep1 == maxpoints)
            {
                Debug.Log("Game Over, Left Paddle Wins!");
                Ball.go = false;
                ResetScore();
            }
        }
        else
        {
            scorep2 += 1;
            if (scorep2 == maxpoints)
            {
                Debug.Log("Game Over, Right Paddle Wins!");
                Ball.go = false;
                ResetScore();
            }
        }

        text.text = $"{scorep1} | {scorep2}";
        Debug.Log(text.text);
    }

    public void ResetScore()
    {
        scorep1 = 0;
        scorep2 = 0;
    }
}
