using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score_scene_script : MonoBehaviour {

	[SerializeField] AudioClip[] reward_sound_array;
	[SerializeField] Color[] reward_color_array;
	[SerializeField] string[] reward_string_array;

	[SerializeField] Text score_text;
	[SerializeField] Text reward_text;

	[SerializeField] private AudioClip score_ding;
	[SerializeField] private AudioClip default_sfx;
 
	private AudioSource sfx_emitter;
	private Animator canvas_animator;
	float currentscore;
	private int reward_index;

	void Start () {

		//currentscore = Mathf.RoundToInt(ui_manager_script.persistent_score);
		currentscore = 5;
		print (currentscore);
		reward_index = Mathf.Clamp (Mathf.RoundToInt (currentscore / 3), 0, 3);
		print (reward_index);
		canvas_animator = this.GetComponent<Animator> ();
		sfx_emitter = this.GetComponent<AudioSource> ();

	}


	public void start_score_countdown(){
	
		sfx_emitter.clip = score_ding;
		StartCoroutine (score_countdown ());
	
	}
		

	IEnumerator score_countdown(){

		for (int i=0; i < currentscore; i++){

			sfx_emitter.Play ();
			score_text.text = i.ToString ();
			yield return new WaitForSeconds(0.3f);

		}

		score_text.text = currentscore.ToString ();
			
		yield return new WaitForSeconds (1.5f);

		sfx_emitter.clip = reward_sound_array [reward_index];
		sfx_emitter.volume = 1;
		sfx_emitter.Play ();
		reward_text.text = reward_string_array [reward_index];
		reward_text.color = reward_color_array [reward_index];

		yield return new WaitForSeconds (sfx_emitter.clip.length / 2);

	}


}
