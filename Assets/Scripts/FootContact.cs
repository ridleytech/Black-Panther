using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FootContact : MonoBehaviour {

	public GameObject opponent;
	public Animator opponentanim;
	PlayerState ps;
	PlayerState opponentPS;
	public Text player1Txt;
	public Text player2Txt;

	// Use this for initialization
	void Start () {

		ps = GetComponentInParent<PlayerState> ();
		opponentPS = opponent.GetComponent<PlayerState> ();
	}

	void OnTriggerEnter (Collider other) {

		//print ("trigger enter:" + other.tag);

		if (other.tag == "Player" && other.gameObject != gameObject && ps.attacking && !opponentPS.knockedDown) {

			//print ("player kicked");

			ps.energy -= 10;
			opponentanim.SetTrigger ("Knockdown");
			opponentPS.knockedDown = true;
			player2Txt.text = "Energy: " + ps.energy.ToString();
		}

	}
}
