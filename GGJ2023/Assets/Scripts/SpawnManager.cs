using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private List<GameObject> spawnLocations = new List<GameObject>();
    private int wave = 0;
    public int totalWaves;
    public List<int> countEnemyMelee;
    public List<int> countEnemyRanged;
    public List<int> countEnemyBoss;
    public List<int> countPickupAmmo;
    public List<int> countPickupHealth;

    public List<float> waveCooldowns;
    private float waveCooldownCounter;

    private List<int> spawnQueue;

    public GameObject enemyMelee;
    public GameObject enemyRanged;
    public GameObject enemyBoss;
    public GameObject pickupAmmo;
    public GameObject pickupHealth;

    void AddToQueue(List<int> counts, int entity)
    {
        for (int i = 0; i < counts[wave]; i++)
        {
            spawnQueue.Add(entity);
        }
    }

    void AttemptSpawn(int entity)
    {
        switch (entity)
        {
            case 0:
                if (!FindEnemySpawn(enemyMelee)) spawnQueue.Add(entity);
                break;
            case 1:
                if (!FindEnemySpawn(enemyRanged)) spawnQueue.Add(entity);
                break;
            case 2:
                if (!FindBossSpawn(enemyBoss)) spawnQueue.Add(entity);
                break;
            case 3:
                if (!FindPickupSpawn(pickupAmmo)) spawnQueue.Add(entity);
                break;
            case 4:
                if (!FindPickupSpawn(pickupHealth)) spawnQueue.Add(entity);
                break;
        }
    }

    bool FindBossSpawn(GameObject entity)
    {
        RandomizeLocations();
        foreach (GameObject location in spawnLocations)
        {
            if (location.GetComponent<SpawnLocation>().ValidBoss(entity)) return true;
        }
        return false;
    }

    bool FindEnemySpawn(GameObject entity)
    {
        RandomizeLocations();
        foreach (GameObject location in spawnLocations)
        {
            if (location.GetComponent<SpawnLocation>().ValidEnemy(entity)) return true;
        }
        return false;
    }

    bool FindPickupSpawn(GameObject entity)
    {
        RandomizeLocations();
        foreach (GameObject location in spawnLocations)
        {
            if (location.GetComponent<SpawnLocation>().ValidPickup(entity)) return true;
        }
        return false;
    }

    void RandomizeLocations()
    {
        List<GameObject> newOrder = new List<GameObject>();
        foreach (GameObject location in spawnLocations)
        {
            newOrder.Insert((int)(Random.Range(0,newOrder.Count - 1)), location);
        }
        spawnLocations = newOrder;
    }

    public void Register(GameObject loc)
    {
        spawnLocations.Add(loc);
    }

    void SpawnWave()
    {
        AddToQueue(countEnemyMelee, 0);
        AddToQueue(countEnemyRanged, 1);
        AddToQueue(countEnemyBoss, 2);
        AddToQueue(countPickupAmmo, 3);
        AddToQueue(countPickupHealth, 4);
        wave++;
    }

    void Start()
    {
        waveCooldownCounter = waveCooldowns[wave];
    }

    void Update()
    {
        waveCooldownCounter -= Time.deltaTime;
        if (waveCooldownCounter <= 0)
        {
            waveCooldownCounter = waveCooldowns[wave];
            if (wave < totalWaves) SpawnWave();
        }
        if (spawnQueue.Count > 0)
        {
            AttemptSpawn(spawnQueue[0]);
            spawnQueue.RemoveAt(0);
        }
    }
}
