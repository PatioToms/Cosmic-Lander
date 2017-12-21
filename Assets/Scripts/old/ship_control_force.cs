using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship_control_force: MonoBehaviour {

    [SerializeField] private float curveSpeed = 2f;
    [SerializeField] private AnimationCurve reactorAcceleration;
    [SerializeField] float push_force = 10f;
	[SerializeField] float rotation_force = 3f;

    private float timerAcceleration = 0;
    private float timerDeceleration = 0;
    Rigidbody2D rb_main;
	ui_manager_script ui_manager;
    private bool gravityOn;
    private Vector2 oldVelocity;

	void Start () {
        
		rb_main = GetComponent<Rigidbody2D> ();
		ui_manager = GameObject.Find ("UI_manager").GetComponent<ui_manager_script> ();

	}

	void Update () {

        if (gravityOn)
        {
            timerDeceleration += Time.deltaTime;
            rb_main.velocity = Vector2.Lerp(oldVelocity, Vector2.down * 5, timerDeceleration);
        }

        //Reactor
        if (Input.GetKey(KeyCode.Space))
        {
            ui_manager.modify_fuel(-0.1f);
            timerAcceleration += Time.deltaTime * curveSpeed;
            rb_main.velocity = gameObject.transform.up * push_force * reactorAcceleration.Evaluate(Mathf.Clamp(timerAcceleration, 0, 1));
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            rb_main.freezeRotation = false;
            gravityOn = true;
            oldVelocity = rb_main.velocity;
        }

		if (Input.GetKeyDown(KeyCode.Space))
		{
			timerAcceleration = reactorAcceleration.Evaluate(0);
			rb_main.freezeRotation = true;
            gravityOn = false;
            timerDeceleration = 0;
		}


        //Rotations
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
	

