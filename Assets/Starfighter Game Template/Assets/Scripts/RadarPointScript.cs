using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Starfighter Game Template/Radar Point Script")]
public class RadarPointScript : MonoBehaviour {

	[HideInInspector]
	public Transform target;
	public GameObject sprite;
	public GameObject zeroSprite;
	public GameObject pointArrow;
	public LineRenderer line;
	Transform radarTransform;
	bool visible;

	void Awake () {
		radarTransform = GameObject.Find ("Player").transform;
		visible = false;
	}

	void Update () {
		transform.position = target.position;

		if (Vector3.Distance (transform.position, radarTransform.position) <= radarTransform.gameObject.GetComponent<PlayerSpaceship> ().radarDistance)
			visible = true;
		else
			visible = false;

		transform.rotation = radarTransform.rotation;

		line.SetPosition (0, new Vector3 (transform.position.x, transform.position.y, transform.position.z));
		if (visible) {
			if (pointArrow != null)
				pointArrow.SetActive (false);
			sprite.SetActive (true);
			RaycastHit hit;
			if (Physics.Raycast (transform.position, -transform.up, out hit, LayerMask.GetMask("Radar"))) {
				line.SetPosition (1, hit.point);
				if (zeroSprite != null)
					zeroSprite.transform.position = hit.point;
			}
			if (Physics.Raycast (transform.position, transform.up, out hit, LayerMask.GetMask("Radar"))) {
				line.SetPosition (1, hit.point);
				if (zeroSprite != null)
					zeroSprite.transform.position = hit.point;
			}
		} else {
			if (pointArrow != null) {
				pointArrow.SetActive (true);
				pointArrow.transform.position = radarTransform.position;
				pointArrow.transform.LookAt (sprite.transform.position);
				pointArrow.transform.localEulerAngles = new Vector3 (0, pointArrow.transform.localEulerAngles.y, 0);
			}
			sprite.SetActive (false);
			line.SetPosition (1, new Vector3 (transform.position.x, transform.position.y, transform.position.z));
		}
	}
}
