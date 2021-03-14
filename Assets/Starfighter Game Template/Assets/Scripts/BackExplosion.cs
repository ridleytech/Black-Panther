using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackExplosion : MonoBehaviour {

	public GameObject parent;
	public float TimerMin;
	public float TimerMax;
	[HideInInspector]
	public bool finished = true;

	public void PlayAnim () {
		finished = false;
		GetComponent<ParticleSystem> ().Play ();
		StartCoroutine (TimerCount());
	}

	IEnumerator TimerCount () {
		yield return new WaitForSeconds (Random.Range(TimerMin,TimerMax));
		finished = true;
	}
}
