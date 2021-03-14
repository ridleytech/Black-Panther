using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackExplosionsManager : MonoBehaviour {

	public int quantity;
	public GameObject explosionPrefab;
	public GameObject[] explosion;
	public float minPos;
	public float maxPos;
	public float rate;
	float timer;

	void Awake () {
		for (int i = 0; i < quantity; i++) {
			Instantiate (explosionPrefab, transform.position, transform.rotation, gameObject.transform);
		}
		explosion = GameObject.FindGameObjectsWithTag ("BG Explosion");
		for (int i = 0; i < explosion.Length; i++) {
			explosion [i].GetComponent<BackExplosion>().parent = gameObject;
		}
	}

	void Update () {
		if (timer <= 0) {
			for (int i = 0; i < explosion.Length; i++) {
				if (explosion [i].GetComponent<BackExplosion>().finished) {
					int r0 = Random.Range (0, 2);
					if (r0 == 0)
						r0 = -1;
					int r1 = Random.Range (0, 2);
					if (r1 == 0)
						r1 = -1;
					int r2 = Random.Range (0, 2);
					if (r2 == 0)
						r2 = -1;
					float x = Random.Range (minPos, maxPos) * r0;
					float y = Random.Range (minPos, maxPos) * r1;
					float z = Random.Range (minPos, maxPos) * r2;
					explosion [i].transform.localPosition = new Vector3 (x, y, z);
					explosion [i].GetComponent<BackExplosion>().PlayAnim ();
					timer = rate;
				}
			}
		} else {
			timer -= 0.1f;
		}
	}
}
