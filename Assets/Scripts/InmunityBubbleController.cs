using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InmunityBubbleController : MonoBehaviour {

    [SerializeField] Vector2[] waypoints;
    [SerializeField] float speed;

    [HideInInspector] public bool playerProtected;

    int indexA, indexB;
    float timer;
    bool transition;

    private void Start()
    {
        indexA = 0;
        indexB = 1;
        transform.position = waypoints[0];
    }

    private void Update()
    {
        if (transition)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(waypoints[indexA], waypoints[indexB], timer * speed);
            if (transform.position == (Vector3)waypoints[indexB])
            {
                indexA += 1;
                indexB += 1;
                timer = 0;
                if (indexB == waypoints.Length)
                    transition = false;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, .5f);
        for (int i = 0; i < waypoints.Length; i++)
        {
            Gizmos.DrawSphere(waypoints[i], 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        transition = true;
        playerProtected = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerProtected = false;
    }
}
