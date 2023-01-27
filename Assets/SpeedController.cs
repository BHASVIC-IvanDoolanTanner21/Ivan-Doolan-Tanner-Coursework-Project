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
    //unitys built in e does not extend far enough to be practical so I have pasted in a more precise version
    private double e = 2.71828182845904523536028747135266249775724709369995957496696762772407663035354759;
    public bool gameStarted = false;
    public int highscore;
    public new Camera camera;
    public GameObject pauseButton, bigPauseButton, startButton, player, rotateButton, jumpButton;
    private bool firstIteration = true, firstIterationTwo = true;
    public bool cameraMoveUp = false, cameraMoveDown = false;

    public Text scoreText;

    private void Start()
    {
        Time.timeScale = 0f;
        gameStarted = false;
        Debug.Log("1");
        highscore = PlayerPrefs.GetInt("highscore", 0);
        camera.transform.position = new Vector3(0, -69, -10);
    }

    private void Update()
    {
        if (startButton.GetComponent<ButtonClickScript>().isPressed)
        {
            gameStarted = true;
            Debug.Log("2");
            startButton.transform.localPosition = new Vector2(0, -450);
            Time.timeScale = 1f;
            firstIteration = true;
        }
        if (player.GetComponent<Controller>().hasLost)
        {
            Time.timeScale = 0f;
            gameStarted = false;
            Debug.Log("3");
            PlayerPrefs.SetInt("highscore", highscore);
            PlayerPrefs.Save();
            if (firstIterationTwo)
            {
                startButton.transform.localPosition = new Vector2(0, -235);
                jumpButton.transform.localPosition = new Vector2(-81, -500);
                rotateButton.transform.localPosition = new Vector2(81, -500);
                cameraMoveDown = true;
                firstIterationTwo = false;
            }
            player.GetComponent<Controller>().hasLost = false;
            score = 0;
        }
        if(gameStarted)
        {
            if(firstIteration)
            {
                jumpButton.transform.localPosition = new Vector2(-81, -240);
                rotateButton.transform.localPosition = new Vector2(81, -240);
                cameraMoveUp = true;
                firstIteration = false;  
            }
            //this measures the ongoing time
            timeAlive += Time.deltaTime;
            score = (int)timeAlive * 100;

            if (pauseButton.GetComponent<ButtonClickScript>().isPressed)
            {
                isPaused = true;
                pauseButton.transform.localPosition = new Vector2(200, 322);
                bigPauseButton.transform.localPosition = new Vector2(0, 0);
                PauseGame();
            }
            if (bigPauseButton.GetComponent<ButtonClickScript>().isPressed)
            {
                isPaused = false;
                pauseButton.transform.localPosition = new Vector2(144, 322);
                bigPauseButton.transform.localPosition = new Vector2(500, 0);
                PauseGame();
            }
        }
        
        //this adds the ongoing score to the score sprite, so that it shows the current score
        scoreText.text = score.ToString();
        //ease in out equation for the speed of the game
        mainSpeed = (float)(80 * (1 / (1 + (Math.Pow(e, (-((timeAlive / 150) - 3)))))) + 16.206);
    }

    public void PauseGame()
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
