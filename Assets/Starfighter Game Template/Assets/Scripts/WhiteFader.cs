using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteFader : MonoBehaviour {

	public int fade;

	void Update () {
		if (fade == 1) {
			if (GetComponent<Image> ().color.a < 1)
				GetComponent<Image> ().color = new Color (1, 1, 1, GetComponent<Image> ().color.a + 0.02f);
		} else {
			if (GetComponent<Image> ().color.a > 0)
				GetComponent<Image> ().color = new Color (1, 1, 1, GetComponent<Image> ().color.a - 0.02f);
		}
	}
}
