using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image stamina;
    public AudioSource gloveReady;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
