using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mineral_script : MonoBehaviour {

	[SerializeField] float value = 20;
	[SerializeField] float strength_x = 50f;
	[SerializeField] float strength_y = 350f;
	[SerializeField] GameObject eggsplosion;
    
	ui_manager_script ui_m;
	Rigidbody2D mineral_rb;
    
	void Start ()
    {
        ui_m = GameObject.Find ("Game Manager").GetComponent<ui_manager_script> ();
	    mineral_rb = GetComponent<Rigidbody2D> ();
        mineral_rb.AddForce(new Vector2(Random.Range(-strength_x, strength_x), Random.Range((strength_y - 100), strength_y)), ForceMode2D.Impulse);
        mineral_rb.MoveRotation(Random.Range(-strength_x, strength_x));
    }


	void OnCollisionEnter2D (Collision2D collision)
    {
		if (collision.gameObject.tag != "mineral") {

			if ((collision.gameObject.tag != "Player") && (mineral_rb.velocity.magnitude >= 0.5f)) {
		
				Instantiate (eggsplosion, transform.position, Quaternion.identity);
				Destroy (gameObject);
			}
		}
	}

	public void Destroy_Special ()
    {
		if (ui_m != null)
        {
			ui_m.modify_score(value);
		}
		Destroy(gameObject);
    }
}
