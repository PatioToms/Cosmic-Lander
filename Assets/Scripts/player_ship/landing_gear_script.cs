using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class landing_gear_script : MonoBehaviour {

	[SerializeField] float probe_distance = 1000f;
    [SerializeField] LayerMask landingLayer;
    [SerializeField] PolygonCollider2D left_leg_collision;
    [SerializeField] PolygonCollider2D right_leg_collision;
    [SerializeField] Transform parentPos;

    private Animator anim;
    private bool landingOn;

    void Start ()
    {
		anim = GetComponent<Animator> ();
    }

    void FixedUpdate ()
    {
        RaycastHit2D hit = Physics2D.Raycast(parentPos.position, Vector2.down, probe_distance, landingLayer);
        //Debug.DrawRay(parentPos.position, Vector2.down * probe_distance, Color.cyan);
        landingOn = hit.collider != null ? true : false;
        anim.SetBool("is_landing_gear_active", landingOn);
    }

    public void SwitchCollisions ()
    {
        left_leg_collision.enabled = !left_leg_collision.enabled;
        right_leg_collision.enabled = !right_leg_collision.enabled;
    }
}
