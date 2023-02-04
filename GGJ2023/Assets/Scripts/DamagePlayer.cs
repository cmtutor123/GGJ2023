using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    GameObject player;

    public int damage;

    public bool destroyOnDamage = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            if (destroyOnDamage) Destroy(gameObject);
        }
    }
}
