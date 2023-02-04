using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    public bool canPickup = false;
    public bool canEnemy = false;
    public bool canBoss = false;

    public float spawnCooldown;
    private float spawnCooldownCounter = 0;

    private bool registered = false;

    private bool currentSpawn = false;
    private GameObject spawned;

    private SpawnManager manager;

    private Transform trans;

    public Vector2 GetPosition()
    {
        return trans.position;
    }

    bool Register()
    {
        manager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        if (manager != null)
        {
            manager.Register(gameObject);
            return true;
        }
        else return false;
    }

    public void Spawn(GameObject entity)
    {
        spawned = Instantiate(entity, GetPosition(), Quaternion.identity);
        currentSpawn = true;
    }

    void Start()
    {
        trans = GetComponent<Transform>();
    }

    private void Update()
    {
        spawnCooldownCounter -= Time.deltaTime;
        if (!registered)
        {
            registered = Register();
        }
        if (currentSpawn && spawned != null)
        {
            spawnCooldownCounter = spawnCooldown;
        }
        if (spawned == null && spawnCooldownCounter <= 0)
        {
            currentSpawn = false;
        }
    }

    public bool ValidBoss(GameObject entity)
    {
        if (!currentSpawn && canBoss)
        {
            Spawn(entity);
            return true;
        }
        else return false;
    }

    public bool ValidEnemy(GameObject entity)
    {
        if (!currentSpawn && canEnemy)
        {
            Spawn(entity);
            return true;
        }
        else return false;
    }

    public bool ValidPickup(GameObject entity)
    {
        if (!currentSpawn && canPickup)
        {
            Spawn(entity);
            return true;
        }
        else return false;
    }
}
