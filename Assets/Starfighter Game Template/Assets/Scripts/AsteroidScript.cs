using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Starfighter Game Template/Asteroid Script")]
public class AsteroidScript : MonoBehaviour {

	[Tooltip("Current health value")]
	public float health;
	[Tooltip("Maximum health")]
	public float maxHealth;
	public float minRot;
	public float maxRot;
	public float minScale;
	public float maxScale;
	Vector3 rot;
	[Tooltip("Explosion prefab to instatiate, one will be selected randomly")]
	public GameObject [] explosion;
	[Tooltip("Asteroid mesh, one will be selected randomly")]
	public GameObject [] mesh;

	void Awake () {
		int r = Random.Range (0, mesh.Length);
		mesh [r].SetActive (true);
		for (int i = 0; i < mesh.Length; i++) {
			if (i != r)
				mesh [i].SetActive (false);
		}
		float sc = Random.Range (minScale, maxScale);
		rot.x = Random.Range (minRot, maxRot);
		rot.y = Random.Range (minRot, maxRot);
		rot.z = Random.Range (minRot, maxRot);
		Vector3 randomStartRot = new Vector3 (Random.Range (0, 360), Random.Range (0, 360), Random.Range (0, 360));
		transform.localRotation = Quaternion.Euler (randomStartRot);
		transform.localScale = new Vector3 (sc, sc, sc);
	}

	public void ModifyHealth(float dmg) {

		health -= dmg;

		if (health <= 0) {
			Die ();
		}
	}

	void Die() {
		int r = Random.Range (0, explosion.Length);
		Instantiate (explosion[r], transform.position, transform.rotation);
		health = maxHealth;
		gameObject.SetActive (false);
	}

	void Update () {
		if (Time.timeScale > 0)
			transform.Rotate (rot, Space.Self);
	}
}
