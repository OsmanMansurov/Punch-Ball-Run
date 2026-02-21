using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    // Starting location
    public Vector3 startingLocation; // used for restarting the game

    // Particle effects
    public ParticleSystem wallParticles;
    public ParticleSystem spikeParticles;

    // Speed zone variables
    public int framesBetweenTriggers = 5; // how often the speed zone creates a force
    private int frameCounter = 0;
    private bool isCollidingWithTrigger = false;
    private TrailRenderer trailRenderer;
    private int counter = 0;

    // Other variables
    public Rigidbody2D rb;
    public GameObject gameOverManager;
    public float forceAmount = 10f;
    public float maxSpeed = 8f;
    public float maxSpeedZone = 12f; // Max speed when in a speed zone
    public float maximumSpeedBoost = 11.5f; // The maximum speed when we hit the ball while it's already at max speed
    public bool disableGlove = false;

    private bool hitMaxSpeed = false;
    private bool goingMaxSpeed = false;
    
    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // If we are in a speed zone
        counter++;
        if (isCollidingWithTrigger == true)
        {
            hitMaxSpeed = false;
            goingMaxSpeed = false;
            // If we are in a fast speed zone, make a green trail renderer
            if (maxSpeedZone == 20f) 
            {
                trailRenderer.startColor = Color.green;
                trailRenderer.endColor = Color.green;
            }
            else 
            {
                trailRenderer.startColor = Color.blue;
                trailRenderer.endColor = Color.blue;
            }
            // Set the maximum speed to a higher value than normal
            if (rb.linearVelocity.magnitude > maxSpeedZone)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeedZone;
            }
        }
        // Regular max speed
        else if (rb.linearVelocity.magnitude > maxSpeed)
        {
            if (hitMaxSpeed)
            {
                goingMaxSpeed = true;
                trailRenderer.startColor = Color.red;
                trailRenderer.endColor = Color.red;
                rb.linearVelocity = rb.linearVelocity.normalized * maximumSpeedBoost;
            }
            else
            {
                // If we just hit max speed, set a flag indicating that we should be able to increase our speed
                hitMaxSpeed = true;
                trailRenderer.startColor = Color.white;
                trailRenderer.endColor = Color.white;
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
        }
        else if (rb.linearVelocity.magnitude < maximumSpeedBoost && goingMaxSpeed == true)
        {
            // If we are not going beyond max speed, we shouldn't be able to go past the limit
            goingMaxSpeed = false;
            hitMaxSpeed = false;
            trailRenderer.startColor = Color.white;
            trailRenderer.endColor = Color.white;
        }
        else if (rb.linearVelocity.magnitude < maxSpeed)
        {
            hitMaxSpeed = false;
            goingMaxSpeed = false;
        }
    }

    public void RestartBooleans()
    {
        Debug.Log("wahoo!");
        disableGlove = false;
        hitMaxSpeed = false;
        goingMaxSpeed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Goal"))
        {
            // If we collide with the goal, trigger a game over screen with bonus points equal
            // to the velocity of the ball
            gameOverManager.GetComponent<GameOverScript>().EndGameGoal((rb.linearVelocity.magnitude / maxSpeed) * 100);
        }

        if (collision.gameObject.CompareTag("Speed Zone"))
        {
            // Reset all previous forces
            rb.linearVelocity = Vector2.zero;
            isCollidingWithTrigger = true;
            maxSpeedZone = 12f;
        }

        if (collision.gameObject.CompareTag("Fast Speed Zone"))
        {
            // Reset all previous forces
            rb.linearVelocity = Vector2.zero;
            isCollidingWithTrigger = true;
            // Max speed is higher for fast speed zone
            maxSpeedZone = 20f;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        frameCounter++;
        if (collision.gameObject.CompareTag("Speed Zone") || 
            collision.gameObject.CompareTag("Fast Speed Zone"))
        {
            isCollidingWithTrigger = true;
            // Remove all previous forces on the rigidbody
            if (frameCounter % framesBetweenTriggers == 0)
            {
                // Get the direction the speed zone is facing
                if (collision.gameObject.transform.eulerAngles.z == 0) 
                {
                    rb.AddForce(Vector2.up * 3f, ForceMode2D.Impulse);
                }
                else if (collision.gameObject.transform.eulerAngles.z == 90) 
                {
                    rb.AddForce(Vector2.left * 3f, ForceMode2D.Impulse);
                }
                else if (collision.gameObject.transform.eulerAngles.z == 180) 
                {
                    rb.AddForce(Vector2.down * 3f, ForceMode2D.Impulse);
                }
                else if (collision.gameObject.transform.eulerAngles.z == 270) 
                {
                    rb.AddForce(Vector2.right * 3f, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Speed Zone") || 
            collision.gameObject.CompareTag("Fast Speed Zone"))
        {
            isCollidingWithTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            // If we hit a wall, play the wall collision particle effect
            // The below line of code gets the precise point of the collision
            wallParticles.transform.position = collision.contacts[0].point;
            wallParticles.Play();
            RestartBooleans();
        }

        if (collision.gameObject.CompareTag("Danger"))
        {
            // If we hit a wall, play the wall collision particle effect
            // The below line of code gets the precise point of the collision
            spikeParticles.transform.position = collision.contacts[0].point;
            spikeParticles.Play();
            // Hit the game over screen
            gameOverManager.GetComponent<GameOverScript>().LoseGame();
            RestartBooleans();
        }

        if (collision.gameObject.CompareTag("Rock"))
        {
            RestartBooleans();
        }
    }
}
