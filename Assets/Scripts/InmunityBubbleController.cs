using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmunityBubbleController : MonoBehaviour {

    [SerializeField] Vector2[] waypoints;
    [SerializeField] float speed;

    bool transition;
    public int pointA;
    int pointB;
    float timer;

    private void Start()
    {
        transition = true;
        pointA = 0;
        pointB = 1;
    }

    private void Update()
    {
        if (transition)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(waypoints[pointA], waypoints[pointB], timer * speed);
            if (timer >= 1)
            {
                pointA += 1;
                pointB += 1;
                timer = 0;
                if (waypoints[pointB] == null)
                {
                    transition = false;
                }
                timer = 0;
            }
        }
    }

}
