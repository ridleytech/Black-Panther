using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Starfighter Game Template/Planet")]
public class PlanetScript : MonoBehaviour {

	public float cloudMovement;
	public MeshRenderer clouds;
	public Vector3 orbitalMovement;

	void Start () {
		cloudMovement = cloudMovement / 10000;
		orbitalMovement = new Vector3 (orbitalMovement.x / 100, orbitalMovement.y / 100, orbitalMovement.z / 100);
	}

	void Update () {
		if (Time.timeScale > 0) {
			transform.Rotate (orbitalMovement, Space.Self);
			if (clouds != null) {
				clouds.material.mainTextureOffset = new Vector2 (clouds.material.mainTextureOffset.x + cloudMovement, 0); 
			}
		}
	}
}
