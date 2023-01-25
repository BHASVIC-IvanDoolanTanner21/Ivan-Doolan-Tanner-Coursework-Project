using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedController : MonoBehaviour
{
    public bool isPaused = false;

    public float mainSpeed = 20;
    public float timeAlive = 0;
    public double timeAliveDivision = 0.001;
    public int score = 0;
    private double e = 2.71828182845904523536028747135266249775724709369995957496696762772407663035354759;

    public Text scoreText;

    private void Update()
    {
        timeAlive += Time.deltaTime;
        score = (int)timeAlive * 100;
        scoreText.text = score.ToString();
        mainSpeed = (float)(80 * (1 / (1 + (Math.Pow(e, (-((timeAlive / 150) - 3)))))) + 16.206);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
