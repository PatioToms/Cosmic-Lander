using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ship_reactor_script : MonoBehaviour {

	[SerializeField] bool can_interact_with_ore = false;
	[SerializeField] ParticleSystem reactor_flame_particle;
	[SerializeField] ParticleSystem reactor_smoke_particle;

	private Rigidbody2D parent_rb;
	private AudioSource reactor_sound_emitter;
	private Animator reactor_animator;
	private HeatObject heatObjectScr;
	private float reactor_sound_alpha;
	public bool can_fly = true; //must be public so other scripts can access it


	// Use this for initialization
	void Start () {

		//reactor_flame_particle = GameObject.Find("reactor_fire").GetComponent<ParticleSystem> ();
		//reactor_smoke_particle = GameObject.Find("reactor_smoke").GetComponent<ParticleSystem> ();
		parent_rb = GetComponentInParent<Rigidbody2D> ();
		reactor_sound_emitter = this.GetComponent<AudioSource> ();
		reactor_animator = GetComponent<Animator> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		reactor_sound_alpha = (Mathf.Clamp (parent_rb.velocity.magnitude, 0, 15) / 15);
		reactor_sound_emitter.pitch = reactor_sound_alpha;
	
		if (Input.GetButtonDown("ReactorInput") && (can_fly)) {

			reactor_animator.SetBool ("is_reactor_active", true);
			reactor_flame_particle.Play ();
			reactor_smoke_particle.Play ();
			can_interact_with_ore = true;
			reactor_sound_emitter.Play ();
		
		}

		if (Input.GetButtonUp("ReactorInput") || (!can_fly)) {

			if (can_interact_with_ore == true) {
			
				reactor_animator.SetBool ("is_reactor_active", false);
				reactor_flame_particle.Stop ();
				reactor_smoke_particle.Stop ();
				can_interact_with_ore = false;
				reactor_sound_emitter.Stop ();

				if (heatObjectScr != null) {
				
					heatObjectScr.Stop_Heat ();
				
				}
			
			}
		}

	}

	void OnTriggerStay2D(Collider2D col)
    {
        heatObjectScr = col.gameObject.GetComponent<HeatObject>();
        if (col.gameObject.layer == 13 && can_interact_with_ore && heatObjectScr != null)
        {
            heatObjectScr.Heat_Up();
        }
	}

	void OnTriggerExit2D(Collider2D col)
    {
        heatObjectScr = col.GetComponent<HeatObject>();
        if (col.gameObject.layer == 13 && can_interact_with_ore && heatObjectScr != null)
        {
            heatObjectScr.Stop_Heat ();
		}
	}

	public void SeparateParticles(){
	
		reactor_flame_particle.Stop ();
		reactor_smoke_particle.Stop ();
		reactor_flame_particle.transform.parent = null;
		reactor_smoke_particle.transform.parent = null;
	
	}

}
