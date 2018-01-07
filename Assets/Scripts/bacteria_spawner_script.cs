using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bacteria_spawner_script : MonoBehaviour {

    [SerializeField] int maxAliveBacterias;
    [SerializeField] GameObject BacteriaPrefab;

    private Animator bacteria_animator; //la variable trigger se llama "activate"
    private ParticleSystem spawn_fx; 


	void Start ()
    {

        bacteria_animator = GetComponent<Animator>();
        spawn_fx = GetComponent<ParticleSystem>();

    }


    void SpawnBacteria()
    { //esta función es la que spawnea la propia bacteria, incluyendo los efectos de partículas, sonido, etc.
        spawn_fx.Play();

    }
	

}
