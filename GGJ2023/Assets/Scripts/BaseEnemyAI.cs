using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class BaseEnemyAI : MonoBehaviour
{
    private Transform target;

    private Path path;
    Rigidbody2D rb;
    Seeker seeker;

    private bool reachedEndOfPath = false;
    public float speed;
    public float nextWaypointDistance;
    private int currentWaypoint;

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }    
    }

    void Start()
    {
        path = GameObject.Find("A* Path").GetComponent<Path>();
        target = GameObject.Find("Player").GetComponent<Transform>();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void Update()
    {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else reachedEndOfPath = false;
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        rb.velocity = direction * speed;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) currentWaypoint++;
    }
}
