using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    Transform target;
    Transform trans;

    public GameObject bullet;

    public float bulletCooldown;
    private float bulletCooldownCounter = 0;
    public float bulletRange;
    public float bulletDestroyDelay;
    public float bulletSpeed;

    bool findTarget()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        return !(target == null);
    }

    Vector2 getTargetAngle()
    {
        return (target.position - trans.position).normalized;
    }

    float getTargetDistance()
    {
        return (target.position - trans.position).magnitude;
    }

    bool hasTarget()
    {
        if (target == null) return findTarget();
        else return true;
    }

    void ShootBullet(Vector2 angle)
    {
        bulletCooldownCounter = bulletCooldown;
        GameObject newBullet = Instantiate(bullet, trans.position, Quaternion.identity);
        Transform newBulletTransform = newBullet.GetComponent<Transform>();
        newBulletTransform.Translate(new Vector3(0, 0, -2));
        newBulletTransform.right = angle;
        Rigidbody2D newBulletRigidbody = newBullet.GetComponent<Rigidbody2D>();
        newBulletRigidbody.velocity = newBulletTransform.right * bulletSpeed;
        Destroy(newBullet, bulletDestroyDelay);
    }

    private void Start()
    {
        trans = GetComponent<Transform>();
    }

    void Update()
    {
        bulletCooldownCounter -= Time.deltaTime;
        if (hasTarget() && bulletCooldownCounter < 0 && getTargetDistance() < bulletRange) ShootBullet(getTargetAngle());
    }
}
