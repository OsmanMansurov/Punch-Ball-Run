using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GloveScript : MonoBehaviour
{
    public float moveSpeed = 5f; // Lower value for better control
    public Rigidbody2D rb;
    public float cooldown = 3f;
    public Transform target; // The glove will point in the direction of this target (the ball)
    public ParticleSystem particles;
    private int originalLayer;
    private int noCollisionLayer;
    private Vector2 previousPosition; // Used to calculate velocity
    public Vector2 currentVelocity;
    public AudioSource impactEffect; // Audio for collision sound effect
    public float elapsed; // elapsed time for glove cooldown
    public GameObject UIManager;
    public GameObject GloveUI;
    public GameObject zoomButton;
    public bool isActive = true;

    private Vector2 cachedMouseWorldPos;
    private Vector2 targetPosition;

    void Start()
    {
        originalLayer = gameObject.layer;
        noCollisionLayer = LayerMask.NameToLayer("NoCollision");
        DisableGlove();
    }

    void Update()
    {
        // Calculate the velocity of the glove
        Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
        currentVelocity = (currentPosition - previousPosition) / Time.deltaTime;
        previousPosition = currentPosition;
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 0f;
        cachedMouseWorldPos = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    void FixedUpdate()
    {
        if (target != null)
        {
            // Point the glove in the direction of the ball
            Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90);
        }

        // Move towards the mouse's position
        targetPosition = cachedMouseWorldPos;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            impactEffect.Play();
            Vector2 pushDir = (collision.transform.position - transform.position).normalized;
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                ballRb.AddForce(pushDir * currentVelocity.magnitude, ForceMode2D.Impulse);
                gameObject.layer = noCollisionLayer;
                StartCoroutine(ResetPushCooldown());
            }
            // Play the particle effect
            particles.transform.position = collision.transform.position;
            particles.Play();
        }
    }
    private IEnumerator ResetPushCooldown()
    {
        elapsed = 0f;
        while (elapsed < cooldown)
        {
            DisableGlove();
            // Elapse time for the cooldown
            elapsed += Time.deltaTime;
            // Calculate the glove's progress from 0 to 1
            float progress = Mathf.Clamp01(elapsed / cooldown);
            // Update the stamina wheel
            UIManager.GetComponent<UIManager>().UpdateStamina(progress);
            yield return null; // wait one frame
        }
        // This yield statement prevents the glove from reactivating
        // while the player is zoomed out
        // TODO: see ZoomScript
        yield return new WaitUntil(() => !zoomButton.GetComponent<ZoomScript>().isZoomed);
        GloveUI.GetComponent<GloveUIScript>().ActivateGloveUI();
    }

    public void ActivateGlove()
    {
        // Disable the glove reactivation button
        isActive = true;
        GloveUI.GetComponent<GloveUIScript>().DisableGloveUI();
        // Make it so the button works again
        gameObject.layer = originalLayer;
        GetComponent<SpriteRenderer>().enabled = true;
    }

    private void DisableGlove()
    {
        // Make sure that the glove can't be controlled
        isActive = false;
        GetComponent<SpriteRenderer>().enabled = false;
        gameObject.layer = noCollisionLayer;
    }
}
