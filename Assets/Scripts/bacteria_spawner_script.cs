using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bacteria_spawner_script : MonoBehaviour {
    
    [SerializeField] GameObject bacteriaPrefab;
    [SerializeField] float spawnForce;
    [SerializeField] float delayToSpawn;

    private Animator bacteria_animator;
    private ParticleSystem spawn_fx;
    GameObject bacteria;
    float oldDelayToSpawn;
    bool animationOn;

    void Start ()
    {
        bacteria_animator = GetComponent<Animator>();
        spawn_fx = GetComponent<ParticleSystem>();
        oldDelayToSpawn = delayToSpawn;
    }

    private void Update()
    {
        if (bacteria == null /*&& bacteria_animator.GetCurrentAnimatorStateInfo(0).IsName("bacteria_spawner_trigger") == false*/)
        {
            delayToSpawn -= Time.deltaTime;
            if(delayToSpawn <= 0)
            {
                SpawnBacteria();
                delayToSpawn = oldDelayToSpawn;
            }
        }
    }

    void SpawnBacteria()
    {
        bacteria_animator.SetTrigger("activate");
        spawn_fx.Play();
        bacteria = Instantiate(bacteriaPrefab, transform.position, Quaternion.identity);
        bacteria.GetComponent<Rigidbody2D>().AddForce(transform.up * spawnForce);
    }
}
