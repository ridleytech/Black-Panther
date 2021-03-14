using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnvironment : MonoBehaviour {

	public GameObject[] enviro;
	public GameObject player;
	bool active = false;

	void Update () {
		if (!active) {
			if (player.GetComponent<PlayerSpaceship> ().enabled) {
				int r = Random.Range (0, enviro.Length);
				enviro [r].SetActive (true);
				active = true;
			}
		}
	}
}
