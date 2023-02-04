using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float bulletSpeed;
    public float bulletCooldown;
    private float bulletCooldownCounter;
    public float bulletDestroyDelay;
    public float reloadInitialTime;
    public float reloadCooldown;
    public float reloadCooldownCounter = 0;

    public int startingBullets;
    public int maxLoaded;
    private int currentBullets;
    private int currentLoaded;

    private bool canShoot = true;
    private bool isReloading = false;

    private Rigidbody2D rb;
    private Camera cam;
    private SpriteRenderer rend;
    private Transform trans;
    private PlayerHealth healthManager;

    public Sprite[] directionalSprite;

    public GameObject bullet;

    public void InstantLoad(int amount)
    {
        if (currentBullets >= amount)
        {
            currentBullets -= amount;
            currentLoaded += amount;
        }
        else
        {
            currentLoaded = currentBullets;
            currentBullets = 0;
        }
        if (currentLoaded > maxLoaded)
        {
            currentBullets += currentLoaded - maxLoaded;
            currentLoaded = maxLoaded;
        }
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PickupHealth")
        {
            if (!healthManager.FullHealth())
            {
                healthManager.Heal(1);
                Destroy(collider.gameObject);
            }
        }
    }

    void Reload()
    {
        if (currentBullets >= 1 && currentLoaded < maxLoaded)
        {
            currentBullets--;
            currentLoaded++;
        }
        if (currentBullets == 0 || currentLoaded == maxLoaded)
        {
            isReloading = false;
        }
    }

    void ShootBullet(Vector2 angle)
    {
        canShoot = false;
        bulletCooldownCounter = bulletCooldown;
        GameObject newBullet = Instantiate(bullet, trans.position, Quaternion.identity);
        Transform newBulletTransform = newBullet.GetComponent<Transform>();
        newBulletTransform.right = angle;
        Rigidbody2D newBulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
        newBulletRigidbody.velocity = newBulletTransform.right * bulletSpeed;
        Destroy(newBullet, bulletDestroyDelay);
    }
    
    void Start()
    {
        currentBullets = startingBullets;
        InstantLoad(maxLoaded);
        rb = GetComponent<Rigidbody2D>();
        cam = GetComponentInChildren<Camera>();
        rend = GetComponentInChildren<SpriteRenderer>();
        trans = GetComponent<Transform>();
        healthManager = GetComponent<PlayerHealth>();
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

        // Reduce Bullet Cooldown
        bulletCooldownCounter -= Time.deltaTime;
        if (bulletCooldownCounter < 0) canShoot = true;
        if (isReloading) reloadCooldownCounter -= Time.deltaTime;

        // Shoot Bullets
        if (Input.GetMouseButtonDown(0) && canShoot && currentLoaded >= 1)
        {
            ShootBullet(dir);
            isReloading = false;
        }

        // Reloading
        if (Input.GetKey(KeyCode.R) && currentLoaded < maxLoaded)
        {
            isReloading = true;
        }
        if (isReloading)
        {
            if (reloadCooldownCounter <= 0)
            {
                reloadCooldownCounter = reloadCooldown;
                Reload();
            }
        }
        else reloadCooldownCounter = reloadInitialTime;
    }
}
