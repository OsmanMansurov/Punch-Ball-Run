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

    // Sound effects
    public AudioSource victory;
    public AudioSource explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        gameOverBackground.SetActive(false);
        restartButton.SetActive(false);
        levelSelectButton.SetActive(false);
        pointsScoredText.text = "";
        gameOverText.text = "";
    }

    public void EndGameGoal(float velocity)
    {
        victory.Play();
        gameOverBackground.SetActive(true);
        levelSelectButton.SetActive(true);
        gameOverText.text = "Game Over!";
        pointsScoredText.text = "     Speed Points: " + Mathf.Round((velocity / 100) * 50).ToString();
        DisableGame();
    }

    public void LoseGame()
    {
        explosionSound.Play();
        gameOverBackground.SetActive(true);
        restartButton.SetActive(true);
        levelSelectButton.SetActive(true);
        // Move the restart and level select buttons to make the screen look cleaner
        // restartButton.transform.position += new Vector3(0, 255, 0);
        // levelSelectButton.transform.position += new Vector3(0, 155, 0);
        gameOverText.text = "Game Over!";
        DisableGame();
    }

    public void DisableGame()
    {
        ball.SetActive(false);
        glove.SetActive(false);
    }

    // This function is meant for removing the game over screen after the user presses restart
    public void RestartGame() 
    {
        // Put the buttons back to their original positions
        // Clear everything else
        gameOverBackground.SetActive(false);
        restartButton.SetActive(false);
        levelSelectButton.SetActive(false);
        pointsScoredText.text = "";
        gameOverText.text = "";
    }
}
