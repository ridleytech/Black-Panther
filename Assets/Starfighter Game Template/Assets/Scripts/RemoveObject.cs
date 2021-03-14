using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveObject : MonoBehaviour {

	public int DestroyAfterSec;
	public AudioClip SoundFX;
	public GameObject Effect;

	void Start()
	{
		StartCoroutine(Remove());
		if (SoundFX != null)
			AudioSource.PlayClipAtPoint (SoundFX, transform.position);
	}

	IEnumerator Remove()
	{
		yield return new WaitForSeconds(DestroyAfterSec);
		if (Effect != null)
			Instantiate (Effect, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
