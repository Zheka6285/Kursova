using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Transform ScoreBot;

    public Text scoreText;

    void Update()
    {
        scoreText.text = ScoreBot.position.z.ToString("0");
    }

}
