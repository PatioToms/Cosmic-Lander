using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bacteria_spawner_script : MonoBehaviour {

    private Animator bacteria_animator;
    private ParticleSystem spawn_fx;


	void Start () {

        bacteria_animator = GetComponent<Animator>();
        spawn_fx = GetComponent<ParticleSystem>();

    }

    void SpawnBacteria() {

        bacteria_animator.SetTrigger("activate");

    }

    void SpawnBacteriaFX() {

        spawn_fx.Play();

    }
	

}
