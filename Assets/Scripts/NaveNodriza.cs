using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaveNodriza : MonoBehaviour {

	[SerializeField] float feedback_time = 2.99f;

	Animator animation_tree;
	ParticleSystem smoke_fx;

	private float active_time;
	private float reserve_time;
	private bool is_coroutine_running;

	void Start(){
	
		smoke_fx = GetComponent<ParticleSystem> ();
		animation_tree = GetComponent<Animator> ();
		active_time = 0f;
		reserve_time = 0f;
		is_coroutine_running = false;
	
	}


    private void OnTriggerEnter2D(Collider2D collision)
    {

		SpawnShipMineral collision_script = collision.gameObject.GetComponent<SpawnShipMineral> ();

		if (collision_script != null) {
			//Destroy(collision.gameObject); //not necessary; the mineral itself has already a function that modifies the score / deletes itself
			collision_script.Destroy_Special ();

			if (!is_coroutine_running) {

				smoke_fx.Play ();
				StartCoroutine (ActivateMachine (feedback_time));
			
			} else {
			
				reserve_time += feedback_time;
			
			}
        }
    }


	IEnumerator ActivateMachine (float extra_time)
	{

		animation_tree.SetBool ("is_active", true);
		is_coroutine_running = true;
		yield return new WaitForSeconds (extra_time);
		is_coroutine_running = false;

		if (reserve_time > 0f) {

			active_time = reserve_time;
			reserve_time = 0f;
			StartCoroutine (ActivateMachine (active_time));

		} else {

			animation_tree.SetBool ("is_active", false);
			smoke_fx.Stop ();
			yield return null;

		}

	}
		
}
	
