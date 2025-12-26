using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    public AudioSource buttonSound;

    public void LoadTutorialLevel()
    {
        // The tutorial level has an index of 2
        StartCoroutine(LoadLevel(2));
    }   

    IEnumerator LoadLevel(int levelIndex)
    {
        // Play the button sound
        buttonSound.Play();

        // Play animation
        transition.SetTrigger("start");
        
        // Wait for animation to stop playing
        yield return new WaitForSeconds(transitionTime);

        // Load scene
        SceneManager.LoadScene(levelIndex);
    }
}
