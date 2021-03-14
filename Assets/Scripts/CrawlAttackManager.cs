using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlAttackManager : MonoBehaviour {

	GameManager gm;
	PlayerState ps;
	Animator anim;

	// Use this for initialization
	void Start () {

		gm = GameObject.Find("GameController").GetComponent<GameManager>();
		ps = GetComponent<PlayerState>();
		anim = GetComponent<Animator>();
	}

//	void OnTriggerEnter (Collider other) {
//
//		if(other.gameObject == gm.currentTarget)
//		{
//			print ("reached target now sweep");
//			anim.SetBool ("crawl",false);
//			//anim.SetTrigger ("sweep");
//			ps.attacking = false;
//		}
//	}
	
	// Update is called once per frame
	void Update () {

		if(anim.GetBool("crawl") == true && gm.currentTarget)
		{
			float distance = Vector3.Distance (gameObject.transform.position,gm.currentTarget.transform.position);

			if(distance < 1)
			{
				print ("reached target now sweep");
				anim.SetBool ("crawl",false);
				ps.attacking = false;
			}
		}
		
	}
}
