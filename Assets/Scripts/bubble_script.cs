using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bubble_script : MonoBehaviour {

    [SerializeField] float damage;
	[SerializeField] float bubble_speed = 3f;
    [SerializeField] GameObject bubble_explosion_prefab;

    SpriteRenderer shrapnel_sprite;
    Animator bubble_animator;


	public void StartSelfDestruct(float lifetime){

		shrapnel_sprite = gameObject.GetComponent<SpriteRenderer> ();
		StartCoroutine (ChangeTint (lifetime));
		StartCoroutine (SelfDestructProcess (lifetime));
	
	}

	IEnumerator SelfDestructProcess(float lifetime){

        bubble_animator = gameObject.GetComponent<Animator>();
        yield return new WaitForSeconds (lifetime-1f);
        bubble_animator.SetBool("is_about_to_burst", true);
        yield return new WaitForSeconds (1f);
		SelfDestruct ();

	}


    public void ActivateCollisions()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
    }


	IEnumerator ChangeTint(float lifetime){
	
		float total_lifetime = lifetime;

		while (lifetime >= 0){

			float color_fraction = lifetime / total_lifetime;
			shrapnel_sprite.color = new Color (color_fraction, color_fraction, color_fraction);
			lifetime -= Time.deltaTime;
			yield return new WaitForFixedUpdate();

		}

		yield return null;
	
	} 


	void SelfDestruct()
    {
        Instantiate(bubble_explosion_prefab, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
	}


	void FixedUpdate()
    {
		transform.Translate (Vector3.up * bubble_speed * Time.fixedDeltaTime);
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Instantiate(bubble_explosion_prefab, transform.position, transform.rotation);
            GameObject.Find("Game Manager").GetComponent<ui_manager_script>().ApplyDamage(damage);
            Destroy(gameObject);
        }
    }

}
