using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[AddComponentMenu("Starfighter Game Template/Spawn Manager")]
public class SpawnManager : MonoBehaviour {

	[Tooltip("Prefabs to spawn, they must have same tag")]
	public GameObject[] obj;
	string oTag;
	[Tooltip("Amount of each prefab to be spawned")]
	public int[] quantity; // If you have, say, 3 types of spaceship prefabs, then size of this array should be 3 as well. 
	public Vector3 SpawnRegion;
	public PlayerSpaceship player;
	public bool formationMode;
	Transform battleCenter;
	float battleSize;
	GameObject [] objects;

	void Awake () {
		battleCenter = GameObject.Find ("BattleCenter").transform;
		battleSize = battleCenter.GetComponent<BattleCenter> ().battleSize;

		oTag = obj[0].tag;
		for (int q = 0; q < quantity.Length; q++) {
			for (int i = 0; i < quantity[q]; i++) {
				Vector3 randomPos = new Vector3 (Random.Range (-SpawnRegion.x, SpawnRegion.x), Random.Range (-SpawnRegion.y, SpawnRegion.y), Random.Range (-SpawnRegion.z, SpawnRegion.z));
				GameObject s = Instantiate (obj[q], transform.position + randomPos, transform.rotation) as GameObject;
				s.GetComponent<StarshipAI> ().battleCenter = battleCenter;
				s.GetComponent<StarshipAI> ().battleSize = battleSize;
				s.GetComponent<StarshipAI> ().player = player;
			}
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

	void SetFormation() {
		formationMode = !formationMode;
		if (formationMode) {
			for (int f = 0; f < player.formationTarget.Length; f++) {
				int d = 1;
				for (int i = 0; i < objects.Length; i++) {
					float newDist = Vector3.Distance (player.transform.position, objects [i].transform.position);
					float oldDist = Vector3.Distance (player.transform.position, objects [d].transform.position);
					if (newDist < oldDist) {
						if (objects [i].GetComponent<StarshipAI> () != null) {
							if (!objects [i].GetComponent<StarshipAI> ().inFormation)
								d = i;
						}
					}
				}
				objects [d].GetComponent<StarshipAI> ().formationTarget = player.formationTarget [f];
				objects [d].GetComponent<StarshipAI> ().inFormation = true;
			}
		} else {
			for (int i = 0; i < objects.Length; i++) {
				if (objects [i] != player.gameObject) {
					objects [i].GetComponent<StarshipAI> ().inFormation = false;
					objects [i].GetComponent<StarshipAI> ().formationTarget = null;
				}
			}
		}
	}

	void Update () {
		Respawn ();

		if (oTag == player.gameObject.tag) {
			if (CrossPlatformInputManager.GetButtonDown ("FormationMode")) {
				SetFormation ();
			}
		}
	}

	void OnDrawGizmos() {
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube (transform.position, SpawnRegion*2);
	}
}
