using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toxic_cloud_script: MonoBehaviour {

	[SerializeField] float scrollSpeed_X = 0.5f;
	[SerializeField] float scrollSpeed_Y = 0.5f;
	[SerializeField] float ScaleToTiles = 0.667f;

	private Renderer renderTexture;
		
	void Awake() {
	
		renderTexture = GetComponent<Renderer>();

		float TextureScale_X = transform.lossyScale.x * ScaleToTiles;
		float TextureScale_Y = transform.lossyScale.z * ScaleToTiles;
		renderTexture.material.SetTextureScale ("_MainTex", new Vector2 (TextureScale_X, TextureScale_Y));
	}
		
	void Update() {
			
		float offset_X = Time.time * scrollSpeed_X;
		float offset_Y = Time.time * scrollSpeed_Y;
		renderTexture.material.SetTextureOffset ("_MainTex", new Vector2 (offset_X, offset_Y));

	}

}
