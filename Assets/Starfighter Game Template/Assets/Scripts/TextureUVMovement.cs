using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureUVMovement : MonoBehaviour {

	public float speedX; //Movement on X axis of the texute
	public float speedY; //and on Y axis as well

	void Update () {
		// I divide X and Y speed values by 100 so that the speed of the movement wouldn't be too fast
		// otherwise you should use really low numbers
		GetComponent<MeshRenderer> ().material.mainTextureOffset += new Vector2 ((speedX/100), (speedY/100));
	}
}
