using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float forceAmount = 10f;
    public float maxSpeed = 8f;
    public bool disableGlove = false;
    public GameObject gameOverManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Objective"))
        {
            // If we collide with the objective, trigger a game over screen with no bonus points
            gameOverManager.GetComponent<GameOverScript>().EndGameRegular();

        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            // If we collide with the goal, trigger a game over screen with bonus points equal
            // to the velocity of the ball
            gameOverManager.GetComponent<GameOverScript>().EndGameGoal(rb.linearVelocity.magnitude);
        }
    }
}
