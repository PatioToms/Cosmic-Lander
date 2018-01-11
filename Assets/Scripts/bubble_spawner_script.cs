using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_spawner_script : MonoBehaviour {

	[SerializeField] float spawn_timer = 5f; //time it takes between each bubble spawn to start spawning another
	[SerializeField] float bubble_timer = 4f; //variable used to set the bubble lifetime variable
    [SerializeField] float startSpawnDelay;
	[SerializeField] GameObject bubble_prefab;
	[SerializeField] Transform spawn_point;
	[SerializeField] AudioClip[] spawn_sound_array;

	private Animator spawner_anim;
	private AudioSource audio_emmiter;
	private ParticleSystem spawn_particle;
    float oldSpawnTimer;
    float oldDelay;

	void Start(){
	
		spawner_anim = GetComponent<Animator> ();
		audio_emmiter = GetComponent<AudioSource> ();
		spawn_particle = GetComponent<ParticleSystem> ();
        //StartCoroutine (BubbleCooldown ());
        oldSpawnTimer = spawn_timer;
	}

    private void Update()
    {
        startSpawnDelay -= Time.deltaTime;
        if(startSpawnDelay <= 0)
        {
            spawn_timer -= Time.deltaTime;
            if (spawn_timer <= 0)
            {
                spawner_anim.SetTrigger("spawn");
                spawn_timer = oldSpawnTimer;
            }
        }
    }

    /*IEnumerator BubbleCooldown(){
	
        yield return new WaitForSeconds(spawn_timer-0.6f);
        spawner_anim.SetTrigger("spawn");
        yield return new WaitForSeconds(0.6f);
        SpawnBubble ();
		StartCoroutine (BubbleCooldown());
	
	}*/

	public void SpawnBubble()
    {
        bubble_script prefab_script = Instantiate (bubble_prefab, spawn_point.position, Quaternion.identity).GetComponent<bubble_script>();
		prefab_script.StartSelfDestruct (bubble_timer);

	}

	public void CreateSound(){
	
		audio_emmiter.clip = spawn_sound_array [Random.Range (0, spawn_sound_array.Length - 1)];
		audio_emmiter.Play();
		spawn_particle.Play();
	
	}

}
