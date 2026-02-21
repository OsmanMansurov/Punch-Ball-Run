using UnityEngine;

public class Squish : MonoBehaviour
{
    public Transform Sprite; // this is an object containing the Ball's SpriteRenderer
    public float Stretch = 0.1f;
    [SerializeField] private Transform squishParent;
    [SerializeField] private Rigidbody2D rb;
    private Vector3 originalScale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originalScale = Sprite.transform.localScale;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
