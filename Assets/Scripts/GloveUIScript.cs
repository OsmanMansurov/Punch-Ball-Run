using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GloveUIScript : MonoBehaviour
{
    public Button gloveButton;
    public Image buttonImage;
    public GameObject zoomButton;
    // Start is called before the first frame update
    void Start()
    {
        ActivateGloveUI();
    }

    // Update is called once per frame
    void Update()
    {
        // Make sure the glove ui is always disabled when we are zoomed
        if (zoomButton.GetComponent<ZoomScript>().isZoomed)
        {
            DisableGloveUI();
        }
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
