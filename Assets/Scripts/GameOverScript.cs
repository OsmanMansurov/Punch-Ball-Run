using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverBackground;
    public GameObject restartButton;
    public GameObject levelSelectButton;
    public GameObject ball;
    public GameObject glove;
    public TMP_Text pointsScoredText;
    public TMP_Text gameOverText;
    public AudioSource music;
    public AudioSource victory;
    public AudioSource defeat;

    // Start is called before the first frame update
    void Start()
    {
        gameOverBackground.SetActive(false);
        restartButton.SetActive(false);
        levelSelectButton.SetActive(false);
        pointsScoredText.text = "";
        gameOverText.text = "";
        // Turn on the music
        music.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndGameGoal(float velocity)
    {
        victory.Play();
        gameOverBackground.SetActive(true);
        levelSelectButton.SetActive(true);
        gameOverText.text = "Game Over!";
        int randomNum = Random.Range(-7, 8);
        pointsScoredText.text = "           Points: " + Mathf.Round(velocity*8 + randomNum).ToString();
        DisableGame();
    }

    public void EndGameRegular()
    {
        victory.Play();
        gameOverBackground.SetActive(true);
        levelSelectButton.SetActive(true);
        gameOverText.text = "Game Over!";
        pointsScoredText.text = "     No bonus points!";
        DisableGame();
    }

    public void LoseGame()
    {
        defeat.Play();
        DisableGame();
    }

    public void DisableGame()
    {
        ball.SetActive(false);
        glove.SetActive(false);
    }
}
