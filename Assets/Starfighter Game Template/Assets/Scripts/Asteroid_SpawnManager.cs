using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_SpawnManager : MonoBehaviour {
	[Tooltip("Prefab to spawn")]
	public GameObject obj;
	string oTag;
	[Tooltip("Amount of prefabs to be spawned")]
	public int quantity;
	public Vector3 SpawnRegion;
	public PlayerSpaceship player;
	GameObject [] objects;

	void Awake () {
		oTag = obj.tag;
		for (int i = 0; i < quantity; i++) {
			Vector3 randomPos = new Vector3 (Random.Range (-SpawnRegion.x, SpawnRegion.x), Random.Range (-SpawnRegion.y, SpawnRegion.y), Random.Range (-SpawnRegion.z, SpawnRegion.z));
			GameObject s = Instantiate (obj, transform.position + randomPos, transform.rotation) as GameObject;
		}
		objects = GameObject.FindGameObjectsWithTag (oTag);
	}

	void Respawn () {
		for (int i = 0; i < objects.Length; i++) {
			if (!objects [i].activeSelf) {
				Vector3 randomPos = new Vector3 (Random.Range (-SpawnRegion.x, SpawnRegion.x), Random.Range (-SpawnRegion.y, SpawnRegion.y), Random.Range (-SpawnRegion.z, SpawnRegion.z));
				objects [i].transform.position = transform.position + randomPos;
				objects [i].transform.rotation = transform.rotation;
				objects [i].SetActive (true);
			}
		}
	}

	void Update () {
		Respawn ();
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube (transform.position, SpawnRegion*2);
	}
}
