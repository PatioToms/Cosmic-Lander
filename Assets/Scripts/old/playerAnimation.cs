using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour {
    //public bool funciona;

    Rigidbody2D RB2D;

	[SerializeField] AnimationCurve SquashCurve;
	[SerializeField] AnimationCurve StretchCurve;

    // Use this for initialization
    void Start () {

		RB2D = GetComponentInParent<Rigidbody2D> ();

	}

	void FixedUpdate () {
        
		transform.LookAt(RB2D.velocity.normalized + (Vector2)transform.position);
        transform.localScale = new Vector3(1, SquashCurve.Evaluate(RB2D.velocity.magnitude), StretchCurve.Evaluate(RB2D.velocity.magnitude));
    
	}
}
