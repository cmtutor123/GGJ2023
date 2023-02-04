using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Hit");
        if (collision.collider.tag == "Object") Destroy(GetComponent<SpriteRenderer>());
        else if (collision.collider.tag == "Enemy")
        {
            collision.collider.GetComponent<EnemyHealth>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}