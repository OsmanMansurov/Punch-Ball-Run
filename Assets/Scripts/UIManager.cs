using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image stamina;
    public AudioSource gloveReady;
    public AudioSource music;
    public AudioSource movingSpikeSound;

    // Ball and glove for restarting the game
    public GameObject ball;
    public GameObject glove;
    public GameObject movingSpike; // Might not exist in every scene

    void Start()
    {
        // music.Play();
    }

    void Update()
    {
        // If there are moving spikes in the scene
        if (movingSpike != null)
        {
            // Play the moving spike sound at the end of each animation cycle
            Animator anim = movingSpike.GetComponent<Animator>();
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Closing"))
            {
                movingSpikeSound.Play();
            }
        }
    }

    public void UpdateStamina(float progress)
    {
        // If the stamina bar is full, make sure it's green
        if (progress == 1f)
        {
            gloveReady.Play();
            stamina.color = new Color32(59, 192, 39, 255);
        }
        // Otherwise, turn it orange
        else
        {
            stamina.color = new Color32(234, 124, 17, 255);
        }
        stamina.fillAmount = progress;
    }

    public void RestartGame()
    {
        ball.SetActive(true);
        glove.SetActive(true);

        // Move the ball to its original starting position
        Vector3 ballStartingLocation = ball.GetComponent<BallScript>().startingLocation;
        ball.transform.position = ballStartingLocation;

        // Remove the game over screen
        GameObject gameOverManager = GameObject.FindGameObjectWithTag("GameOverManager");
        gameOverManager.GetComponent<GameOverScript>().RestartGame();

        // Reset the glove
        glove.GetComponent<GloveScript>().DisableGlove();
        GameObject gloveUI = GameObject.FindGameObjectWithTag("GloveUI");
        gloveUI.GetComponent<GloveUIScript>().ActivateGloveUI();
        
        // Reset the stamina bar without playing the gloveReady sound
        stamina.color = new Color32(59, 192, 39, 255);
        stamina.fillAmount = 1f;
    }
}