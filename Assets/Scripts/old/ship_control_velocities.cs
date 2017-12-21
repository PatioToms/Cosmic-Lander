using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship_control_velocities: MonoBehaviour {

	[SerializeField] float push_force = 10f;
	[SerializeField] float rotation_force = 3f;
	[SerializeField] float gravity = -0.3f;
	[SerializeField] AnimationCurve engine_curve;

	float push_force_multiplier;
	bool is_using_reactor;
	float cache_gravity;
	float ship_speed;
	Rigidbody2D rb_main;


	void Start () {

		is_using_reactor = false;
		cache_gravity = gravity;
		push_force_multiplier = 1;
		rb_main = GetComponent<Rigidbody2D> ();
	
	}

	void FixedUpdate () {

		rb_main.velocity += (Vector2.down * cache_gravity); // we apply gravity, wich in this case will be a constant force

		print (push_force_multiplier);

		if (is_using_reactor) {

			rb_main.velocity = (gameObject.transform.up * push_force * push_force_multiplier); 

		}


		if (Input.GetKeyDown(KeyCode.Space)) {
			
			cache_gravity -= gravity;
			//EngineStartup ();
			is_using_reactor = true;

		}
			

		if (Input.GetKeyUp(KeyCode.Space)) {

			cache_gravity += gravity;
			is_using_reactor = false;
			//StopCoroutine ("EngineStartup");
			//push_force_multiplier = 0;

		}

		if (Input.GetKey (KeyCode.A)) {
		
			rb_main.MoveRotation (rb_main.rotation += (rotation_force * Time.deltaTime)); // left rotation
		
		}

		if (Input.GetKey (KeyCode.D)) {

			rb_main.MoveRotation (rb_main.rotation -= (rotation_force * Time.deltaTime)); // right rotation

		}

	}


	IEnumerator EngineStartup (){

		for (float rev_time = 0f; rev_time >= 1f; rev_time += 0.05f){

			push_force_multiplier = rev_time * rev_time;
			yield return null;

		}
	
	}

}

