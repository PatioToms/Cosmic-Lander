using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_manager_script : MonoBehaviour {

	[SerializeField] bool does_it_trigger = false;
	[SerializeField] float timer = 5f;

	[SerializeField] AudioSource ambient_sound;
	[SerializeField] Animator fade_animator;
	[SerializeField] string level_name = "Lvl_MainMenu";


	void Start(){
	
		if (does_it_trigger) {
		
			Invoke ("change_level_generic", timer);
		
		}
	
	}


	public void change_level_special () {
	
		fade_animator.SetTrigger ("QuitLevel");
		StartCoroutine (change_level());
	
	}

	public void change_level_generic () {

		StartCoroutine (change_level());

	}

	IEnumerator change_level(){

		while (ambient_sound.volume > 0) {
		
			ambient_sound.volume -= Time.fixedDeltaTime * 0.75f;
			yield return new WaitForFixedUpdate ();
		
		}
			
		SceneManager.LoadScene(level_name);
		yield return null;
	
	}	


}
