using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovementOld: MonoBehaviour {

    [SerializeField] private float curveSpeed = 2f;
    [SerializeField] private AnimationCurve reactorAcceleration;
    [SerializeField] float push_force = 10f;
	[SerializeField] float rotation_force = 3f;

    private float timer = 0;
	float cache_gravity;
	float ship_speed;
	Rigidbody2D rb_main;
	ui_manager_script ui_manager;


	void Start () {
        
		rb_main = GetComponent<Rigidbody2D> ();
		ui_manager = GameObject.Find ("UI_manager").GetComponent<ui_manager_script> ();

	}

	void Update () {

        /*if (is_using_reactor) {
			rb_main.velocity = (gameObject.transform.up * push_force);
			ui_manager.modify_fuel (-0.1f);
		}
		*/

        if (Input.GetKey(KeyCode.Space))
        {
            ui_manager.modify_fuel(-0.1f);
            timer += Time.deltaTime * curveSpeed;
            rb_main.velocity = gameObject.transform.up * push_force * reactorAcceleration.Evaluate(Mathf.Clamp(timer, 0, 1));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            timer = reactorAcceleration.Evaluate(0);
			rb_main.freezeRotation = false;
        }

		if (Input.GetKeyDown(KeyCode.Space))
		{
			//cache_gravity -= gravity;
			//is_using_reactor = true;
			timer = reactorAcceleration.Evaluate(0);
			rb_main.freezeRotation = true;
		}

		if (Input.GetKey (KeyCode.A))
        {
			rb_main.MoveRotation (rb_main.rotation += (rotation_force * Time.deltaTime)); // left rotation
		}

		if (Input.GetKey (KeyCode.D))
        {
			rb_main.MoveRotation (rb_main.rotation -= (rotation_force * Time.deltaTime)); // right rotation
		}
	}

}
	

