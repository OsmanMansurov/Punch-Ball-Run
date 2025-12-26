using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject playButton;
    public GameObject settingsButton;
    public GameObject backButton;
    public GameObject settingsBackground;
    public GameObject audioButton;
    public AudioSource buttonSound;
    public AudioSource menuTheme;
    public static bool musicOn;
    public Animator settingsAnim;

    void Start()
    {
        menuTheme.loop = true;
        musicOn = true;
        // menuTheme.Play();
        backButton.SetActive(false);
        audioButton.SetActive(false);
        settingsAnim.SetBool("down", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        buttonSound.Play();
        SceneManager.LoadScene("TutorialLevel");
    }

    public void OpenSettings()
    {
        // Start by hiding the play and settings buttons
        settingsAnim.SetBool("down", false);
        buttonSound.Play();
        playButton.SetActive(false);
        settingsButton.SetActive(false);
        backButton.SetActive(true);
        audioButton.SetActive(true);
    }

    public void GoBack()
    {
        // Trigger the animation to move the settings bar in reverse
        settingsAnim.SetBool("down", true);
        buttonSound.Play();
        playButton.SetActive(true);
        settingsButton.SetActive(true);
        backButton.SetActive(false);
        audioButton.SetActive(false);
    }

    public void ToggleMusic()
    {
        buttonSound.Play();
        if (menuTheme.isPlaying)
        {
            menuTheme.Pause();
            musicOn = false;
        }
        else
        {
            menuTheme.UnPause();
            musicOn = true;
        }
    }
}
