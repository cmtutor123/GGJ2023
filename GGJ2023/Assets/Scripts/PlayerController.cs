using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float bulletSpeed;
    public float bulletCooldown;

    private bool canShoot = true;

    private Rigidbody2D rb;
    private Camera cam;
    private SpriteRenderer rend;
    private Transform trans;

    public Sprite[] directionalSprite;
    
    public GameObject bullet;

    void ShootBullet(Vector2 angle)
    {
        GameObject newBullet = Instantiate(bullet, trans.position, Quaternion.identity);
        Transform newBulletTransform = newBullet.GetComponent<Transform>();
        newBulletTransform.right = angle;
        Rigidbody2D newBulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
        newBulletRigidbody.velocity = newBulletTransform.right * bulletSpeed;
    }
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();
        rend = GetComponentInChildren<SpriteRenderer>();
        trans = GetComponent<Transform>();
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

        // Calculate Directional Sprite
        float angleIncrement = 360 / (directionalSprite.Length * 2);
        int counter = 0;
        while (angle - angleIncrement * counter > 0)
        {
            counter++;
        }
        int directionalSpriteIndex = counter / 2;
        if (directionalSpriteIndex >= directionalSprite.Length) directionalSpriteIndex = 0;
        rend.sprite = directionalSprite[directionalSpriteIndex];

        // Shoot Bullets
        if (Input.GetKey(KeyCode.Space) && canShoot)
        {
            ShootBullet(dir);
        }
    }
}
