using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBarManager : MonoBehaviour
{
    public float xLoc;
    public float yLoc;
    public float xSize;
    public float ySize;
    private float zPos;
    private float healthPercent = 1f;

    private Transform trans;

    private Camera cam;

    public void SetHealthPercentage(float percent)
    {
        healthPercent = percent;
        UpdateHealthBar();
    }

    void Start()
    {
        trans = GetComponent<Transform>();
        zPos = trans.position.z;
        Vector3 newPos = Camera.main.ViewportToWorldPoint(new Vector2(xLoc, yLoc));
        newPos.z = zPos;
        trans.localPosition = newPos;
        trans.localScale = Camera.main.ViewportToWorldPoint(new Vector2(xSize, ySize));
    }

    void Update()
    {
        trans = GetComponent<Transform>();
        zPos = trans.position.z;
        Vector3 newPos = Camera.main.ViewportToWorldPoint(new Vector2(xLoc, yLoc));
        newPos.z = zPos;
        trans.position = newPos;
        Vector3 newScale = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) - Camera.main.ViewportToWorldPoint(new Vector2(0,0));
        trans.localScale = new Vector3(newScale.x * xSize, newScale.y * ySize, 1);
    }

    void UpdateHealthBar()
    {
        Transform greenTrans = GameObject.Find("GreenBar").GetComponent<Transform>();
        greenTrans.localScale = new Vector3(healthPercent, 1, 1);
    }
}
