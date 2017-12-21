using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dummy_ship_script : MonoBehaviour {

	void Start () {

		GameObject.Find ("obj_player_camera").GetComponent<CamMovement> ().target = transform;

	}
	

}
