using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public AudioSource buttonSound;
    public GameObject zoomButton;
    public GameObject staminaBar;
    public GameObject gloveButton;
    public string[] lines;
    public float textSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        StartDialogue();
        zoomButton.SetActive(false);
        staminaBar.SetActive(false);
        gloveButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    private void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            zoomButton.SetActive(true);
            staminaBar.SetActive(true);
            gloveButton.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void NextLineButton()
    {
        buttonSound.Play();
        if (textComponent.text == lines[index])
        {
            NextLine();
        }
        else
        {
            StopAllCoroutines();
            textComponent.text = lines[index];
        }
    }
}
