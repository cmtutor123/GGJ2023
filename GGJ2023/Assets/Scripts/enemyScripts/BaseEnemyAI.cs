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

    public float speed;
    public float nextWaypointDistance;
    private int currentWaypoint;

    bool findTarget()
    {
        target = GameObject.Find("Player").GetComponent<Transform>();
        return !(target == null);
    }

    bool hasTarget()
    {
        if (target == null) return findTarget();
        else return true;
    }

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
        target = GameObject.Find("Player").GetComponent<Transform>();

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);

        seeker.StartPath(rb.position, target.position, OnPathComplete);
    }

    void Update()
    {
        if (!hasTarget()) return;
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count) return;
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;

        rb.velocity = direction * speed;

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) currentWaypoint++;
    }

    void UpdatePath()
    {
        if (seeker.IsDone()) seeker.StartPath(rb.position, target.position, OnPathComplete);
    }
}
