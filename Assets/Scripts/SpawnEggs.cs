using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEggs : MonoBehaviour {

    [SerializeField] int minerals_to_spawn = 3;
    [SerializeField] float explode_delay = 0.4f;
    [SerializeField] AudioClip[] on_destroyed_fx;
    [SerializeField] GameObject mineral_prefab;
    [SerializeField] Sprite broken_sprite;
    [SerializeField] Animator animator;

    AudioSource sound_emitter;
    SpriteRenderer main_sprite;
    ParticleSystem explosion_particle;

    private void Start()
    {
        main_sprite = GetComponent<SpriteRenderer>();
        explosion_particle = GetComponent<ParticleSystem>();
        sound_emitter = GetComponent<AudioSource>();
    }

    public void GiveLoot()
    {
        StartCoroutine(_GiveLoot());
    }

    IEnumerator _GiveLoot()
    {
        animator.SetTrigger("explode");
        Destroy(GetComponent<PolygonCollider2D>());
        yield return new WaitForSeconds(explode_delay);
        sound_emitter.clip = on_destroyed_fx[Random.Range(0, on_destroyed_fx.Length)];
        sound_emitter.Play();
        main_sprite.sprite = broken_sprite;
        explosion_particle.Play();

        foreach (Transform child in transform)
        {  //we delete the eggs sprite

            GameObject.Destroy(child.gameObject);

        }

        for (int m = 0; m < minerals_to_spawn; m++)
        {

            Instantiate(mineral_prefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);

        }

        yield return null;

    }

}
