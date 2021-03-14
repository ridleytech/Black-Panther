using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollider : MonoBehaviour {

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

		if (other.GetComponent<Collider>().gameObject.tag == "Emerald AI" && ps.attacking && other.gameObject == gm.currentTarget) {

			//print ("hit Emerald AI");

			if (other.GetComponent<Collider>().gameObject.GetComponent<Emerald_AI> () != null) {

				damage = 5;

				//Emerald Damage from UFPS Randall

				Emerald_AI EmeraldComponent = other.GetComponent<Collider>().gameObject.GetComponent<Emerald_AI> ();
				EmeraldComponent.Damage (damage, Emerald_AI.TargetType.Player);
			}
		}
		else if (other.GetComponent<Collider>().gameObject.name.Contains("Playmaker") && ps.swingingSword) {
		
			//print ("Playmaker");

			Animator anim = other.GetComponentInParent<Animator> ();

			if (ps.isSwordAttack) {

				anim.SetTrigger ("takeDamage");
			
				EnemyHealth eh = other.GetComponentInParent<EnemyHealth>();
				eh.health -= 25f;

//				AudioSource audio = GetComponent<AudioSource>();
//
//				int rand = Random.Range (0, gm.hitClips.Length);
//
//				//yield return new WaitForSeconds(audio.clip.length);
//
//				if (!audio.isPlaying) {
//					audio.clip = gm.hitClips[rand];
//					audio.Play();
//				}
			}


		}

	}
}
