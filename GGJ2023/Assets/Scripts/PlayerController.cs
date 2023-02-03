using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float tempSpeed = speed;
        if (Mathf.Sqrt(Mathf.Pow(horizontalInput, 2) + Mathf.Pow(verticalInput, 2)) > 1)
        {
            tempSpeed = speed / Mathf.Sqrt(2);
        }
        rb.velocity = new Vector2(horizontalInput * tempSpeed, verticalInput * tempSpeed);
    }
}
