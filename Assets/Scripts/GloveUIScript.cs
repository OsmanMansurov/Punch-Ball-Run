using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GloveUIScript : MonoBehaviour
{
    public Button gloveButton;
    public Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        ActivateGloveUI();
    }

    public void ActivateGloveUI()
    {
        gloveButton.enabled = true;
        buttonImage.enabled = true;
    }

    public void DisableGloveUI()
    {
        gloveButton.enabled = false;
        buttonImage.enabled = false;
    }
}
