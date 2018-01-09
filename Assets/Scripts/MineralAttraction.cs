using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineralAttraction : MonoBehaviour {

    [SerializeField] private float speed = 15f;
    [SerializeField] private GameObject getMineralParticlesPrefab;
    public float reactorModifier;
    [SerializeField] float distanceToTake;
    
    private Transform target;
    private float distance;
    private Rigidbody2D rb;
    private float oldGravity;
    private bool isAttracted;
    private ShipMovement shipScr;
    ui_manager_script UIScr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shipScr = GameObject.Find("obj_player_ship").GetComponent<ShipMovement>();
        UIScr = GameObject.Find("Game Manager").GetComponent<ui_manager_script>();
        oldGravity = rb.gravityScale;
    }
    
    private void FixedUpdate()
    {
        if (isAttracted && shipScr.numMinerals < 3)
        {
            Vector3 direction = target.position - transform.position;
            rb.velocity = direction * speed;
            distance = Vector3.Distance(target.position, transform.position);
            if (distance <= distanceToTake)
            {
                Instantiate(getMineralParticlesPrefab, target);

                shipScr.numMinerals += 1;
                shipScr.reactorForce -= reactorModifier;
                shipScr.vuelta = true;
                UIScr.SetUIEggs(shipScr.numMinerals);

                Destroy(gameObject);
            }
        }
        /*if (isAttracted && shipScr.numMinerals >= 3)
        {
            transform.position = Random.insideUnitCircle * 2;
        }*/
    }

    public void EnableAttraction (Transform destination)
    {
        isAttracted = true;
        rb.gravityScale = 0;
        target = destination;
    }

    public void DisableAttraction()
    {
        isAttracted = false;
        rb.gravityScale = oldGravity;
    }
}
