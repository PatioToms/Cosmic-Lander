using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class self_destroying_object : MonoBehaviour {

	[SerializeField] float delay = 4f;

	void Start () {

		StartCoroutine (countdown());

	}

	IEnumerator countdown(){
	
		yield return new WaitForSeconds(delay);
		Destroy (gameObject);
	
	
	}
		
}
