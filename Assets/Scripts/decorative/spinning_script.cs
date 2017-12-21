using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinning_script : MonoBehaviour {

	[SerializeField] float rotation_speed = 1f;

	void Update () {

		transform.Rotate(Vector3.forward, rotation_speed * Time.deltaTime);

	}
}
