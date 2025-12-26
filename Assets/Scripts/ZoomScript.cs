using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScript : MonoBehaviour
{
    // Start is called before the first frame update
    // TODO: I don't like the zoom system turning off scripts very much
    // This could lead to some problems in the future
    public int targetSize;
    public float zoomSpeed = 5f;
    public GameObject glove;
    public GameObject gloveButton;
    public bool isZoomed = false;
    void Start()
    {
        Camera.main.orthographicSize = 5f;
        targetSize = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleZoom();
        }
    }

    public void ToggleZoom()
    {
        if (Camera.main.orthographicSize == 5f)
        {
            // If the player wishes to zoom the camera, disable the glove script,
            // glove ui script, and the glove UI
            isZoomed = true;
            glove.GetComponent<GloveScript>().enabled = false;
            gloveButton.GetComponent<GloveUIScript>().DisableGloveUI();
            gloveButton.GetComponent<GloveUIScript>().enabled = false;
            Camera.main.orthographicSize = 10f;
        }
        else
        {
            // If the player wishes to return the camera to normal zoom, enable
            // the glove script, glove ui script, and the glove UI
            isZoomed = false;
            glove.GetComponent<GloveScript>().enabled = true;

            gloveButton.GetComponent<GloveUIScript>().enabled = true;

            // Make sure the glove is off-cooldown before it reappears
            // Also make sure that the UI doesn't reappear if the glove is already active
            if ((glove.GetComponent<GloveScript>().elapsed >=
                glove.GetComponent<GloveScript>().cooldown ||
                glove.GetComponent<GloveScript>().elapsed == 0f) &&
                glove.GetComponent<GloveScript>().isActive == false)
            {
                gloveButton.GetComponent<GloveUIScript>().ActivateGloveUI();
            }
            Camera.main.orthographicSize = 5f;
        }
    }
}
