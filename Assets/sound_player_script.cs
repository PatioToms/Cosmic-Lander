using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound_player_script : MonoBehaviour {

	[SerializeField] private AudioClip other_clip;
	[SerializeField] private AudioSource sound_emitter;

	public void playSound(){
		
		sound_emitter.pitch = Random.Range (0.7f, 1f);
		sound_emitter.Play ();
	
	}

	public void playSoundSpecial(){
	
		sound_emitter.clip = other_clip;
		sound_emitter.pitch = 1f;
		sound_emitter.Play ();
	
	}


}
