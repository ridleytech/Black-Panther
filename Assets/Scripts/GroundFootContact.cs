using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GroundFootContact : MonoBehaviour {

	PlayerState ps;

	// Use this for initialization
	void Start () {

		ps = GetComponentInParent<PlayerState> ();
	}

//	void OnTriggerEnter (Collider other) {
//
//		//print ("trigger enter:" + other.tag);
//
//		if (other.gameObject.name.Contains("Terrain"))
//		{
//			print ("landed");
//			ps.jumping = false;
//		}
//
//	}
}
