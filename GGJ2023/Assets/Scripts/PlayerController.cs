using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public Sprite[] directionalSprite;
    public Camera cam;
    public SpriteRenderer rend;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();
        rend = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        // Movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float tempSpeed = speed;
        if (Mathf.Sqrt(Mathf.Pow(horizontalInput, 2) + Mathf.Pow(verticalInput, 2)) > 1)
        {
            tempSpeed = speed / Mathf.Sqrt(2);
        }
        rb.velocity = new Vector2(horizontalInput * tempSpeed, verticalInput * tempSpeed);

        // Determine Direction
        Vector2 dir = Input.mousePosition - cam.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (angle < 0) angle += 360;
        float angleIncrement = 360 / (directionalSprite.Length * 2);
        int counter = 0;
        while (angle - angleIncrement * counter > 0)
        {
            counter++;
        }
        int directionalSpriteIndex = counter / 2;
        if (directionalSpriteIndex >= directionalSprite.Length) directionalSpriteIndex = 0;
        rend.sprite = directionalSprite[directionalSpriteIndex];
    }
}
