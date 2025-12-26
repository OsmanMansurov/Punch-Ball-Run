using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject ball;
    public float moveSpeed = 5f;

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, ball.transform.position, moveSpeed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10f);
    }
}
