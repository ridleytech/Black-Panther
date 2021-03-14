using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HandContact : MonoBehaviour {

	private GameObject opponent;
	private Animator opponentanim;
	PlayerState ps;
	PlayerState opponentPS;
	EnemyHealth eh;
	public Animator anim;

//	public Text player1Txt;
//	public Text player2Txt;

	// Use this for initialization
	void Start () {

		ps = GetComponentInParent<PlayerState> ();
		anim = GetComponentInParent<Animator> ();
	}

	void OnTriggerEnter (Collider other) {

		//print ("trigger enter:" + other.tag);

		float attackCurve = anim.GetFloat(Animator.StringToHash("attack1"));

		if (other.tag == "enemy" && other.gameObject != gameObject && attackCurve > .04f) {

			opponent = other.gameObject;

			opponentPS = opponent.GetComponentInParent<PlayerState> ();
			//opponentanim = opponent.GetComponentInParent<Animator> ();
			eh = opponent.GetComponentInParent<EnemyHealth> ();

			if (eh.health > 0) {

				print ("player slashed");

				eh.health -= 105;
				//opponentanim.SetTrigger ("Knockdown");
				//opponentanim.SetBool ("Dead", true);

				//Rigidbody tb = opponent.GetComponent<Rigidbody> ();

				//opponentPS.dead = true;
			}


			//opponentPS.knockedDown = true;
			//player2Txt.text = "Energy: " + ps.energy.ToString();
		}
	}
}
