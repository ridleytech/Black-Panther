using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootCollider : MonoBehaviour {

	PlayerState ps;
	GameManager gm;

	void Start () {

		ps = GetComponentInParent<PlayerState> ();
		gm = GameObject.Find ("GameController").GetComponent<GameManager>();
	}

//	void OnCollisionEnter (Collision col)
//	{
//		print ("col name: " + col.gameObject.name);
//
//		if(col.gameObject.GetComponent<Collider>().gameObject.tag == "Emerald AI")
//		{
//			print ("col Emerald AI");
//		}
//	}

	void OnTriggerEnter (Collider other) {

		int damage = 100;

//		print ("other col name: " + other.gameObject.name);
//		print ("other col  : " + other.gameObject.tag);

		if (other.GetComponent<Collider>().gameObject.name.Contains("SweepBox") && ps.isFootAttack) {
		
			print ("sweep collider");

			Animator anim = other.GetComponentInParent<Animator> ();

			anim.SetTrigger ("swept");

			EnemyHealth eh = other.GetComponentInParent<EnemyHealth>();
			eh.health -= 10f;

//			AudioSource audio = GetComponent<AudioSource>();
//
//			int rand = Random.Range (0, gm.hitClips.Length);
//
//			//yield return new WaitForSeconds(audio.clip.length);
//
//			if (!audio.isPlaying) {
//				audio.clip = gm.hitClips[rand];
//				audio.Play();
//			}
		}

	}
}
