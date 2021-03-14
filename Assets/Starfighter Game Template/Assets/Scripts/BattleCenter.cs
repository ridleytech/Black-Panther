using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCenter : MonoBehaviour {

	public float battleSize;

	void OnDrawGizmos() {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, battleSize);
	}
}
