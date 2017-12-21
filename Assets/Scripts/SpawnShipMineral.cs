using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnShipMineral : MonoBehaviour {

    [SerializeField] float value = 20;
    [SerializeField] GameObject eggsplosion;
    [SerializeField] float spawnForce;

    ui_manager_script ui_m;
    Rigidbody2D rb;

    private void Start()
    {
        ui_m = GameObject.Find("Game Manager").GetComponent<ui_manager_script>();
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(transform.up * spawnForce);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "mineral")
        {
            if ((collision.gameObject.tag != "Player") && (rb.velocity.magnitude >= 0.25f))
            {
                Instantiate(eggsplosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    public void Destroy_Special()
    {
        if (ui_m != null)
        {
            ui_m.modify_score(value);
        }
        Destroy(gameObject);
    }
}
