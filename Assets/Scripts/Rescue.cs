using UnityEngine;
using System.Collections;

public class Rescue : MonoBehaviour {

	GameManager gm;
	public bool rescued;
	private Animator anim;
	public Rescue rescue;

	void Start () {

		gm = GameObject.Find ("GameController").GetComponent<GameManager> ();	

		anim = GetComponent<Animator> ();
		rescue = GetComponent<Rescue> ();

		//print ("family: " + gm.family.Length);

//		for (int i = 0; i < gm.family.Length; i++) {
//
//			GameObject fam = gm.family [i] as GameObject;
//
//			print ("member name: "+fam.name);
//		}
	}
	
//	void Update () {
//	
//	}

	void OnTriggerEnter (Collider other) {

		if (other.tag == "Player") {

			//print ("trigger enter:" + other.tag);

			rescue.rescued = true;
			gm.rescued = true;
			anim.SetBool ("rescued", true);

			for (int i = 0; i < gm.family.Length; i++) {

				GameObject fam = gm.family [i] as GameObject;

				if (fam.name != "boy6 (1)") 
				{
					//print (fam.name + " rescued");

					Animator famAnim = fam.GetComponent<Animator> ();
					Rescue famRescue = fam.GetComponent<Rescue> ();

					famAnim.SetBool ("rescued", true);
					famRescue.rescued = true;
				}
			}
		}
	}
}
