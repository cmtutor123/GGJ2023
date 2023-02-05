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

    private Transform trans;

    private EnemyHealth health;

    public int wallOffset;

    private int currentState = 0;

    void Defeated()
    {
        SceneManager.LoadScene("WinScreen");
    }

    void Start()
    {
        trans = GetComponent<Transform>();
        Instantiate(vineWall, new Vector3(trans.position.x, trans.position.y + wallOffset, trans.position.z), Quaternion.identity);
    }

    void Update()
    {
        if (health.GetHealth() <= 0) Defeated();
    }
}
