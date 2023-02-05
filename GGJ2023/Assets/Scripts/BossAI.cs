using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossAI : MonoBehaviour
{
    public Sprite bossClosed;
    public Sprite bossOpened;

    public GameObject vineAttack;
    public GameObject vineWall;

    private BoxCollider2D hitbox;
    private SpriteRenderer rend;

    private SpawnManager waveSpawner;

    private Transform trans;

    private EnemyHealth health;

    public float wallOffset;

    private int currentState = 0;
    public List<float> stateDurations = new List<float>();
    private float stateCounter;

    void Defeated()
    {
        SceneManager.LoadScene("WinScreen");
    }

    void Close()
    {
        rend.sprite = bossClosed;
        hitbox.enabled = false;
    }

    void Open()
    {
        rend.sprite = bossOpened;
        hitbox.enabled = true;
    }

    void Start()
    {
        trans = GetComponent<Transform>();
        Instantiate(vineWall, new Vector3(trans.position.x, trans.position.y + wallOffset, trans.position.z), Quaternion.identity);
        hitbox = GetComponent<BoxCollider2D>();
        rend = GetComponent<SpriteRenderer>();
        waveSpawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        stateCounter = stateDurations[0];
        health = GetComponent<EnemyHealth>();
    }

    void Update()
    {
        if (health.GetHealth() <= 0) Defeated();
        stateCounter -= Time.deltaTime;
        if (stateCounter <= 0)
        {
            currentState++;
            if (currentState == stateDurations.Count) currentState = 0;
            stateCounter = stateDurations[currentState];
            switch (currentState)
            {
                case 0:
                    Open();
                    break;
                case 1:
                    Close();
                    waveSpawner.spawnQueue.Add(0);
                    waveSpawner.spawnQueue.Add(0);
                    waveSpawner.spawnQueue.Add(1);
                    waveSpawner.spawnQueue.Add(1);
                    break;
                case 2:
                    Close();
                    waveSpawner.spawnQueue.Add(3);
                    waveSpawner.spawnQueue.Add(3);
                    waveSpawner.spawnQueue.Add(4);
                    break;
            }
        }
        
    }
}
