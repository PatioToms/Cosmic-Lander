using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ore_script : MonoBehaviour {

	[SerializeField] int minerals_to_spawn = 3;
	[SerializeField] float red_value = 0.3686f; 
	[SerializeField] float heat_resistance = 0.05f;
	[SerializeField] float explode_delay = 0.4f;
	[SerializeField] AudioClip[] on_destroyed_fx;
	[SerializeField] GameObject mineral_prefab;
	[SerializeField] Sprite broken_sprite;

	private bool is_heating_up = false;
	private bool has_blown_up = false;

	ParticleSystem explosion_particle;
	SpriteRenderer main_sprite;
	AudioSource sound_emitter;
	Animator egg_animator;


	// Use this for initialization
	void Start () {

		explosion_particle = GetComponent<ParticleSystem> ();
		main_sprite = GetComponent<SpriteRenderer> ();
		sound_emitter = GetComponent<AudioSource> ();
		egg_animator = GetComponentInParent<Animator> ();

	}

	public void Stop_Heat(){
	
		is_heating_up = false;
		egg_animator.SetBool("is_heating_up", is_heating_up);
	
	}

	public void Heat_Up (){

		if (!is_heating_up) {
		
			is_heating_up = true;
			egg_animator.SetBool("is_heating_up", is_heating_up);
		
		}


		red_value += heat_resistance;
		main_sprite.color = new Color (red_value, 0, 0);

		if (red_value >= 1 && !has_blown_up) {

			has_blown_up = true;
			StartCoroutine (Give_Loot());
		
		}
	
	}

	IEnumerator Give_Loot (){

		egg_animator.SetTrigger ("explode");
		Destroy (GetComponent<PolygonCollider2D>());
		yield return new WaitForSeconds (explode_delay);

		sound_emitter.clip = on_destroyed_fx[Random.Range(0, on_destroyed_fx.Length)];
		sound_emitter.Play ();
		main_sprite.sprite = broken_sprite;
		explosion_particle.Play ();

		foreach (Transform child in transform) {  //we delete the eggs sprite
		
			GameObject.Destroy(child.gameObject);
		
		}

		for (int m = 0; m < minerals_to_spawn; m++){

			Instantiate (mineral_prefab, transform.position, Quaternion.identity);
			yield return new WaitForSeconds (0.1f);

		}

		yield return null;
	
	}

}
