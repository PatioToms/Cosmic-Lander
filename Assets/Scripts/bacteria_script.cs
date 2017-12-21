using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bacteria_script : MonoBehaviour {

    [SerializeField] float damage;
    [SerializeField] CamShakeManager.Properties shakeProperties;
    [SerializeField] GameObject bacteria_explosion; // explosión que daña al jugador cuando está en su rango
	[SerializeField] float speed = 5f;
	[SerializeField] AudioClip[] audio_array;
    [SerializeField] Transform player;

    Animator bacteria_anim; // el bool que cambia el estado de la bacteria es is_aggro_triggered
	AudioSource audio_emitter;
	[SerializeField] ParticleSystem trail_fx;
    bool canFollow;
	Coroutine sound_coroutine;

	void Start ()
    {
		bacteria_anim = this.GetComponent<Animator> ();
		audio_emitter = this.GetComponent<AudioSource> ();
	}

    void FixedUpdate()
    {
        if (canFollow)
        {
            Vector2 direction = player.position - transform.position;
            transform.Translate(direction * speed * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.tag == "Player")
		{
			if (sound_coroutine == null) {
				sound_coroutine = StartCoroutine (MakeSound ()); 
				trail_fx.Play ();
				canFollow = true;
				bacteria_anim.SetBool("is_aggro_triggered", true);
			}
		}
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canFollow = false;
			trail_fx.Stop ();
            bacteria_anim.SetBool("is_aggro_triggered", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Instantiate(bacteria_explosion, transform.position, transform.rotation);
			trail_fx.Stop ();
			trail_fx.transform.parent = null;
            GameObject.Find("Game Manager").GetComponent<ui_manager_script>().ApplyDamage(damage);
            //GameObject.Find("Camera").GetComponent<CamShakeManager>().StartShake(shakeProperties);
            Destroy(gameObject);
        }
    }

	IEnumerator MakeSound()
    {
	
		audio_emitter.clip = audio_array [Random.Range (0, audio_array.Length - 1)];
		audio_emitter.Play ();
		yield return new WaitForSeconds (3f);
		sound_coroutine = null;
		yield return null;
	
	}
		
}
